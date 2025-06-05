using Cinema.Application.Application.Interfaces.Cinema;
using Cinema.Application.Common.Interfaces;
using Cinema.Application.DTO.Movie;
using Cinema.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Cinema.API.Controllers.Cinema
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieHubService _movieHubService;
        private readonly IWebHostEnvironment _env;

        public MovieController(IMovieRepository movieRepository, IMovieHubService movieHubService, IWebHostEnvironment env)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _movieHubService = movieHubService ?? throw new ArgumentNullException(nameof(movieHubService));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
        {
            return Ok(await _movieRepository.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MovieDto>> GetById(Guid id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return movie is null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> Create([FromBody] CreateMovieDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _movieRepository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _movieRepository.UpdateAsync(id, dto);

            await _movieHubService.SendMovieUpdateAsync(new MovieDto
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                Duration = dto.Duration.TotalMinutes.ToString(),
                Rating = dto.Rating,
                ImagePath = dto.ImagePath
            });
        
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _movieRepository.DeleteAsync(id);
        
            await _movieHubService.SendMovieDeleteAsync(id);
        
            return deleted ? NoContent() : NotFound();
        }
    
        /// <summary>
        /// Загрузка обложки
        /// </summary>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            //TODO: указание Dev/Host
            //var uploadsFolder = Path.Combine("/root/riderDeploy/wwwroot", "images", "movies");
            var uploadsFolder = Path.Combine("C:\\GitHubRepositories\\cinemaNet\\Cinema.Client\\wwwroot\\images_movies", "images", "movies");
            Directory.CreateDirectory(uploadsFolder); // Создаем папку, если её нет

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Возвращаем URL файла
            var fileUrl = $"/images/movies/{uniqueFileName}";
            return Ok(fileUrl);
        }
    
        /// <summary>
        /// Удаление старой обложки(при обновлении)
        /// </summary>
        [HttpDelete("delete-image")]
        public IActionResult DeleteImage([FromQuery] string filename)
        {
            var imagePath = Path.Combine(_env.WebRootPath, "images", "movies", filename);

            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    System.IO.File.Delete(imagePath);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Ошибка при удалении файла: {ex.Message}");
                }
            }

            return NotFound("Файл не найден");
        }
    }
}
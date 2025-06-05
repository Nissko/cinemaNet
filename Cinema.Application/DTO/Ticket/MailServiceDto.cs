namespace Cinema.Application.DTO.Ticket
{
    public record MailServiceDto
    {
        // Receiver
        public List<string> To { get; init; }
        public List<string> Bcc { get; init; }
        public List<string> Cc { get; init; }
        // Sender
        public string? From { get; init; }
        public string? DisplayName { get; init; }
        public string? ReplyTo { get; init; }
        public string? ReplyToName { get; init; }
        // Content
        public string Subject { get; init; }
        public string? Body { get; init; }        
    };
}


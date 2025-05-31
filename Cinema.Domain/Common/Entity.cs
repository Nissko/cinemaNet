namespace Cinema.Domain.Common
{
    public abstract class Entity
    {
        Guid _Id;
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}

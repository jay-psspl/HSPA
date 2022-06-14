namespace WebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository cityRepository { get; }

        IUserRepository UserRepository { get; } 

        Task<bool> SaveAsync();
    }
}

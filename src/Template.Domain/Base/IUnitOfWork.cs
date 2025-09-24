namespace Template.Domain.Base
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}

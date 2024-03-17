namespace Taskify.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetList();

        Task<T> GetItem(Guid id);

        Task<Guid> Create(T item);
        Task<Guid> Update(T item);
        Task<Guid> Delete(Guid id);
    }
}

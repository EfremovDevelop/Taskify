namespace Taskify.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetList();

        Task<T> GetItem(int id);

        Task<int> Create(T item);
        Task<int> Update(T item);
        Task<int> Delete(int id);
    }
}

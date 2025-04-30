namespace WebApi_ProjectTaskComments.Services.Interfaces
{
    public interface IUniversalAsyncService<T>
    {
        IEnumerable<T> GetAllAsync();
        IEnumerable<T> GetAllByParentItemIdAsync(int parentItemId);
        T GetByIdAsync(int id);
        T CreateAsync(T item);
        T UpdateAsync(T item);
        bool DeleteAsync(int id);
        bool Delete(T item);
    }
}

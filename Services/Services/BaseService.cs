using Repositories.Models;
using Repositories.Repositories;

namespace Services.Services
{
    public interface IService<T> where T : IAmAModel, new()
    {
        Task<IList<T>> GetAll();
        Task Insert(T item);
        Task Delete(T item);
        Task<T> GetById(int id);
    }
    public class BaseService<T> : IService<T> where T : IAmAModel, new()
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IList<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Insert(T item)
        {
            await _repository.Insert(item);
        }

        public async Task Delete(T item)
        {
            await _repository.Delete(item);
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}

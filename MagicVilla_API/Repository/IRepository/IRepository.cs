using System;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository
{
	public interface IRepository<T> where T : class
	{
		Task Create(T entity);
		Task<List<T>> GetAll(Expression<Func<T,bool>>? filter = null);
		Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);
		Task Remove(T entity);
		Task Save();
    }
}


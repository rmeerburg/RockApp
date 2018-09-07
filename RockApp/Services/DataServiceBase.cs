using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RockApp.Models;

namespace RockApp.Services
{
    /// <summary>  
    ///     Base class for querying with the database
    /// </summary>  
    public abstract class DataServiceBase<TData, TKey> : IDataServiceBase<TData, TKey>
        where TData: class, IIdentifyable<TKey>
    {
        protected RockAppContext Context { get; }

        public DataServiceBase(RockAppContext context)
        {
            Context = context;
        }

        protected abstract DbSet<TData> DataSet { get; }
        
        public virtual IEnumerable<TData> GetAllAsync() => DataSet;

        public virtual Task<TData> GetAsync(TKey id) => DataSet.FirstOrDefaultAsync(d => d.Id.Equals(id));

        public virtual async Task<TData> CreateAsync(TData data)
        {
            DataSet.Add(data);
            await Context.SaveChangesAsync();
            return data;
        }

        public virtual async Task<TData> SaveAsync(TData data)
        {
            var savedData = DataSet.Update(data);
            await Context.SaveChangesAsync();
            return savedData.Entity;
        }

        public virtual async Task Delete(TData data)
        {
            DataSet.Remove(data);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(TKey id)
        {
            await Context.SaveChangesAsync();
        }
    }

    public interface IDataServiceBase<TData, TKey>
        where TData: class, IIdentifyable<TKey>
    {
        IEnumerable<TData> GetAllAsync();
        Task<TData> GetAsync(TKey id);
        Task<TData> CreateAsync(TData data);
        Task<TData> SaveAsync(TData data);
        Task Delete(TData data);
        Task Delete(TKey id);
    }
}
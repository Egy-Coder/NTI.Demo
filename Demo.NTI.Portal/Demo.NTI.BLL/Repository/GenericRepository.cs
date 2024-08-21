using Demo.NTI.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demo.NTI.BLL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(
                                                             Expression<Func<TEntity, bool>>? filter = null,
                                                             int? page = null,
                                                             int pageSize = 0,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                             List<Expression<Func<TEntity, object>>>? includeProperties = null,
                                                            bool noTrack = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>(); // _context.employee;

            if (filter != null)
            {
                query = query.Where(filter); // _context.employee.where(a => a.id == 10);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty); // eager Load
                    // _context.employee.where(a => a.id == 10).include(department);
                }
            }

            if (noTrack)
            {
                query = query.AsNoTracking();
                // _context.employee.where(a => a.id == 10).include(department).AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
                // _context.employee.where(a => a.id == 10).include(department).AsNoTracking().OrderBy(a => a.id);
            }

            if (page.HasValue && page > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize).Take(pageSize);
                // _context.employee.where(a => a.id == 10).include(department).AsNoTracking().OrderBy(a => a.id).skip().take();
            }

            return await query.ToListAsync();
            // _context.employee.where(a => a.id == 10).include(department).AsNoTracking().OrderBy(a => a.id).skip().take().ToListAsync();
        }




        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<int> CountAsync()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>(); // context.Employees;
            return await query.CountAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>>? includeProperties = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty); // eager Load
                }
            }

            return await query.FirstOrDefaultAsync();
        }
    }

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetAllAsync(
                                                Expression<Func<TEntity, bool>>? filter = null,
                                                int? page = null,
                                                int pageSize = 10,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
                                                List<Expression<Func<TEntity, object>>>? includeProperties = null,
                                                bool noTrack = false);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity,bool>> filter,
            List<Expression<Func<TEntity, object>>>? includeProperties = null);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        public Task<int> CountAsync();
    }
}


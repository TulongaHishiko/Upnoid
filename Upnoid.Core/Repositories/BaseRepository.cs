using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Domain.Abstracts;

namespace Upnoid.Core.Repositories
{
    public class BaseRepository<T>:IBaseRepository<T> where T:BaseModel
    {
        private UpnoidContext _context;

        #region Properties
        public BaseRepository(UpnoidContext context)
        {
            _context = context;
        }
        #endregion
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => _context.Set<T>().AsEnumerable());
        }
        public virtual async Task<int> Count()
        {
            return await Task.Run(() => _context.Set<T>().Count());
        }
        public virtual async Task<IEnumerable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Task.Run(() =>
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query.AsEnumerable();
            }
            );
        }

        public virtual async Task<T> GetSingle(int id)
        {
            return await Task.Run(() =>
            {
                return _context.Set<T>().FirstOrDefault(x => x.Id == id);
            });
        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<T>().FirstOrDefault(predicate);
            });

        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Task.Run(() =>
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return query.Where(predicate).FirstOrDefault();
            });

        }

        public virtual async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<T>().Where(predicate);
            });

        }

        public virtual async Task Add(T entity)
        {
            await Task.Run(() =>
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                _context.Set<T>().Add(entity);
            }).ContinueWith((i) => _context.SaveChanges());

        }
        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await Task.Run(() =>
            {
                _context.Set<T>().AddRange(entities);
            }).ContinueWith((i) => _context.SaveChanges());

        }
        public virtual async Task Update(T entity)
        {
            await Task.Run(() =>
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Modified;
            }).ContinueWith((i) => _context.SaveChanges());

        }
        public virtual async Task Delete(int entityID)
        {
            await Task.Run(async () =>
            {
                T entity = await GetSingle(entityID);

                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }).ContinueWith((i) => _context.SaveChanges());

        }

        public virtual async Task DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            await Task.Run(() =>
            {
                IEnumerable<T> entities = _context.Set<T>().Where(predicate);

                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                    _context.Entry<T>(entity).State = EntityState.Modified;
                }
            }).ContinueWith((i) => _context.SaveChanges());

        }
    }
}

using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagment.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly HrDatabaseContext _context;

        public GenericRepository(HrDatabaseContext context)
        {
            this._context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync()
        {   // as we are expecting a generic type we only specify the set of that type.
            // This AsNoTracking improves the performance of the app since EF core is not tracking 
            // changes because you're only getting the data in DBContext. 
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

       
    }
}

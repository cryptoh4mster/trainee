using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using traineeDAL.EF;
using traineeDAL.Interfaces;

namespace traineeDAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly TraineeDbContext _context;
        private readonly DbSet<TEntity> _table;

        public GenericRepository(TraineeDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            await _table.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<TEntity> Update(TEntity obj)
        {
            //TODO: Проверить обновляются все поля или только измененные??
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(object id)
        {
            TEntity entityExist = await _table.FindAsync(id);
            _table.Remove(entityExist);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Data
{
    public class ModpackTypeService
    {
        private ApplicationDbContext _dbContext;

        public ModpackTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<ModpackType>> GetAllAsync()
        {
            return await _dbContext.Modpacks.Include(x => x.Missions).ToListAsync();
        }

        public async Task<ModpackType?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Modpacks.Include(x => x.Missions).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ModpackType?> GetMostCommonAsync()
        {
            return (await _dbContext.Modpacks.Include(x => x.Missions).ToListAsync()).MaxBy(x => x.Missions.Count);
        }

        public async Task InsertOrUpdateAsync(ModpackType modpack)
        {
            if (await _dbContext.Modpacks.AnyAsync(x => x.Id == modpack.Id))
                _dbContext.Modpacks.Update(modpack);
            else
                _dbContext.Modpacks.Add(modpack);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var modpack = _dbContext.Modpacks.FirstOrDefault(x => x.Id == id);
            if (modpack is not null)
            {
                _dbContext.Modpacks.Remove(modpack);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
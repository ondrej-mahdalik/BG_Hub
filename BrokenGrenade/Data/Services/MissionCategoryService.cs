using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Data
{
    public class MissionCategoryService
    {
        private ApplicationDbContext _dbContext;

        public MissionCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<MissionCategory>> GetAllAsync()
        {
            return await _dbContext.Categories
                .Include(x => x.Missions)
                .ToListAsync();
        }

        public async Task<MissionCategory?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories
                .Include(x => x.Missions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MissionCategory?> GetMostCommonAsync()
        {
            return (await _dbContext.Categories
                .Include(x => x.Missions)
                .ToListAsync())
                .MaxBy(x => x.Missions.Count);
        }

        public async Task InsertOrUpdateAsync(MissionCategory category)
        {
            if (await _dbContext.Missions.AnyAsync(x => x.Id == category.Id))
                _dbContext.Categories.Update(category);
            else
                _dbContext.Categories.Add(category);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is not null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
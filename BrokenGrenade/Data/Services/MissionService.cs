using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Data
{
    public class MissionService
    {
        private ApplicationDbContext _dbContext;

        public MissionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Mission>> GetAllAsync(DateTime? start = null, DateTime? end = null)
        {
            var query = _dbContext.Missions
                .Include(i => i.Author)
                .Include(i => i.Category).Where(x => true);

            if (start.HasValue)
                query = query.Where(x => x.Start > start.Value);

            if (end.HasValue)
                query = query.Where(x => x.Start < end.Value);

            return await query.ToListAsync();
        }

        public async Task<List<Mission>> GetUpcomingAsync(DateTime? end = null)
        {
            var query = _dbContext.Missions
                .Include(i => i.Author)
                .Include(i => i.Category)
                .Where(x => x.Start > DateTime.Now);

            if (end.HasValue)
                query = query.Where(x => x.Start < end.Value);

            return await query.ToListAsync();
        }

        public async Task<int> GetMissionCountAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = _dbContext.Missions.Where(x => true);
            if (from.HasValue)
                query = query.Where(x => x.Start > from);

            if (to.HasValue)
                query = query.Where(x => x.Start < to);

            return await query.CountAsync();
        }

        public async Task<Mission?> GetByIdAsync(Guid missionId)
        {
            return await _dbContext.Missions
                .Include(i => i.Author)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(x => x.Id == missionId);
        }

        public async Task InsertOrUpdateAsync(Mission mission)
        {
            if (await _dbContext.Missions.AnyAsync(x => x.Id == mission.Id))
                _dbContext.Missions.Update(mission);
            else
                _dbContext.Missions.Add(mission);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid missionId)
        {
            var mission = _dbContext.Missions.FirstOrDefault(x => x.Id == missionId);
            if (mission is not null)
            {
                _dbContext.Missions.Remove(mission);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
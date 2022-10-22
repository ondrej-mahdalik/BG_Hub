using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Data
{
    public class UserService
    {
        private ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .Include(x => x.Missions)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _dbContext.Users
                .Include(x => x.Missions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertOrUpdateAsync(User user)
        {
            if (await _dbContext.Users.AnyAsync(x => x.Id == user.Id))
                _dbContext.Users.Update(user);
            else
                _dbContext.Users.Add(user);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user is not null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrokenGrenade.Data;

namespace BrokenGrenade.Pages
{
    public class MissionsModel : PageModel
    {
        private readonly BrokenGrenade.Data.ApplicationDbContext _context;

        public MissionsModel(BrokenGrenade.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Mission> Mission { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Missions != null)
            {
                Mission = await _context.Missions
                .Include(m => m.Author)
                .Include(m => m.Category).ToListAsync();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Stark.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stark.Models;
using Microsoft.EntityFrameworkCore;

namespace Stark.Components
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation : ViewComponent
    {
        private readonly StarkDbContext _dbContext;

        public Navigation(StarkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _dbContext.WebLinks.OrderBy(w => w.DisplayOrder).ToListAsync());
        }
    }
}

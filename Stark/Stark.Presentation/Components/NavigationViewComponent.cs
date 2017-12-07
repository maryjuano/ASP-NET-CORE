using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stark.DAL;
using Microsoft.EntityFrameworkCore;

namespace Stark.Presentation.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public NavigationViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var links = await _dbContext.WebLinks.OrderBy(w => w.DisplayOrder).ToListAsync();

            return View(links);
        }
    }
}

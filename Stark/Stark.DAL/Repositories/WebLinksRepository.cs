using Stark.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stark.DAL.Repositories
{
    public class WebLinksRepository : GenericRepository<WebLink>, IWebLinksRepository
    {
        public WebLinksRepository(AppDbContext context) : base(context)
        {
        }

        public WebLink GetWebLink(int WebLinkId)
        {
            var query = GetAll().FirstOrDefault(b => b.Id == WebLinkId);
            return query;
        }

        public async Task<WebLink> GetSingleAsyn(int WebLinkId)
        {
            return await _context.Set<WebLink>().FindAsync(WebLinkId);
        }


    }
}

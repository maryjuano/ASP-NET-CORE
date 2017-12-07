using Stark.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stark.DAL.Repositories
{
    public interface IWebLinksRepository : IGenericRepository<WebLink>
    {
        WebLink GetWebLink(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stark.DAL.Repositories;
using Stark.DAL.Models;

namespace Stark.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/weblinks")]
    public class WebLinksController : Controller
    {
        IWebLinksRepository _webLinksRepo;

        public WebLinksController(IWebLinksRepository webLinksRepo)
        {
            _webLinksRepo = webLinksRepo;
        }

        [Route("~/api/weblinks/get")]
        [HttpGet]
        public async Task<IEnumerable<WebLink>> Index()
        {
            return await _webLinksRepo.GetAllAsyn();
        }

        protected override void Dispose(bool disposing)
        {
            _webLinksRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
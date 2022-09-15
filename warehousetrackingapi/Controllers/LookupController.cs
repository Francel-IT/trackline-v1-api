using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{
    [Route("lookup")]

    public class LookupController : Controller
    {

        [NonAction]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("gettypes")]
        public async Task<IEnumerable<LookupModel>> GetTypes()
        {
            await using (var context = new Context())
            {
                return (from a in context.Types.Where(a => a.Active.Equals("Active"))
                 select new LookupModel
                 {
                     Value = a.Guid,
                     Text = a.Type

                 }).ToList();
            }
        }

        [HttpGet]
        [Route("getlocations")]
        public async Task<IEnumerable<LookupModel>> GetLocations()
        {
            await using (var context = new Context())
            {
                return (from a in context.Location.Where(a => a.Active.Equals("Active"))
                        select new LookupModel
                        {
                            Value = a.Guid,
                            Text = a.Location

                        }).ToList();
            }
        }


        [HttpGet]
        [Route("getreaders")]
        public async Task<IEnumerable<LookupModel>> GetReaders()
        {
            await using (var context = new Context())
            {
                return (from a in context.Readers.Where(a => a.Active.Equals("Active"))
                        select new LookupModel
                        {
                            Value = a.Guid,
                            Text = a.Readername

                        }).ToList();
            }
        }

        [HttpGet]
        [Route("getstations")]
        public async Task<IEnumerable<LookupModel>> GetStations()
        {
            await using (var context = new Context())
            {
                return (from a in context.Stations.Where(a => a.Active.Equals("Active"))
                        select new LookupModel
                        {
                            Value = a.Guid,
                            Text = a.Station

                        }).ToList();
            }
        }

        [HttpGet]
        [Route("getcategories")]
        public async Task<IEnumerable<LookupModel>> GetCategories()
        {
            await using (var context = new Context())
            {
                return (from a in context.Categories.Where(a => a.Active.Equals("Active"))
                        select new LookupModel
                        {
                            Value = a.Guid,
                            Text = a.Category

                        }).ToList();

            }
        }
    }
}

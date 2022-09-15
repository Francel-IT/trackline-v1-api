using Microsoft.AspNetCore.Mvc;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{

    [Route("location")]
    public class LocationController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getlocations")]
        public async Task<IEnumerable<LocationModel>> Get()
        {
            await using (var context = new Context())
            {
                return  context.Location.ToList();
            }
        }


        [HttpGet]
        [Route("getlocationbyid")]
        public async Task<LocationModel> GetLocationById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Location.Where(a => a.Guid.ToString() == Id).FirstOrDefault();

            }
        }


        [HttpPost]
        [Route("addlocation")]
        public async Task<ResponseModel> AddLocation([FromBody] LocationModel locationModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();

                var location = context.Location.Where(a => a.Location.ToLower() == locationModel.Location.ToLower()).FirstOrDefault();
                if (location == null)
                {
                    Guid guid = Guid.NewGuid();
                    locationModel.Guid = guid;
                    locationModel.Dateupdated = DateTime.Now;
                    locationModel.Updatedby = "currentuser";
                    locationModel.Datecreated = DateTime.Now;
                    locationModel.Createdby = "currentuser";
                    context.Location.Add(locationModel);
                    await context.SaveChangesAsync();

                    response.StatusCode = "200";
                    response.StatusMessage = "Location successfully saved!";

                    return response;


                }
                else
                {
                    response.StatusCode = "200";
                    response.StatusMessage = "Location already exist!";

                    return response;
                }
            }

        }

        [HttpPost]
        [Route("updatelocation")]
        public async Task<ResponseModel> UpdateLocation([FromBody] LocationModel locationModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (IsDataExist(locationModel.Location, locationModel.Guid))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Location already exist!";
                    return response;
                }
                else
                {
                    var location = context.Location.Where(a => a.Guid == locationModel.Guid).SingleOrDefault();

                    location.Dateupdated = DateTime.Now;
                    location.Updatedby = "currentuser";
                    location.Active = locationModel.Active;
                    location.Location = locationModel.Location;
                    location.Description = locationModel.Description;

                    context.Location.Update(location).Property(x => x.Id).IsModified = false;
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Location successully updated!";
                    return response;
                }
            }
        }


        [HttpPost]
        [Route("deletelocation")]
        public async Task<ResponseModel> DeleteLocation([FromBody] LocationModel locationModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                var result = context.Location.Where(a => a.Guid == locationModel.Guid).SingleOrDefault();
                context.Location.Remove(result);
                await context.SaveChangesAsync();
                response.StatusCode = "200";
                response.StatusMessage = "Location successully deleted!";
                return response;
            }
        }


        private Boolean IsDataExist(string Name, Guid Id)
        {
            using (var context = new Context())
            {

                var result = context.Location.Where(a => a.Location.ToLower() == Name.ToLower()).FirstOrDefault();

                if (result == null)
                {
                    return false;
                }
                else
                {
                    if (Id == Guid.Empty)
                    {
                        return true;
                    }
                    else
                    {
                        if (Id == result.Guid)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }

            }

        }
    }
}

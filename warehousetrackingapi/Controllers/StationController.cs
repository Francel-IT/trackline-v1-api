using Microsoft.AspNetCore.Mvc;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{
    [Route("station")]
    public class StationController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getstations")]
        public async Task<IEnumerable<StationModelDTO>> Get()
        {
            await using (var context = new Context())
            {

                var query = (from a in context.Stations
                             join b in context.Readers
                             on a.Reader equals b.Guid
                             select new StationModelDTO
                             {
                                 Station = a.Station,
                                 Reader = b.Readername,
                                 Active = a.Active,
                                 Createdby = a.Createdby,
                                 Datecreated = a.Datecreated,
                                 Updatedby = a.Updatedby,
                                 Dateupdated = a.Dateupdated,
                                 Id = a.Id,
                                 Guid = a.Guid

                             }).ToList();

                return query;
            }
        }


        [HttpGet]
        [Route("getstationbyid")]
        public async Task<StationModel> GetStationById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Stations.Where(a => a.Guid.ToString() == Id).FirstOrDefault();

            }
        }


        [HttpPost]
        [Route("addstation")]
        public async Task<ResponseModel> AddStation([FromBody] StationModel stationModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();

                if (IsDataExist(stationModel.Station, Guid.Empty))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Station already exist!";
                    return response;
                }
                else
                {

                    var station = context.Stations.Where(a => a.Station.ToLower() == stationModel.Station.ToLower()).FirstOrDefault();
                    if (station == null)
                    {
                        Guid guid = Guid.NewGuid();
                        stationModel.Guid = guid;
                        stationModel.Dateupdated = DateTime.Now;
                        stationModel.Updatedby = "currentuser";
                        stationModel.Datecreated = DateTime.Now;
                        stationModel.Createdby = "currentuser";
                        context.Stations.Add(stationModel);
                        await context.SaveChangesAsync();

                        response.StatusCode = "200";
                        response.StatusMessage = "Station successfully saved!";

                        return response;


                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.StatusMessage = "Station already exist!";

                        return response;
                    }
                }
            }

        }

        [HttpPost]
        [Route("updatestation")]
        public async Task<ResponseModel> UpdateStation([FromBody] StationModel stationModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (IsDataExist(stationModel.Station, stationModel.Guid))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Station already exist!";
                    return response;
                }
                else
                {
                    var station = context.Stations.Where(a => a.Guid == stationModel.Guid).SingleOrDefault();

                    station.Dateupdated = DateTime.Now;
                    station.Updatedby = "currentuser";
                    station.Active = stationModel.Active;
                    station.Station = stationModel.Station;
                    station.Reader = stationModel.Reader;

                    context.Stations.Update(station).Property(x => x.Id).IsModified = false;
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Station successully updated!";
                    return response;
                }
            }
        }


        [HttpPost]
        [Route("deletestation")]
        public async Task<ResponseModel> DeleteStation([FromBody] StationModel stationModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                var result = context.Stations.Where(a => a.Guid == stationModel.Guid).SingleOrDefault();
                context.Stations.Remove(result);
                await context.SaveChangesAsync();
                response.StatusCode = "200";
                response.StatusMessage = "Station successully deleted!";
                return response;
            }
        }


        private Boolean IsDataExist(string Name, Guid Id)
        {
            using (var context = new Context())
            {

                var result = context.Stations.Where(a => a.Station.ToLower() == Name.ToLower()).FirstOrDefault();

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

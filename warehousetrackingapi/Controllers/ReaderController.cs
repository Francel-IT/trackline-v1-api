using Microsoft.AspNetCore.Mvc;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{
    [Route("reader")]
    public class ReaderController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getreaders")]
        public async Task<IEnumerable<ReaderModel>> Get()
        {
            await using (var context = new Context())
            {
                return context.Readers.ToList();
            }
        }


        [HttpGet]
        [Route("getreaderbyid")]
        public async Task<ReaderModel> GetReaderById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Readers.Where(a => a.Guid.ToString() == Id).FirstOrDefault();

            }
        }


        [HttpPost]
        [Route("addreader")]
        public async Task<ResponseModel> AddReader([FromBody] ReaderModel readerModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();

                if (IsDataExist(readerModel.Readername, Guid.Empty))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Reader already exist!";
                    return response;
                }
                else
                {

                    var reader = context.Readers.Where(a => a.Readername.ToLower() == readerModel.Readername.ToLower()).FirstOrDefault();
                    if (reader == null)
                    {
                        Guid guid = Guid.NewGuid();
                        readerModel.Guid = guid;
                        readerModel.Dateupdated = DateTime.Now;
                        readerModel.Updatedby = "currentuser";
                        readerModel.Datecreated = DateTime.Now;
                        readerModel.Createdby = "currentuser";
                        context.Readers.Add(readerModel);
                        await context.SaveChangesAsync();

                        response.StatusCode = "200";
                        response.StatusMessage = "Reader successfully saved!";

                        return response;


                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.StatusMessage = "Reader already exist!";

                        return response;
                    }
                }
            }

        }

        [HttpPost]
        [Route("updatereader")]
        public async Task<ResponseModel> UpdateReader([FromBody] ReaderModel readerModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (IsDataExist(readerModel.Readername, readerModel.Guid))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Reader already exist!";
                    return response;
                }
                else
                {
                    var reader = context.Readers.Where(a => a.Guid == readerModel.Guid).SingleOrDefault();

                    reader.Dateupdated = DateTime.Now;
                    reader.Updatedby = "currentuser";
                    reader.Active = readerModel.Active;
                    reader.Readername = readerModel.Readername;
                    reader.Ipaddress = readerModel.Ipaddress;
                    reader.Port = readerModel.Port;

                    context.Readers.Update(reader).Property(x => x.Id).IsModified = false;
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Reader successully updated!";
                    return response;
                }
            }
        }


        [HttpPost]
        [Route("deletereader")]
        public async Task<ResponseModel> DeleteReader([FromBody] ReaderModel readerModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                var result = context.Readers.Where(a => a.Guid == readerModel.Guid).SingleOrDefault();
                context.Readers.Remove(result);
                await context.SaveChangesAsync();
                response.StatusCode = "200";
                response.StatusMessage = "Reader successully deleted!";
                return response;
            }
        }


        private Boolean IsDataExist(string Name, Guid Id)
        {
            using (var context = new Context())
            {

                var result = context.Readers.Where(a => a.Readername.ToLower() == Name.ToLower()).FirstOrDefault();

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

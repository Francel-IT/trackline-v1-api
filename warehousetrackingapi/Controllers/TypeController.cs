using Microsoft.AspNetCore.Mvc;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{

    [Route("type")]
    public class TypeController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("gettypes")]
        public async Task<IEnumerable<TypeModel>> Get()
        {
            await using (var context = new Context())
            {
                return  context.Types.ToList();
            }
        }


        [HttpGet]
        [Route("gettypebyid")]
        public async Task<TypeModel> GetTypeById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Types.Where(a => a.Guid.ToString() == Id).FirstOrDefault();

            }
        }


        [HttpPost]
        [Route("addtype")]
        public async Task<ResponseModel> AddType([FromBody] TypeModel typeModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();

                var type = context.Types.Where(a => a.Type.ToLower() == typeModel.Type.ToLower()).FirstOrDefault();
                if (type == null)
                {
                    Guid guid = Guid.NewGuid();
                    typeModel.Guid = guid;
                    typeModel.Dateupdated = DateTime.Now;
                    typeModel.Updatedby = "currentuser";
                    typeModel.Datecreated = DateTime.Now;
                    typeModel.Createdby = "currentuser";
                    context.Types.Add(typeModel);
                    await context.SaveChangesAsync();

                    response.StatusCode = "200";
                    response.StatusMessage = "Type successfully saved!";

                    return response;


                }
                else
                {
                    response.StatusCode = "200";
                    response.StatusMessage = "Type already exist!";

                    return response;
                }
            }

        }

        [HttpPost]
        [Route("updatetype")]
        public async Task<ResponseModel> UpdateType([FromBody] TypeModel typeModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (IsDataExist(typeModel.Type, typeModel.Guid))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Type already exist!";
                    return response;
                }
                else
                {
                    var type = context.Types.Where(a => a.Guid == typeModel.Guid).SingleOrDefault();

                    type.Dateupdated = DateTime.Now;
                    type.Updatedby = "currentuser";
                    type.Active = typeModel.Active;
                    type.Type = typeModel.Type;
                    type.Description = typeModel.Description;

                    context.Types.Update(type).Property(x => x.Id).IsModified = false;
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Type successully updated!";
                    return response;
                }
            }
        }


        [HttpPost]
        [Route("deletetype")]
        public async Task<ResponseModel> DeleteType([FromBody] TypeModel typeModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                var result = context.Types.Where(a => a.Guid == typeModel.Guid).SingleOrDefault();
                context.Types.Remove(result);
                await context.SaveChangesAsync();
                response.StatusCode = "200";
                response.StatusMessage = "Type successully deleted!";
                return response;
            }
        }


        private Boolean IsDataExist(string Name, Guid Id)
        {
            using (var context = new Context())
            {

                var result = context.Types.Where(a => a.Type.ToLower() == Name.ToLower()).FirstOrDefault();

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

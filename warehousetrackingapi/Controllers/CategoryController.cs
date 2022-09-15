using Microsoft.AspNetCore.Mvc;
using System.Net;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{

    [Route("category")]
    public class CategoryController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getcategories")]
        public async Task<IEnumerable<CategoryModel>> Get()
        {
            await using (var context = new Context())
            {
                return context.Categories.ToList();
            }
        }


        [HttpGet]
        [Route("getcategorybyid")]
        public async Task<CategoryModel> GetCategoryById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Categories.Where(a => a.Guid.ToString() == Id).FirstOrDefault();

            }
        }


        [HttpPost]
        [Route("addcategory")]
        public async Task<ResponseModel> AddCategory([FromBody] CategoryModel categoryModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
               
                if (IsDataExist(categoryModel.Category, Guid.Empty))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Category already exist!";
                    return response;
                }
                else { 

                var category = context.Categories.Where(a => a.Category.ToLower() == categoryModel.Category.ToLower()).FirstOrDefault();
                if (category == null)
                {
                    Guid guid = Guid.NewGuid();
                    categoryModel.Guid = guid;
                    categoryModel.Dateupdated = DateTime.Now;
                    categoryModel.Updatedby = "currentuser";
                    categoryModel.Datecreated = DateTime.Now;
                    categoryModel.Createdby = "currentuser";
                    context.Categories.Add(categoryModel);
                    await context.SaveChangesAsync();

                    response.StatusCode = "200";
                    response.StatusMessage = "Category successfully saved!";

                    return response;


                }
                else
                {
                    response.StatusCode = "200";
                    response.StatusMessage = "Category already exist!";

                    return response;
                }
                }
            }

        }

        [HttpPost]
        [Route("updatecategory")]
        public async Task<ResponseModel> UpdateCategory([FromBody] CategoryModel categoryModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (IsDataExist(categoryModel.Category, categoryModel.Guid))
                {

                    response.StatusCode = "200";
                    response.StatusMessage = "Category already exist!";
                    return response;
                }
                else
                {
                    var category = context.Categories.Where(a => a.Guid == categoryModel.Guid).SingleOrDefault();

                    category.Dateupdated = DateTime.Now;
                    category.Updatedby = "currentuser";
                    category.Active = categoryModel.Active;
                    category.Category = categoryModel.Category;
                    category.Description = categoryModel.Description;

                    context.Categories.Update(category).Property(x => x.Id).IsModified = false;
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Category successully updated!";
                    return response;
                }
            }
        }


        [HttpPost]
        [Route("deletecategory")]
        public async Task<ResponseModel> DeleteCategory([FromBody] CategoryModel categoryModel)
        {
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                var result = context.Categories.Where(a => a.Guid == categoryModel.Guid).SingleOrDefault();
                context.Categories.Remove(result);
                await context.SaveChangesAsync();
                response.StatusCode = "200";
                response.StatusMessage = "Category successully deleted!";
                return response;
            }
        }


        private Boolean IsDataExist(string Name, Guid Id)
        {
            using (var context = new Context())
            {

                var result = context.Categories.Where(a => a.Category.ToLower() == Name.ToLower()).FirstOrDefault();

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

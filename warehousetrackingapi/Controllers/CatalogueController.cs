using Microsoft.AspNetCore.Mvc;
using warehousetrackingapi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace warehousetrackingapi.Controllers
{
    [Route("catalogue")]
    public class CatalogueController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getcatalogues")]
        public async Task<IEnumerable<CatalogueModel>> Get()
        {
            await using (var context = new Context())
            {
                return context.Catalogues.ToList();
            }
        }

        [HttpGet]
        [Route("getcataloguebyid")]
        public async Task<CatalogueModel> GetCatalogueById(int id)
        {
            await using (var context = new Context())
            {
                return context.Catalogues.Where(a => a.Id == id).FirstOrDefault();
            }
        }

        [HttpGet]
        [Route("getcatalogueitems")]
        public async Task<IEnumerable<CatalogueItemsModel>> GetById(int Id)
        {
            await using (var context = new Context())
            {
                return context.CatalogueItems.Where(a => a.catalogueId == Id).ToList();
            }
        }

        [HttpGet]
        [Route("getcatalogueitemsbyworkorderid")]
        public async Task<IEnumerable<CatalogueItemsModel>> GetByWorkOrderId(int Id)
        {
            await using (var context = new Context())
            {
                return (from a in context.WorkOrders.Where(a => a.Workorderid == Id)
                             from b in context.CatalogueItems.Where(x => x.catalogueId.ToString() == a.CatalogueId)
                             select b).ToList();
            }
        }



        [HttpPost]
        [Route("addcatalogues")]
        public async Task<ResponseModel> AddCatalogue([FromBody] CatalogueDataModel cataloguedata)
        {

            List<CatalogueItemsModel> catalogueitemsmodel = cataloguedata.catalogueitems;
            await using (var context = new Context())
            {
                ResponseModel response = new ResponseModel();
                if (cataloguedata.name != "")
                {
                    
                    CatalogueModel cataloguemodel = new CatalogueModel();
                    Guid guid = Guid.NewGuid();
                    cataloguemodel.Guid = guid;
                    cataloguemodel.name = cataloguedata.name;
                    cataloguemodel.numberOfItems = cataloguedata.numberOfItems;
                    cataloguemodel.Dateupdated = DateTime.Now;
                    cataloguemodel.Updatedby = "currentuser";
                    cataloguemodel.Datecreated = DateTime.Now;
                    cataloguemodel.Createdby = "currentuser";
                    context.Catalogues.Add(cataloguemodel);
                    await context.SaveChangesAsync();
                    foreach (var item in catalogueitemsmodel)
                    {
                        CatalogueItemsModel catalogueitemmodel = new CatalogueItemsModel();
                        catalogueitemmodel = item;
                        catalogueitemmodel.Guid = guid;
                        catalogueitemmodel.catalogueId = cataloguemodel.Id;

                        context.CatalogueItems.Add(catalogueitemmodel);

                    }
                    await context.SaveChangesAsync();
                    response.StatusCode = "200";
                    response.StatusMessage = "Catalogue successfully saved!";
                    return response;
                }
                else
                {
                    response.StatusCode = "200";
                    response.StatusMessage = "";
                    return response;
                }
                
            }

        }

        [HttpPost]
        [Route("updatecatalogues")]
        public async Task<ActionResult> UpdateCatalogue([FromBody] CatalogueDataModel cataloguedata)
        {
            try
            {
                await using (var context = new Context())
                {
                    var catalogue = context.Catalogues.Where(a => a.Id == cataloguedata.Id).FirstOrDefault();
                    var catitemtodelete = context.CatalogueItems.Where(a => a.catalogueId == cataloguedata.Id).ToList();
                    List<CatalogueItemsModel> items = cataloguedata.catalogueitems;

                    if (catalogue == null)
                    {
                        return BadRequest("Invalid Id");
                    }
                    else
                    {
                        catalogue.name = cataloguedata.name;
                        catalogue.Dateupdated = DateTime.Now;
                        catalogue.Updatedby = "currentuser";
                        catalogue.numberOfItems = cataloguedata.numberOfItems;

                        context.Catalogues.Update(catalogue).Property(x => x.Id).IsModified = false;

                        foreach (var itemtodelete in catitemtodelete)
                        {
                            context.CatalogueItems.Attach(itemtodelete);
                            context.CatalogueItems.Remove(itemtodelete);
                        }

                        foreach (var item in items)
                        {
                            CatalogueItemsModel catalogueitemmodel = new CatalogueItemsModel();
                            catalogueitemmodel = item;
                            catalogueitemmodel.Guid = catalogue.Guid;
                            catalogueitemmodel.catalogueId = catalogue.Id;

                            context.CatalogueItems.Add(catalogueitemmodel);
                        }
                        await context.SaveChangesAsync();
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}

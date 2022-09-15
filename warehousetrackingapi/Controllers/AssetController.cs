using Microsoft.AspNetCore.Mvc;
using TrackingDemoApi;
using warehousetrackingapi.Models;
using System.Web;
namespace warehousetrackingapi.Controllers
{
    [Route("asset")]
    public class AssetController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }
        
        
        [HttpGet]
        [Route("getassets")]
        public List<AssetModelDTO> Get()
        {
            
             using (var context = new Context())
            {
                var query = (from a in context.Assets
                             join b in context.Types
                             on a.Type equals b.Guid
                             join c in context.Categories
                             on a.Category equals c.Guid
                             select new AssetModelDTO
                             {
                                 Tag = a.Tag,
                                 Category = c.Category,
                                 Manufacturer = a.Manufacturer,
                                 Model = a.Model,
                                 Description = a.Description,
                                 Active =a.Active,
                                 Createdby =a.Createdby,
                                 Datecreated =a.Datecreated,    
                                 Updatedby =a.Updatedby,    
                                 Dateupdated =a.Dateupdated,
                                 Type = b.Type,
                                 Itemname = a.Itemname,
                                 Id = a.Id,
                                 Guid = a.Guid,
                                 Status = a.Status,

                             }).ToList();

                return query;
            }
        }


        [HttpGet]
        [Route("GetNotAllowed")]
        public List<AssetModel> GetNotAllowed()
        {

            using (var context = new Context())
            {
                var query =  context.Assets.Where(a=>a.IsAllowedToGoOut==true).ToList();

                return query;
            }
        }


        [HttpPost]
        [Route("UploadAssetPicture")]
        public async void UploadAssetPicture([FromForm] string tag, IFormFile  file)
        {
            var form = Request.Form;

            await using (var context = new Context())
            {
            //    Random random = new Random();
            //    string ran = random.Next(1000, 9999).ToString();
            //    var asset = context.Assets.Where(a => a.Tag == Tag).FirstOrDefault();
            //    if (asset != null)
            //    {

            //        if (file != null)
            //        {
            //            asset.AssetImage = asset.Tag + "_" + ran + Path.GetExtension(file.FileName);
            //            asset.AssetImagePath = "AssetPictures";
            //        }
            //        else
            //        {
            //            asset.AssetImage = "";
            //            asset.AssetImagePath = "";
            //        }
            //        context.Assets.Update(asset);

            //        await context.SaveChangesAsync();


            //        if (file != null)
            //        {
            //            var path = Path.Combine(asset.AssetImagePath, asset.AssetImage);

            //            using (var stream = new FileStream(path, FileMode.Create))
            //            {
            //                await file.CopyToAsync(stream);
            //            }
            //        }

            //    }
            }
        }

        [HttpGet]
        [Route("getassetbyid")]
        public async Task<AssetModel> GetAssetById(string Id)
        {
            await using (var context = new Context())
            {
                return context.Assets.Where(a => a.Guid.ToString()==Id).FirstOrDefault();
                
            }
        }


        [HttpGet]
        [Route("getassetbytag")]
        public async Task<AssetModel> getassetbytag(string Id)
        {
            await using (var context = new Context())
            {
                return context.Assets.Where(a => a.Tag==Id).FirstOrDefault();

            }
        }

        [HttpPost]
        [Route("addasset")]
        public async Task<ActionResult> AddAsset([FromBody] AssetModel assetModel)
        {
            await using(var context = new Context())
            {
                Guid guid = Guid.NewGuid();
                assetModel.Guid = guid;
                assetModel.Dateupdated = DateTime.Now;
                assetModel.Updatedby = "currentuser";
                assetModel.Datecreated = DateTime.Now;
                assetModel.Createdby = "currentuser";
                context.Assets.Add(assetModel);
                await context.SaveChangesAsync();
                return Ok();
            }
            
        }

        [HttpPost]
        [Route("addbulkasset")]
        public async Task<ActionResult> AddBulkAsset([FromBody] AssetModel assetModel)
        {
            try
            {
                await using (var context = new Context())
                {
                            Guid guid = Guid.NewGuid();
                            assetModel.Tag = assetModel.Tag;
                            assetModel.Guid = guid;
                            assetModel.Dateupdated = DateTime.Now;
                            assetModel.Updatedby = "currentuser";
                            assetModel.Datecreated = DateTime.Now;
                            assetModel.Createdby = "currentuser";
                            assetModel.Status = "Checkin";
                            context.Assets.Add(assetModel);
                            await context.SaveChangesAsync();
                            assetModel.Id = 0;
                    return Ok();
                }
            }catch (Exception ex) {
                return Ok();
            }

        }

        [HttpPost]
        [Route("updateasset")]
        public async Task<ActionResult> UpdateAsset([FromBody] AssetModel assetModel)
        {
            try
            {
                await using (var context = new Context())
                {
                    var asset = context.Assets.Where(a => a.Guid == assetModel.Guid).FirstOrDefault();

                    if (asset == null)
                    {
                        return BadRequest("Invalid Id");
                    }
                    else
                    {
                        asset.Dateupdated = DateTime.Now;
                        asset.Updatedby = "currentuser";
                        asset.Active = assetModel.Active;
                        asset.Category = assetModel.Category;
                        asset.Description = assetModel.Description;
                        asset.Itemname = assetModel.Itemname;
                        asset.IsAllowedToGoOut = assetModel.IsAllowedToGoOut;
                        asset.Type = assetModel.Type;
                        asset.Manufacturer = assetModel.Manufacturer;
                        asset.Model = assetModel.Model;
                        asset.Tag = assetModel.Tag;

                        context.Assets.Update(asset).Property(x => x.Id).IsModified = false;
                        await context.SaveChangesAsync();
                        return Ok();
                    }
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
    }
}

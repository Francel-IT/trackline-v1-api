using Microsoft.AspNetCore.Mvc;
using System.Net;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{
    [Route("tag")]
    public class TagController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        

        

        [HttpPost]
        [Route("savewarehousemoveout")]
        public async Task<ActionResult> SaveWarehouseMoveOut([FromBody] List<String> Tag)
        {
            await using (var context = new Context())
            {
                foreach (var item in Tag)
                {
                    LogModel logsModel = new LogModel();
                    logsModel.Tag = item;
                    logsModel.Dateupdated = DateTime.Now;
                    logsModel.Updatedby = "currentuser";
                    logsModel.Datecreated = DateTime.Now;
                    logsModel.Createdby = "currentuser";
                    logsModel.Mode = "1";
                    context.Logs.Add(logsModel);

                    var asset = context.Assets.Where(a => a.Tag == item && a.Active=="Active").FirstOrDefault();
                    if (asset != null)
                    {
                        //asset.Status = 1;
                        context.Assets.Update(asset).Property(x => x.Id).IsModified = false; 
                    }

                    await context.SaveChangesAsync();
                }

                return Ok();
            }

        }

        //This function change the item location and status by getting the reader location and action (checkin/checkout)
        [HttpPost]
        [Route("changeitemlocationstatus")]
        public async Task<ActionResult> ChangeItemLocationStatus([FromBody] List<TagModelDTO> Tag) 
        {
            await using (var context = new Context())
            {
                foreach (var item in Tag)
                {
                    var readerconfig = (from a in context.ReaderConfig
                                 from b in context.Location
                                 where a.Location == b.Guid && a.IpAddress == item.Ipaddress && a.Antenna==item.Antenna
                             select new ReaderConfigModelDTO
                             {
                                 Reader = a.Reader,
                                 Action =a.Action,
                                 Location = b.Guid,
                                 LocationName = b.Location

                             }).FirstOrDefault();

                    if (readerconfig != null)
                    {
                        var asset = context.Assets.Where(a => a.Tag == item.Tag && a.Active == "Active").FirstOrDefault();
                        if (asset != null)
                        {
                            asset.Status = readerconfig.LocationName + " " + readerconfig.Action;
                            asset.Location = readerconfig.Location;
                            context.Assets.Update(asset).Property(x => x.Id).IsModified = false; ;
                        }

                        LogModel logsModel = new LogModel();
                        logsModel.Tag = item.Tag;
                        logsModel.Dateupdated = DateTime.Now;
                        logsModel.Updatedby = "currentuser";
                        logsModel.Datecreated = DateTime.Now;
                        logsModel.Createdby = "currentuser";
                        logsModel.Mode = readerconfig.Reader + " " + readerconfig.Action;
                        context.Logs.Add(logsModel);

                        await context.SaveChangesAsync();
                    }
                }

                return Ok();
            }

        }


        [HttpPost]
        [Route("binchangeitemlocationstatus")]
        public async Task<ActionResult> BinChangeItemLocationStatus([FromBody] List<TagModelDTO> Tag)
        {
            await using (var context = new Context())
            {
                foreach (var item in Tag)
                {
                    var readerconfig = (from a in context.ReaderConfig
                                        from b in context.ORSession
                                        from c in context.Location
                                        where a.Location == c.Guid && a.Id == b.Reader && b.Status == "Open" && a.IpAddress == item.Ipaddress && a.Antenna == item.Antenna && a.Action=="BIN"
                                        select new ORSessionModelDTO
                                        {
                                            Id = b.Id,
                                            ReaderConfigId = b.Reader,
                                            Location = c.Guid,
                                            LocationName = c.Location

                                        }).FirstOrDefault();

                    if (readerconfig != null)
                    {
                        var asset = context.Assets.Where(a => a.Tag == item.Tag && a.Active == "Active").FirstOrDefault();
                        if (asset != null)
                        {
                            asset.Status = readerconfig.LocationName + " " + readerconfig.Action;
                            asset.Location = readerconfig.Location;
                            asset.IsConsumed = true;
                            context.Assets.Update(asset).Property(x => x.Id).IsModified = false; 
                            await context.SaveChangesAsync();
                        }

                        var oritems =  context.ORSessionItems.Where(a=>a.ORSessionId == readerconfig.Id && a.Tag==item.Tag).FirstOrDefault();
                        if (oritems == null)
                        {
                            ORSessionItemsModel orItems = new ORSessionItemsModel();
                            Guid guid = Guid.NewGuid();
                            orItems.Tag = item.Tag;
                            orItems.Datecreated = DateTime.Now;
                            orItems.ORSessionId = readerconfig.Id;
                            context.ORSessionItems.Add(orItems);
                            await context.SaveChangesAsync();
                        }
                    }
                }

                return Ok();
            }

        }

        [HttpGet]
        [Route("searchTagByRfid")]
        public async Task<TagModel> SearchTagByRfid(string Tag)
        {
            await using (var context = new Context())
            {
                var query = new TagModel();

                query = (from a in context.Assets
                         from t in context.Types.Where(w => w.Guid == a.Type).DefaultIfEmpty()
                         from c in context.Categories.Where(w => w.Guid == a.Category).DefaultIfEmpty()
                         where a.Tag == Tag
                         select new TagModel
                         {
                             Tag = a.Tag,
                             TagType = "Asset",
                             Name = a.Itemname,
                             Type = t.Type,
                             Category = c.Category,
                             Manufacturer = a.Manufacturer,
                             Model = a.Model,
                             Guid = a.Guid,
                             Status = a.Status,
                             Id = a.Id

                         }).FirstOrDefault();

                //if (query == null)
                //{
                //    query = (from e in context.Employees
                //             where e.Tag == Tag
                //             select new TagModel
                //             {
                //                 Tag = e.Tag,
                //                 TagType = "Employee Id",
                //                 Name = e.Firstname + " " + e.Lastname,
                //                 Type = "",
                //                 Category = "",
                //                 Manufacturer = "",
                //                 Model = "",
                //                 Guid = e.Guid,
                //                 Id = e.Id

                //             }).FirstOrDefault();
                //}

                if (query == null)
                {
                    query = new TagModel();
                    query.TagType = "";
                    query.Name = "Unrecognized";
                }


                return query;
            }
        }


        [HttpGet]
        [Route("isTrucK")]
        public IActionResult isTrucK(string Tag)
        {
             using (var context = new Context())
            {
                HttpResponseMessage r = new HttpResponseMessage();
                var query = new TagModel();

                query = (from a in context.Assets
                         from t in context.Types.Where(w => w.Guid == a.Type).DefaultIfEmpty()
                         where a.Tag == Tag && t.Type=="Truck"
                         select new TagModel
                         {
                             Tag = a.Tag,
                             TagType = "Asset",
                             Name = a.Itemname,
                             Type = t.Type,
                             Manufacturer = a.Manufacturer,
                             Model = a.Model,
                             Guid = a.Guid,
                             Id = a.Id

                         }).FirstOrDefault();


                if (query == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
                  
            }
        }

       

        [HttpGet]
        [Route("isEmployeeTag")]
        public IActionResult IsEmployeeTag(string Tag)
        {
            using (var context = new Context())
            {
                HttpResponseMessage r = new HttpResponseMessage();

                var query = context.Employees.Where(a => a.Tag == Tag).FirstOrDefault();

                if (query == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }

            }
        }


        [HttpGet]
        [Route("searchtagsinout")]
        public async Task<IEnumerable<LogModelDTO>> searchTagsInOut()
        {
            await using (var context = new Context())
            {

                var query = (from l in context.CheckInOutTransactions
                         from a in context.Assets.Where(w => w.Tag == l.Tag).DefaultIfEmpty()
                         from t in context.Types.Where(w => w.Guid == a.Type).DefaultIfEmpty()
                         from c in context.Categories.Where(w => w.Guid == a.Category).DefaultIfEmpty()
                         from e in context.Employees.Where(w=> w.Tag == l.Employee).DefaultIfEmpty()
                         select new LogModelDTO
                         {
                             Tag = l.Tag,
                             Itemname = a.Itemname,
                             Type = t.Type,
                             Category = c.Category,
                             Manufacturer = a.Manufacturer,
                             Model = a.Model,
                             Mode = l.Mode,
                             Employee = e.Firstname+" "+e.Lastname,
                             Datecreated=l.Datecreated

                         }).ToList().OrderByDescending(a=>a.Datecreated);


                return query;
            }
        }

        [HttpGet]
        [Route("checkTagIfExist")]
        public async Task<TagModel> CheckTagIfExist(string Tag, Guid Id)
        {
            await using (var context = new Context())
            {
                var query = new TagModel();

                query = (from a in context.Assets
                         where a.Tag == Tag
                         select new TagModel
                         {
                             Tag = a.Tag,
                             TagType = "Asset",
                             Name = a.Itemname,
                             Guid = a.Guid,
                             Id = a.Id

                         }).FirstOrDefault();

                if(query == null)
                {
                    query = (from a in context.Employees
                             where a.Tag == Tag
                             select new TagModel
                             {
                                 Tag = a.Tag,
                                 TagType = "EmployeeId",
                                 Name = a.Firstname,
                                 Guid = a.Guid,
                                 Id = a.Id

                             }).FirstOrDefault();
                }

                if (query == null)
                {
                    query = new TagModel();
                    query.Tag = "";
                }
                else if (Id == Guid.Empty)
                {
                    query.Tag = "";
                }
                else
                {
                    if (query.Guid == Id)
                    {
                        query.Tag = "";
                    }
                    else
                    {

                    }
                }


                return query;
            }
        }
    }
}

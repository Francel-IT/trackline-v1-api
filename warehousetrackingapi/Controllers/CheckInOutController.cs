using Microsoft.AspNetCore.Mvc;
using TrackingDemoApi.Models;
using warehousetrackingapi.Models;

namespace TrackingDemoApi.Controllers
{
    [Route("CheckInOut")]
    public class CheckInOutController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("SaveCheckOut")]
        public async Task<ActionResult> SaveCheckOut([FromBody] List<String> Tag)
        {
            await using (var context = new Context())
            {
                string EmployeeTag = Tag[0];
                Tag.RemoveAt(0);
                foreach (var item in Tag)
                {
                    Int64 TransactionNo = context.Logs.Max(a => a.TransactionNo);
                    if (TransactionNo <= 0)
                    {
                        TransactionNo = 1;
                    }
                    else
                    {
                        TransactionNo++;
                    }
                    CheckInOutModel inoutModel = new CheckInOutModel();
                    inoutModel.Tag = item;
                    inoutModel.Dateupdated = DateTime.Now;
                    inoutModel.Updatedby = "currentuser";
                    inoutModel.Datecreated = DateTime.Now;
                    inoutModel.Createdby = "currentuser";
                    inoutModel.Mode = "Checkout";
                    inoutModel.TransactionNo = TransactionNo;
                    inoutModel.Employee = EmployeeTag;
                    context.CheckInOutTransactions.Add(inoutModel);
                    var asset = context.Assets.Where(a => a.Tag == item).FirstOrDefault();
                    if (asset != null)
                    {
                        asset.Status = "Checkout";
                        context.Assets.Update(asset).Property(x => x.Id).IsModified = false;
                    }

                    await context.SaveChangesAsync();
                }

                return Ok();
            }
        }

        [HttpGet]
        [Route("TransactionReport")]
        public async Task<IEnumerable<CheckInOutModelDTO>> TransactionReport()
        {

            //return (from a in context.WorkOrders
            //        from b in context.WorkOrderItems.Where(x => x.WorkOrderId == a.Workorderid).DefaultIfEmpty()
            //        from c in context.Employees.Where(x => x.Employeeid == a.EmployeeId).DefaultIfEmpty()
            //        select new WorkOrderModelDTO


            await using (var context = new Context())
            {
                var report = (from a in context.CheckInOutTransactions
                              from e in context.Employees.Where(x => x.Tag == a.Employee).DefaultIfEmpty()
                              group a by new
                              {
                                  a.TransactionNo,
                                  e.Firstname,
                                  e.Lastname,
                                  a.Mode,
                              } into b
                              select new CheckInOutModelDTO()
                              {
                                  TransactionNo = b.Key.TransactionNo.ToString(),
                                  Employee = b.Key.Firstname+" "+b.Key.Lastname,
                                  Mode = b.Key.Mode,
                                  ItemCount = b.Count()
                              }
                                    ).Distinct().ToList().OrderByDescending(a=>a.TransactionNo);
                return report;
              

            }
        }



        [HttpPost]
        [Route("SaveTransaction")]
        public async Task<ActionResult> SaveTransaction([FromBody] List<string> Tag)
        {

            await using (var context = new Context())
            {
                Int64 TransactionNo;
                try
                {

                    TransactionNo = context.CheckInOutTransactions.Max(a => a.TransactionNo);
                    if (TransactionNo <= 0)
                    {
                        TransactionNo = 1;
                    }
                    else
                    {
                        TransactionNo++;
                    }
                }
                catch
                {
                    TransactionNo = 1;
                }
                var EmployeeTag = "";
                List<string> ListOfEmployees = new List<string>(); 
                foreach (var item in Tag)
                {
                    var i = item.Split(",");
                    var res = context.Employees.Where(a => a.Tag == i[0]).FirstOrDefault();
                    if (res != null)
                    {
                        EmployeeTag = i[0];
                        ListOfEmployees.Add(i[0]);

                    }
                }
                foreach (var item in Tag)
                {
                    var i = item.Split(",");
                    if (i[1] == "Ant1" || i[1] == "Ant3" || i[1] == "Ant4")
                    {
                        var status = "";
                        var t = i[0];
                        CheckInOutModel inoutModel = new CheckInOutModel();
                       
                        if (!ListOfEmployees.Contains(i[0]))
                        {
                            if (i[1] == "Ant1")
                            {
                                status = "Checkin";
                            }
                            if (i[1] == "Ant3" || i[1] == "Ant4")
                            {
                                status = "Checkout";
                            }
                            if (status != "") { 
                            var checkStatus = context.Assets.Where(a => a.Tag == i[0]).FirstOrDefault();
                                if (checkStatus != null)
                                {
                                    if (checkStatus.Status != status)
                                    {
                                        inoutModel.Tag = i[0];
                                        inoutModel.Dateupdated = DateTime.Now;
                                        inoutModel.Updatedby = "currentuser";
                                        inoutModel.Datecreated = DateTime.Now;
                                        inoutModel.Createdby = DateTime.Now.ToString("dd-MMM-yyyy");


                                        inoutModel.TransactionNo = TransactionNo;
                                        inoutModel.Mode = status;
                                        inoutModel.Employee = EmployeeTag;
                                        context.CheckInOutTransactions.Add(inoutModel);
                                        var asset = context.Assets.Where(a => a.Tag == i[0] ).FirstOrDefault();
                                        if (asset != null )
                                        {
                                                asset.Status = status;
                                                context.Assets.Update(asset).Property(x => x.Id).IsModified = false;
                                        }


                                       
                                        var wo = context.WorkOrderItems.Where(a => a.Tag==t && a.WOStatus == "Open").FirstOrDefault();
                                        if (wo != null)
                                        {
                                            wo.ItemStatus = status;
                                            context.WorkOrderItems.Update(wo).Property(x => x.Id).IsModified = false;
                                           if (status == "Checkout")
                                           {
                                                var workorder = context.WorkOrders.Where(a => a.Workorderid == wo.WorkOrderId).FirstOrDefault();
                                                if (workorder != null)
                                                {
                                                    workorder.LastCheckOut = DateTime.Now;
                                                    context.WorkOrders.Update(workorder).Property(x => x.Workorderid).IsModified = false;
                                                }
                                            }

                                        }
                                       

                                        await context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                }

                return Ok();

            }
        }

        [HttpGet]
        [Route("getworkorderitemscheckout")]
        public async Task<IEnumerable<AssetModelDTO>> GetWorkOrderItemsCheckOut()
        {
            await using (var context = new Context())
            {
                var query = (from w in context.WorkOrders
                             join a in context.WorkOrderItems
                             on w.Workorderid equals a.WorkOrderId
                             join b in context.Assets
                             on a.Tag equals b.Tag
                             join c in context.Types
                             on b.Type equals c.Guid
                             join d in context.Categories
                             on b.Category equals d.Guid
                             where DateTime.Now.AddSeconds(-30) <= w.LastCheckOut
                             select new AssetModelDTO
                             {
                                 Tag = a.Tag,
                                 Category = d.Category,
                                 Manufacturer = b.Manufacturer,
                                 Model = b.Model,
                                 Description = b.Description,
                                 Active = b.Active,
                                 Type = c.Type,
                                 Itemname = b.Itemname,
                                 Id = a.WorkOrderId,
                                 Status = a.ItemStatus,
                                 Datecreated=a.DateCheckout



                             }).ToList().OrderBy(a=>a.Id);

                return query;

            }
        }

        [HttpGet]
        [Route("getTransactionItems")]
        public async Task<IEnumerable<AssetModelDTO>> getTransactionItems(int Id,string Status)
        {
            await using (var context = new Context())
            {
                var query = (from a in context.CheckInOutTransactions
                             join b in context.Assets
                             on a.Tag equals b.Tag
                             join c in context.Types
                             on b.Type equals c.Guid
                             join d in context.Categories
                             on b.Category equals d.Guid
                             where a.TransactionNo == Id && a.Mode==Status
                             select new AssetModelDTO
                             {
                                 Tag = a.Tag,
                                 Category = d.Category,
                                 Manufacturer = b.Manufacturer,
                                 Model = b.Model,
                                 Description = b.Description,
                                 Active = b.Active,
                                 Type = c.Type,
                                 Datecreated = b.Datecreated,
                                 Itemname = b.Itemname,
                                 Id = a.Id,
                                 Status = b.Status

                             }).ToList().OrderByDescending(a=>a.Datecreated);

                return query;

            }
        }

        [HttpGet]
        [Route("getLatestTransactionItems")]
        public async Task<IEnumerable<AssetModelDTO>> getLatestTransactionItems()
        {
            await using (var context = new Context())
            {
                var query = (from a in context.CheckInOutTransactions
                             join b in context.Assets
                             on a.Tag equals b.Tag
                             join c in context.Types
                             on b.Type equals c.Guid
                             join d in context.Categories
                             on b.Category equals d.Guid
                             from e in context.Employees.Where(x => x.Tag == a.Employee).DefaultIfEmpty()
                             where a.Datecreated >= DateTime.Now.AddMinutes(-200)
                             select new AssetModelDTO
                             {
                                 Tag = a.Tag,
                                 Category = d.Category,
                                 Manufacturer = b.Manufacturer,
                                 Model = b.Model,
                                 Description = b.Description,
                                 Active = b.Active,
                                 Employee = e.Firstname + " " + e.Lastname,
                                 Type = c.Type,
                                 Datecreated = a.Datecreated,
                                 Itemname = b.Itemname,
                                 Id = a.Id,
                                 Status = a.Mode
                             }).ToList().OrderByDescending(a => a.Datecreated);

                return query;

            }
        }

        [HttpGet]
        [Route("getDashboardCount")]
        public async Task<DashboardModel> getDashboardCount()
        {
            DashboardModel d= new DashboardModel();
            await using (var context = new Context())
            {
                d.ItemOutToday =context.CheckInOutTransactions.Where(a=>a.Datecreated>DateTime.Now.AddDays(-1) && a.Mode=="Checkout").Count().ToString();
                d.ItemINToday = context.CheckInOutTransactions.Where(a => a.Datecreated > DateTime.Now.AddDays(-1) && a.Mode == "Checkin").Count().ToString();
                d.ItemOutTotal = context.Assets.Where(a => a.Status == "Checkout").Count().ToString();
                d.ItemINTotal = context.Assets.Where(a => a.Status == "Checkin").Count().ToString();
                d.AssignedItem = context.WorkOrderItems.Where(a => a.ItemStatus == "Assigned").Count().ToString();
                d.ActiveWorkorder = context.WorkOrderItems.Where(a => a.ItemStatus !="Checkin").GroupBy(a=>a.WorkOrderId).Count().ToString();
                d.OneWeekOut = context.WorkOrderItems.Where(a => a.ItemStatus == "Checkout" && DateTime.Now.AddDays(-7)> a.DateCheckout).Count().ToString();
                return d;

            }
        }

        [HttpPost]
        [Route("SaveCheckIn")]
        public async Task<ActionResult> SaveCheckIn([FromBody] List<String> Tag)
        {
            await using (var context = new Context())
            {
                string EmployeeTag = Tag[0];
                Tag.RemoveAt(0);
                foreach (var item in Tag)
                {
                    Int64 TransactionNo = context.Logs.Max(a => a.TransactionNo);
                    if (TransactionNo <= 0)
                    {
                        TransactionNo = 1;
                    }
                    else
                    {
                        TransactionNo++;
                    }
                    CheckInOutModel inoutModel = new CheckInOutModel();
                    inoutModel.Tag = item;
                    inoutModel.Dateupdated = DateTime.Now;
                    inoutModel.Updatedby = "currentuser";
                    inoutModel.Datecreated = DateTime.Now;
                    inoutModel.Createdby = "currentuser";
                    inoutModel.Mode = "Checkin";
                    inoutModel.TransactionNo = TransactionNo;
                    inoutModel.Employee = EmployeeTag;
                    context.CheckInOutTransactions.Add(inoutModel);
                    var asset = context.Assets.Where(a => a.Tag == item).FirstOrDefault();
                    if (asset != null)
                    {
                        asset.Status = "Checkin";
                        context.Assets.Update(asset).Property(x => x.Id).IsModified = false;
                    }

                    await context.SaveChangesAsync();
                }

                return Ok();
            }

        }
    }

}

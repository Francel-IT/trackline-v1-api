using Microsoft.AspNetCore.Mvc;
using System.Linq;
using warehousetrackingapi.Models;

namespace warehousetrackingapi.Controllers
{
    [Route("workorder")]
    public class WorkOrderController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getworkorders")]
        public async Task<IEnumerable<WorkOrderModelDTO>> GetWorkOrders()
        {
            await using (var context = new Context())
            {
                return (from a in context.WorkOrders
                        from b in context.WorkOrderItems.Where(x => x.WorkOrderId == a.Workorderid).DefaultIfEmpty()
                        from c in context.Employees.Where(x => x.Employeeid == a.EmployeeId).DefaultIfEmpty()
                        from d in context.Catalogues.Where(x => x.Id.ToString() == a.CatalogueId).DefaultIfEmpty()
                        select new WorkOrderModelDTO
                        {
                            Workorderid = a.Workorderid,
                            Description = a.Description,
                            Workorderdate = a.Workorderdate,
                            Status = a.Status,
                            Employeeid = a.EmployeeId,
                            Employeename = c.Firstname + " " + c.Lastname,
                            CatalogueId = a.CatalogueId,
                            Cataloguename = d.name
                        }).Distinct().ToList();


            }
        }


        [HttpGet]
        [Route("getworkorderbyemployeeid")]
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrdersByEmployeeId(string Id)
        {
            await using (var context = new Context())
            {
                return context.WorkOrders.Where(a=>a.EmployeeId== Id).ToList().OrderByDescending(a=>a.Datecreated);


            }
        }

        


        [HttpPost]
        [Route("assignworkorderitems")]
        public async Task<ActionResult> AssignWorkOrderItems([FromBody] WorkOrderItemsDTO Assets)
        {
            await using (var context = new Context())
            {
                if (Assets != null)
                {
                    var Tags = Assets.Tag.Split(',');

                    foreach (var item in Tags)
                    {
                        if (item != "")
                        {
                            var WOI = context.WorkOrderItems.Where(a => a.Tag == item && a.WorkOrderId == Assets.Workorderid).FirstOrDefault();
                            if (WOI != null)
                            {
                                WOI.ItemStatus = "Assigned";
                                context.WorkOrderItems.Update(WOI).Property(x => x.Id).IsModified = false;
                            }
                            else
                            {

                                WorkOrderItemsModel items = new WorkOrderItemsModel();
                                items.Tag = item;
                                items.WorkOrderId = Assets.Workorderid;
                                items.EmployeeId = Assets.Employeeid;
                                items.WOStatus = "Open";
                                //items.Location = Assets.Location;
                                items.ItemStatus = "Assigned";
                                items.DateCheckout = DateTime.Now;

                                context.WorkOrderItems.Add(items);

                                // update status in asset table for validation purpose
                                var itemid = context.Assets.Where(a => a.Tag == item).FirstOrDefault();
                                if (itemid != null)
                                {
                                    itemid.Status = "Assigned";
                                    context.Assets.Update(itemid).Property(x => x.Id).IsModified = false;
                                }
                            }

                            await context.SaveChangesAsync();
                        }
                    }
                }

                return Ok();
            }
        }

        [HttpPost]
        [Route("saveworkordercheckout")]
        public async Task<ActionResult> SaveWorkOrderCheckout([FromBody] WorkOrderItemsDTO Assets)
        {
            await using (var context = new Context())
            {
                if (Assets != null)
                {
                    var Tags = Assets.Tag.Split(',');

                    foreach (var item in Tags)
                    {
                        if (item != "")
                        {
                            var itemWO = context.WorkOrderItems.Where(a => a.Tag == item && a.ItemStatus == "Assigned" && a.WorkOrderId == Assets.Workorderid).FirstOrDefault();
                            // item was already assigned to the work order 
                            if (itemWO != null)
                            {
                                itemWO.ItemStatus = "Checkout";
                                itemWO.DateCheckout = DateTime.Now;
                                itemWO.EmployeeId = Assets.Employeeid;
                                context.WorkOrderItems.Update(itemWO).Property(x => x.Id).IsModified = false;

                                var itemid = context.Assets.Where(a => a.Tag == item).FirstOrDefault();
                                if (itemid != null)
                                {
                                    itemid.Status = "Checkout";
                                    context.Assets.Update(itemid).Property(x => x.Id).IsModified = false;
                                }
                            }
                            else
                            {
                                // item was added during checkout process 
                                var itemAdd = context.Assets.Where(a => a.Tag == item && a.Status == "Checkin").FirstOrDefault();
                                if (itemAdd != null)
                                {
                                        // add the item to current work order items
                                        WorkOrderItemsModel itemToAdd = new WorkOrderItemsModel();
                                        itemToAdd.Tag = item;
                                        itemToAdd.WorkOrderId = Assets.Workorderid;
                                        itemToAdd.EmployeeId = Assets.Employeeid;
                                        itemToAdd.WOStatus = "Open";
                                        //itemToAdd.Location = Assets.Location;
                                        itemToAdd.ItemStatus = "Checkout";
                                        itemToAdd.DateCheckout = DateTime.Now;

                                        context.WorkOrderItems.Add(itemToAdd);

                                        // change the status of item in Asset table as well
                                        var itemToAddid = context.Assets.Where(a => a.Tag == item).FirstOrDefault();
                                        if (itemToAddid != null)
                                        {
                                            itemToAddid.Status = "Checkout";
                                            context.Assets.Update(itemToAddid).Property(x => x.Id).IsModified = false;
                                        }
                                    
                                }

                            }

                            await context.SaveChangesAsync();
                        }
                    }
                }
                return Ok();

            }
        }

        [HttpPost]
        [Route("saveworkordercheckin")]
        public async Task<ActionResult> SaveWorkOrderCheckin([FromBody] WorkOrderItemsDTO Assets)
        {
            await using (var context = new Context())
            {
                var Tags = Assets.Tag.Split(',');

                foreach (var item in Tags)
                {
                    if (item != "")
                    {
                        var itemWO= context.WorkOrderItems.Where(a => a.Tag == item && a.ItemStatus=="Checkout").FirstOrDefault();
                        if (itemWO != null)
                        {
                            itemWO.ItemStatus = "Checkin";
                            itemWO.DateReturned = DateTime.Now;
                            itemWO.ReturnedBy = Assets.Employeeid;
                            context.WorkOrderItems.Update(itemWO).Property(x => x.Id).IsModified = false;

                            var itemid = context.Assets.Where(a => a.Tag == item).FirstOrDefault();
                            if (itemid != null)
                            {
                                itemid.Status = "Checkin";
                                context.Assets.Update(itemid).Property(x => x.Id).IsModified = false;
                            }
                        }

                        

                        await context.SaveChangesAsync();
                    }
                }

                return Ok();
            }
        }

        [HttpGet]
        [Route("getworkorderitems")]
        public async Task<IEnumerable<AssetModelDTO>> GetWorkOrderItems(int Id)
        {
                await using (var context = new Context())
                {
                var query = (from a in context.WorkOrderItems
                             join b in context.Assets
                             on a.Tag equals b.Tag
                             join c in context.Types
                             on b.Type equals c.Guid
                             join d in context.Categories
                             on b.Category equals d.Guid
                             where a.WorkOrderId == Id
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
                                 Id = a.Id,
                                 Status = a.ItemStatus

                             }).ToList();

                
                    return query;

                }
        }


       


        [HttpPost]
        [Route("addworkorder")]
        public async Task<ActionResult> AddWorkOrder([FromBody]  WorkOrderModelDTO workOrderDTOModel)
        {
            try
            {
                await using (var context = new Context())
                {
                    //WorkOrderItemsModel workOrderItemsModel;
                    WorkOrderModel workOrderModel = new WorkOrderModel();

                    workOrderModel.Status = "Open";
                    workOrderModel.Description = workOrderDTOModel.Description;
                    workOrderModel.EmployeeId = workOrderDTOModel.Employeeid;
                    workOrderModel.Workorderdate = Convert.ToDateTime(workOrderDTOModel.Workorderdate);
                    workOrderModel.CatalogueId = workOrderDTOModel.CatalogueId;
                    workOrderModel.Dateupdated = DateTime.Now;
                    workOrderModel.Updatedby = "currentuser";
                    workOrderModel.Datecreated = DateTime.Now;
                    workOrderModel.Createdby = "currentuser";



                    context.WorkOrders.Add(workOrderModel);
                    await context.SaveChangesAsync();

                    var id = workOrderModel.Workorderid;



                    //foreach (WorkOrderItemsDTO a in assetModel.Assets)
                    //{

                    //    workOrderItemsModel = new WorkOrderItemsModel();
                    //    workOrderItemsModel.Tag = a.Tag;
                    //    workOrderItemsModel.AssetId = a.Guid;
                    //    workOrderItemsModel.ItemStatus = "";
                    //    workOrderItemsModel.WorkOrderId = id;
                    //    workOrderItemsModel.WOStatus = "Open";

                    //    context.WorkOrderItems.Add(workOrderItemsModel);
                    //    await context.SaveChangesAsync();
                    //}

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [Route("closeworkorder")]
        public async Task<ActionResult> CloseWorkOrder(int Id) 
        {
            await using (var context = new Context())
            {
                var workOrder = context.WorkOrders.Where(a => a.Workorderid == Id && a.Status == "Open").FirstOrDefault();
                if (workOrder != null)
                {
                    var workOrderItems = context.WorkOrderItems.Where(b => b.WorkOrderId == Id).ToList();
                    
                    if (workOrderItems.All(item => item.ItemStatus == "Checkin" || item.ItemStatus == "Assigned"))
                    {
                        workOrderItems.ForEach(item => { item.WOStatus = "Closed"; 
                            context.WorkOrderItems.Update(item).Property(x => x.Id).IsModified = false; });
                        
                        workOrder.Status = "Closed";
                        context.WorkOrders.Update(workOrder).Property(x => x.Workorderid).IsModified = false;

                        await context.SaveChangesAsync();

                        return Ok();
                    }
                }
                return BadRequest("Work Order cannot be closed!");
            }
        }
    }
}

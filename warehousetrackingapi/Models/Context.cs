using Microsoft.EntityFrameworkCore;
using TrackingDemoApi.Models;

namespace warehousetrackingapi.Models
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options) { }
        public virtual DbSet<AssetModel> Assets { get; set; }
        public virtual DbSet<TypeModel> Types { get; set; }
        public virtual DbSet<CategoryModel> Categories { get; set; }

        public virtual DbSet<EmployeeModel> Employees { get; set; }

        public virtual DbSet<WorkOrderItemsModel> WorkOrderItems { get; set; }

        public virtual DbSet<WorkOrderModel> WorkOrders { get; set; }

        public virtual DbSet<ReaderModel> Readers { get; set; }

        public virtual DbSet<StationModel> Stations { get; set; }
        public virtual DbSet<LogModel> Logs { get; set; }

        public virtual DbSet<ReaderConfigModel> ReaderConfig { get; set; }

        public virtual DbSet<LocationModel> Location { get; set; }

        public virtual DbSet<ORSessionModel> ORSession { get; set; }

        public virtual DbSet<ORSessionItemsModel> ORSessionItems { get; set; }

        public virtual DbSet<CheckInOutModel> CheckInOutTransactions { get; set; }
        public virtual DbSet<CatalogueModel> Catalogues { get; set; }
        public virtual DbSet<CatalogueItemsModel> CatalogueItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=ls\sqlexpress;trusted_connection=true;initial catalog=TrackingDemo;MultipleActiveResultSets=True;App=EntityFramework;User ID=NVS123;Password=nvs123");
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class WorkOrderModel: CreationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Workorderid { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public DateTime Workorderdate { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string EmployeeId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string WorkOrderType { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Status { get; set; }
        public DateTime LastCheckOut { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string CatalogueId { get; set; }

    }

    public class WorkOrderModelDTO
    {
        public DateTime Workorderdate { get; set; }
        public string Description { get; set; }
        public string Employeeid { get; set; }
        public string Employeename { get; set; }
        public int Workorderid { get; set; }
        public string Status { get; set; }
        public string CatalogueId { get; set; }

        public string Cataloguename { get; set; }

    }
}

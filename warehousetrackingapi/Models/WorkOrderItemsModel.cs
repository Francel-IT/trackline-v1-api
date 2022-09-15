using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class WorkOrderItemsModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int WorkOrderId { get; set; }


        public Guid AssetId { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Tag { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string ItemStatus { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string WOStatus { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string EmployeeId { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string ReturnedBy { get; set; }

        public DateTime DateCheckout { get; set; }
        public DateTime DateReturned { get; set; }

    }


  
    public class WorkOrderItemsDTO
    {
        public string Tag { get; set; }
        public Guid Guid { get; set; }
        public Guid Location { set; get; }
        public string Employeeid { set; get; }
        public int Workorderid { get; set; }


    }
}

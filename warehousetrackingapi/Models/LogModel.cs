using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class LogModel :CreationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Tag { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Mode { get; set; }

        public string Employee { get; set; }

        public Int64 TransactionNo { get; set; }

    }

    public class LogModelDTO : CreationModel
    {
        public string Tag { get; set; }

        public string Mode { get; set; }


        public string Itemname { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }


        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string Active { get; set; }

        public string Employee { get; set; }
    }
}

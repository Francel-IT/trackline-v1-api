using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class AssetModel : CreationModel

    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Tag { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Itemname { get; set; }

        public Guid? Type { get; set; }

        public Guid? Category { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Manufacturer { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Model { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Active { get; set; }

        public string Status { get; set; }

        public Guid Location { get; set; }

        public bool IsConsumed { get; set; }

        public bool IsAllowedToGoOut { get; set; }

        public string AssetImage { get; set; }

        public string AssetImagePath { get; set; }

    }

    public class AssetModelDTO : CreationModel

    {

        public int Id { get; set; }

        public Guid Guid { get; set; }


        public string Tag { get; set; }

        public string Itemname { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }


        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string Active { get; set; }

        public string Status { get; set; }

        public string Employee { get; set; }
        public bool IsAllowedToGoOut { get; set; }
    }
}

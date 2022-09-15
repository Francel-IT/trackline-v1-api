using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class LocationModel : CreationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Location { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Active { get; set; }

    }
}

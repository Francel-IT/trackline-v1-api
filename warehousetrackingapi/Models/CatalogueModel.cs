using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class CatalogueModel : CreationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string name { get; set; } // catalogue name

        [Column(TypeName = "int")]
        public int numberOfItems { get; set; } 

        

    }
}

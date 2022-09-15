using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class CatalogueItemsModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }

        [Column(TypeName = "int")]
        public int catalogueId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string assetType { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string assetGuid { get; set; }

        [Column(TypeName = "int")]
        public int quantity { get; set; }
    }

    
}

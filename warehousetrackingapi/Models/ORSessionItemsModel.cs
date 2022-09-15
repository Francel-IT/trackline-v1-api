using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class ORSessionItemsModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        public int ORSessionId { get; set; }

        public string Tag { get; set; }

        public int AssetId { get; set; }
        public DateTime Datecreated { get; set; }
    }
}

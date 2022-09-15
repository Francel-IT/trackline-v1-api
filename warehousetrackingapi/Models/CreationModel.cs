using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace warehousetrackingapi.Models
{
    public class CreationModel
    {
        public DateTime Datecreated { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Createdby { get; set; }

        public DateTime Dateupdated { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Updatedby { get; set; }
    }
}

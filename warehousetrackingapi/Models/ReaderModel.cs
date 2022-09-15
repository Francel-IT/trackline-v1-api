using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class ReaderModel : CreationModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Readername { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Ipaddress { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Port { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Active { get; set; }
    }
}

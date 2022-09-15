using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class ORSessionModel : CreationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string ORNo { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string PatientNo { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Status { get; set; }

        public int Reader { get; set; }

    }


    public class ORSessionModelDTO
    {
        public int Id { get; set; }


        public int ReaderConfigId { get; set; }

        public Guid Location { get; set; }

        public string Action { get; set; }

        public string LocationName { get; set; }

    }
}

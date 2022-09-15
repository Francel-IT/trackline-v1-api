using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class EmployeeModel : CreationModel
    {

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Firstname { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Middlename { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Lastname { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Email { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Employeeid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Tag { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Active { get; set; }
    }
}

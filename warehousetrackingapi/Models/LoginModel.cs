using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TrackingDemoApi.Models
{
    public class LoginModel
    {
        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Password { get; set; }
    }
}

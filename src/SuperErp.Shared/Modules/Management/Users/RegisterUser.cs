using System.ComponentModel.DataAnnotations;

namespace SuperErp.Management.Model
{
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int DelegationId { get; set; }

        public DateTime StartDate { get; set; }
    }
}

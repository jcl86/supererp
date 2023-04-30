using System.ComponentModel.DataAnnotations;

namespace SuperErp.Management.Model
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}

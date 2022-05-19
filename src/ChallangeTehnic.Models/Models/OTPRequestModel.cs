using System.ComponentModel.DataAnnotations;

namespace ChallangeTehnic.Models.Models
{
    public class OTPRequestModel
    {
        [Required]
        [StringLength(6)]
        [Display(Name = "Cod conectare: ")]
        public string OTP { get; set; } = String.Empty;
        public string? OTPCurent { get; set; } = String.Empty;

        public DateTime OTPCreatLaDataUTC { get; set; }
        public decimal UtilizatorId { get; set; }
    }
}
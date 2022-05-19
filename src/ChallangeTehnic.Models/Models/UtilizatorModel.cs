using System.ComponentModel.DataAnnotations;

namespace ChallangeTehnic.Models.Models
{
    /// <summary>
    /// Folosim o clasa simpla. Pentru ca nu folosim o baza de date.
    /// </summary>
    public class UtilizatorModel
    {
        [Required]
        public decimal Id { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string? Nume { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public bool EsteLogat { get; set; }

        public OTPModel? OTP { get; set; }

    }
}
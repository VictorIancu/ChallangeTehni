using System.ComponentModel.DataAnnotations;

namespace ChallangeTehnic.Models.ViewModels
{
    /// <summary>
    /// Este primul ecran care se afiseaza utilizatorului. 
    /// De obicei... un cod OTP se solicita dupa ce autentificarea 
    /// simpla a avut loc cu succes.
    /// de aceea solicitam un utilizator si o parola, pentru a putea trece la solicitarea 
    /// unui cod OTP.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        public string? Utilizator { get; set; }

        [Required]
        public string? Parola { get; set; }
    }
}
namespace ChallangeTehnic.Models.Models
{
    public class OTPModel
    {
        public OTPModel(string otPassword)
        {
            OTPassword = otPassword;
        }

        public OTPModel(string otPassword, bool succes) : this(otPassword)
        {
            OTPGeneratCuSucces = succes;
        }
        public string? OTPassword { get; init; }
        public DateTime? OTPCreatLaDataUTC { get; set; }
        public bool OTPGeneratCuSucces { get; init; } = false;
    }
}
using System.Security.Cryptography;
using System.Text;
using ChallangeTehnic.Models.Models;

namespace ChallangeTehnic.Services.Servicii
{
    /*
        https://datatracker.ietf.org/doc/html/rfc6238#section-5.2
        https://datatracker.ietf.org/doc/html/rfc4226#section-5.4

        Un alt link interesant
        https://www.geeksforgeeks.org/one-time-password-otp-algorithm-in-cryptography/

    */
    public sealed class EncryptionService
    {
        private static readonly IDepozit _depozit = new Depozit();
        private static DateTime UNIX_EPOCH => new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static UtilizatorModel ObtineParolaOTP(UtilizatorModel utilizator)
        {
            if (utilizator is null) throw new ArgumentNullException(nameof(utilizator));
            UtilizatorModel u = _depozit.ObtineUtilizatorCuId(utilizator.Id);
            if (u is null) throw new Exception("Resursa indisponibila, reveniti mai tarziu");
            u.EsteLogat = utilizator.EsteLogat;
            DateTime utcNow = DateTime.UtcNow;
            return ObtineParolaOTP(u, utcNow); // utilizator
        }


        public static UtilizatorModel ObtineParolaOTP(UtilizatorModel utilizator, DateTime utcNow)
        {
            if (utilizator is null) throw new ArgumentNullException(nameof(utilizator));
            long iteratie = ObtineIteratie(utcNow);
            OTPModel otpModel = GenerazaParolaOTPDinUtilizatorSiIteratie(utilizator, iteratie);
            otpModel.OTPCreatLaDataUTC = utcNow;
            utilizator.OTP = otpModel;
            return utilizator; // utilizator
        }



        public static bool ValideazaParolaOTP(OTPRequestModel requestModel)
        {
            if (requestModel is null) throw new ArgumentNullException(nameof(requestModel));
            UtilizatorModel u = _depozit.ObtineUtilizatorCuId(requestModel.UtilizatorId);
            if (u is null) throw new Exception(nameof(u));
            UtilizatorModel otpModel = ObtineParolaOTP(u, requestModel.OTPCreatLaDataUTC);
            if (otpModel?.OTP?.OTPassword == requestModel.OTP)
                return true;
            return false;
        }

        /// <summary>
        ///    https://datatracker.ietf.org/doc/html/rfc4226#section-5.4
        ///    Codul este preluat din exemplul de la sectiunea 5.4
        ///    precum si
        ///    https://returnsmart.blogspot.com/2014/03/test_10.html
        /// </summary>
        /// <returns></returns>
        private static OTPModel GenerazaParolaOTPDinUtilizatorSiIteratie(UtilizatorModel utilizator, long iterationnumber, int digits = 6)
        {
            // Se converteste iterationNumber in byte[]
            byte[] iterationNumberByte = BitConverter.GetBytes(iterationnumber);

            if (BitConverter.IsLittleEndian) Array.Reverse(iterationNumberByte);
            string nume = utilizator.Nume ?? "Acest sir de caractere ar trebui sa fie secret";
            // HMAC-SHA1 algorithm (Hashed Message Authentication Code)
            byte[] userIdByte = Encoding.ASCII.GetBytes(nume);
            HMACSHA1 userIdHMAC = new(userIdByte);
            byte[] hash = userIdHMAC.ComputeHash(iterationNumberByte);
            //RFC4226 http://tools.ietf.org/html/rfc4226#section-5.4
            int offset = hash[^1] & 0xf; //0xf = 15d
            int binary =
            ((hash[offset] & 0x7f) << 24)      //0x7f = 127d
            | ((hash[offset + 1] & 0xff) << 16) //0xff = 255d
            | ((hash[offset + 2] & 0xff) << 8)
            | (hash[offset + 3] & 0xff);

            int password = binary % (int)Math.Pow(10, digits);
            string passwordString = password.ToString(new string('0', digits));
            bool succes = passwordString.Length == 6;
            return new OTPModel(passwordString, succes);
        }

        private static long ObtineIteratie(DateTime utcNow)
        {
            return (long)(utcNow - UNIX_EPOCH).TotalSeconds;
        }
    }


}
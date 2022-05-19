namespace ChallangeTehnic.Tests.ServiciiTests
{
    using ChallangeTehnic.Models.Models;
    using ChallangeTehnic.Services.Servicii;


    public class ServiciiTests
    {
        [Fact]
        public void EncryptionService_ObtineParolaOTP_DacaGasesteUtilizatorulInBazaDeDate()
        {
            // Arrange
            UtilizatorModel victor = new()
            {
                Id = 1,
                Nume = "victoriancu",
                Email = "ceva@ceva.ceva"
            };

            // Act
            UtilizatorModel victorCuOTP = EncryptionService.ObtineParolaOTP(victor);
            int OTPPassLenght = victorCuOTP?.OTP?.OTPassword?.Length ?? 0;
            // Assert
            Assert.NotNull(victorCuOTP!.OTP);
            Assert.Equal(6, OTPPassLenght);
        }

        [Fact]
        public void EncryptionService_ObtineParolaOTP_GenereazaAcelsiOTP_CuAcelesiDate()
        {
            //Arrange
            DateTime timpUTCAcum = DateTime.UtcNow;
            UtilizatorModel victor = new()
            {
                Id = 1,
                Nume = "victoriancu",
                Email = "ceva@ceva.ceva"
            };
            //Act
            UtilizatorModel u1 = EncryptionService.ObtineParolaOTP(victor, timpUTCAcum);
            UtilizatorModel u2 = EncryptionService.ObtineParolaOTP(victor, timpUTCAcum);
            string? otp1 = u1?.OTP?.OTPassword;
            string? otp2 = u2?.OTP?.OTPassword;
            //Assert
            Assert.NotNull(otp1);
            Assert.NotNull(otp2);
            Assert.Equal(otp1, otp2);
        }

        [Fact]
        public void EncryptionService_ObtineParolaOTP_GenereazaOTPDiferit_CuTimpDiferit()
        {
            // Arrange
            DateTime timpUTCAcum = DateTime.UtcNow;
            DateTime TimpUTCMinusOZi = DateTime.UtcNow.AddDays(-1);
            UtilizatorModel victor = new()
            {
                Id = 1,
                Nume = "victoriancu",
                Email = "ceva@ceva.ceva"
            };
            // Act
            UtilizatorModel uCuTimpUtcAcum = EncryptionService.ObtineParolaOTP(victor, timpUTCAcum);
            string? otp1 = uCuTimpUtcAcum?.OTP?.OTPassword;
            UtilizatorModel uCuTimpUtcMinuOZi = EncryptionService.ObtineParolaOTP(victor, TimpUTCMinusOZi);
            string? otp2 = uCuTimpUtcMinuOZi?.OTP?.OTPassword;
            // Assert
            Assert.NotNull(otp1);
            Assert.NotNull(otp2);
            Assert.NotSame(otp1, otp2);


        }

        [Fact]
        public void EncryptionService_ObtineParolaOTP_GenereazaOTPDiferit_CuNumeUtilizatorDiferit()
        {
            // Arrange
            DateTime timpUTCAcum = DateTime.UtcNow;
            UtilizatorModel victor = new()
            {
                Id = 1,
                Nume = "victoriancu",
                Email = "ceva@ceva.ceva"
            };

            UtilizatorModel george = new()
            {
                Id = 2,
                Nume = "george_george"
            };
            // Act
            UtilizatorModel v = EncryptionService.ObtineParolaOTP(victor, timpUTCAcum);
            UtilizatorModel g = EncryptionService.ObtineParolaOTP(george, timpUTCAcum);
            string? otp1 = v?.OTP?.OTPassword;
            string? otp2 = g?.OTP?.OTPassword;
            // Assert
            Assert.NotNull(otp1);
            Assert.NotNull(otp2);
            Assert.NotSame(otp1, otp2);
        }
    }
}
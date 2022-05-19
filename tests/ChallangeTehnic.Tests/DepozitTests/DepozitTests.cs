namespace ChallangeTehnic.Tests.DepozitTests
{
    using ChallangeTehnic.Models.Models;
    using ChallangeTehnic.Services;
    public class DepozitTests
    {
        [Fact]
        public void Depozit_Are_Un_UtilizatorIn_Lista()
        {
            // Arrange
            IDepozit depozit = new Depozit();
            //Act 
            int count = depozit.Utilizatori().Count;
            //Assert
            Assert.Equal(1, count);
        }

        [Fact]
        public void Depozit_Creaza_Utilizator()
        {
            //Arrange
            IDepozit depozit = new Depozit();
            UtilizatorModel u = new()
            {
                Id = 2,
                Nume = "Test",
                Email = "test@text.test"
            };
            // Act
            depozit.CreazaUtilizator(u);
            int count = depozit.Utilizatori().Count;
            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void Depozit_ultimulId_inLista_Este_2()
        {
            //Arrange
            IDepozit depozit = new Depozit();
            UtilizatorModel u = new()
            {
                Id = 2,
                Nume = "Test",
                Email = "test@text.test"
            };
            depozit.CreazaUtilizator(u);
            //Act
            decimal ultimulIdDinLista = depozit.Utilizatori().OrderBy(x => x.Id).Last().Id;
            // Assert
            Assert.Equal(2, ultimulIdDinLista);
        }

        [Fact]
        public void Depozit_Actualizeaza_Utilizator()
        {
            //Arrange
            IDepozit depozit = new Depozit();
            UtilizatorModel u = depozit.ObtineUtilizatorCuId(1);
            //Act
            string numeNou = "TestTest";
            u.Nume = numeNou;
            depozit.ActualizeazaUtilizator(u);
            UtilizatorModel utilizatorActualizat = depozit.ObtineUtilizatorCuId(1);
            // Assert
            Assert.Equal(numeNou, utilizatorActualizat.Nume);

        }

        [Fact]
        public void Depozit_Sterge_Utilizator()
        {
            //Arrange
            IDepozit depozit = new Depozit();
            UtilizatorModel u = depozit.Utilizatori().First();
            //Act
            depozit.StergeUtilizator(u);
            int count = depozit.Utilizatori().Count;
            // Assert  
            Assert.Equal(0, count);
        }
    }
}
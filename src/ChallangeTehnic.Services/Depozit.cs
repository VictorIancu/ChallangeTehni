using ChallangeTehnic.Models.Models;

namespace ChallangeTehnic.Services
{
    public class Depozit : IDepozit
    {
        private readonly List<UtilizatorModel> _utilizatori;
        public Depozit()
        {
            _utilizatori = new List<UtilizatorModel>
            {
                new UtilizatorModel
                {
                    Id = 1,
                    Nume = "victoriancu",
                    Email = "ceva@ceva.ceva"
                }
            };
        }

        public void ActualizeazaUtilizator(UtilizatorModel utilizator)
        {
            if (utilizator is null) throw new ArgumentNullException(nameof(utilizator));
            UtilizatorModel existent = ObtineUtilizatorCuId(utilizator.Id);
            if (existent is null) throw new ArgumentException("resursa solicitata nu este disponibila in acest moment", nameof(utilizator));
            StergeUtilizator(existent);
            existent.Nume = utilizator.Nume;
            existent.Email = utilizator.Email;
            _utilizatori.Add(existent);
        }

        public void CreazaUtilizator(UtilizatorModel utilizator)
        {
            if (utilizator is null) throw new ArgumentNullException(nameof(utilizator));
            decimal ultimulId = _utilizatori.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();
            utilizator.Id = ultimulId + 1;
            _utilizatori.Add(utilizator);
        }

        public UtilizatorModel ObtineUtilizatorCuId(decimal id)
        {
            if (id < 1) throw new ArgumentOutOfRangeException(nameof(id));
            if (id > _utilizatori.Count) throw new ArgumentOutOfRangeException(nameof(id));
            return _utilizatori.Where(x => x.Id == id).FirstOrDefault()!; // verificam pentru  null la utilizare
        }

        public UtilizatorModel ObtineUtilizatorCuNume(string? nume)
        {
            if (string.IsNullOrEmpty(nume)) throw new ArgumentNullException(nameof(nume));
            return _utilizatori.Where(x => x.Nume!.Contains(nume)).FirstOrDefault()!; // verificam pentru null la utilizare
        }

        public void StergeUtilizator(UtilizatorModel utilizator)
        {
            if (utilizator is null) throw new ArgumentNullException(nameof(utilizator));
            _utilizatori.Remove(utilizator);
        }

        public List<UtilizatorModel> Utilizatori()
        {
            return _utilizatori ?? new List<UtilizatorModel>();
        }
    }
}
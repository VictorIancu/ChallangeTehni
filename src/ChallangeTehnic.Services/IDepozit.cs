using ChallangeTehnic.Models.Models;

namespace ChallangeTehnic.Services
{
    public interface IDepozit
    {
        List<UtilizatorModel> Utilizatori();
        UtilizatorModel ObtineUtilizatorCuId(decimal id);
        UtilizatorModel ObtineUtilizatorCuNume(string? nume);

        void CreazaUtilizator(UtilizatorModel utilizator);
        void ActualizeazaUtilizator(UtilizatorModel utilizator);
        void StergeUtilizator(UtilizatorModel utilizator);

    }
}
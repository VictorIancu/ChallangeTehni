using ChallangeTehnic.Models.ViewModels;
using ChallangeTehnic.Models.Models;


namespace ChallangeTehnic.Services.Servicii
{
    /// <summary>
    /// Folosim aceasta clasa pentru validarea si logarea utilizatorului.
    /// Renuntam la inregistrarea utilizatorului sau functia "rememberMe".
    /// </summary>
    public class LoginService
    {
        private readonly IDepozit _depozit = new Depozit();
        public RaspunsModel<UtilizatorModel> Login(LoginViewModel viewModel)
        {
            RaspunsModel<UtilizatorModel> raspuns = new();
            // Validam doar simpla existenta a unui input.
            if (viewModel.Utilizator != null && viewModel.Parola != null)
            {

                UtilizatorModel u = _depozit.ObtineUtilizatorCuNume(viewModel.Utilizator);
                if (u is null) throw new Exception(nameof(u));
                u.EsteLogat = true;
                raspuns.Data = u;
                raspuns.HttpStatusCode = 200;
                raspuns.Mesaj = "Bine ai venit!";
                return raspuns;
            }
            raspuns.HttpStatusCode = 500;
            raspuns.Mesaj = "Utilizator sau parola gresita!";
            return raspuns;
        }

        public UtilizatorModel ObtineUtilizatorCuId(decimal id)
        {
            return _depozit.ObtineUtilizatorCuId(id);
        }
    }
}
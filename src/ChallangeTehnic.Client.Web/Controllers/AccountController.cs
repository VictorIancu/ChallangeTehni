using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChallangeTehnic.Client.Web.Models;
using ChallangeTehnic.Models.ViewModels;
using ChallangeTehnic.Services.Servicii;
using ChallangeTehnic.Models.Models;

namespace ChallangeTehnic.Client.Web.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LoginService _loginService;


    public AccountController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _loginService = new LoginService();

    }

    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // POST 
    [HttpPost]
    public IActionResult Index(LoginViewModel model)
    {
        const string Mesaj = "A intervenit o problema, te rog incearca mai tarziu!";
        if (ModelState.IsValid)
        {
            RaspunsModel<UtilizatorModel> loginResult = _loginService.Login(model);
            if (loginResult.HttpStatusCode >= 200)
            {
                return RedirectToAction("ObtineUserOTP", loginResult.Data);
            }
            ModelState.AddModelError("", loginResult.Mesaj ?? Mesaj);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ObtineUserOTP(UtilizatorModel utilizator)
    {
        if (utilizator is null || utilizator.EsteLogat == false) return RedirectToAction("Index"); // Ne intoarcem la index daca nu avem un utilizator e o problema undeva si
                                                                                                   // nu lasam utilizatorul sa mearga mai departe.
        try
        {
            UtilizatorModel utilizatorCuOTP = EncryptionService.ObtineParolaOTP(utilizator);
            if (utilizatorCuOTP.OTP is not null && utilizatorCuOTP.OTP.OTPGeneratCuSucces)
            {
                ViewData["Utilizator"] = utilizatorCuOTP;
                OTPRequestModel m = new OTPRequestModel();
                m.OTPCreatLaDataUTC = utilizatorCuOTP.OTP.OTPCreatLaDataUTC ?? DateTime.UtcNow;
                m.OTPCurent = utilizatorCuOTP.OTP.OTPassword;
                m.UtilizatorId = utilizatorCuOTP.Id;
                return View(m);
            }
        }

        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        // 
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ObtineUserOTP(OTPRequestModel viewModel)
    {
        bool valid = EncryptionService.ValideazaParolaOTP(viewModel);
        if (valid) return RedirectToAction("Index", "Home");
        // suntem aici atunci validarea a esuat. cel mai probabil codul a fost gresit
        // introdus de utilizator. 
        // Rugam utilizatorul sa reintroduca un nou cod.
        UtilizatorModel u = _loginService.ObtineUtilizatorCuId(viewModel.UtilizatorId);
        // aici stim ca utilizatorul a trecut de prima etapa a autentificarii.
        u.EsteLogat = true;
        return RedirectToAction("ObtineUserOTP", u);
    }


    public IActionResult VerificaUtilizator(decimal? utilizatorId)
    {
        if (utilizatorId == null) return RedirectToAction("Index");
        UtilizatorModel u = _loginService.ObtineUtilizatorCuId(utilizatorId.Value);
        if (u is null) return RedirectToAction("Index");
        // De principiu folosesc un cookie care sa imi spuna cum este utilizatorul
        u.EsteLogat = true;
        return RedirectToAction("ObtineUserOTP", u);
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

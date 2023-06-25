using System.ComponentModel.DataAnnotations;
using ecommerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecommerce.Pages
{
    public class Login : PageModel
    {
        public class DadosLogin
        {
            [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
            [EmailAddress]
            [Display(Name = "E-mail")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Senha { get; set; }

            [Display(Name = "Lembrar de mim")]
            public bool Lembrar { get; set; }
        }

        private readonly SignInManager<AppUser> _signInManager;

        public Login(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public DadosLogin Dados { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string MensagemDeErro { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(MensagemDeErro))
            {
                ModelState.AddModelError(string.Empty, MensagemDeErro);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // elimina o cookie anterior para garantir um processo de login novo
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            Console.WriteLine(Dados.Email);
            Console.WriteLine(Dados.Senha);

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Dados.Email, Dados.Senha, Dados.Lembrar, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                     Console.WriteLine("passou aqui");
                    return Page();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
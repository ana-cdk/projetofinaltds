using ecommerce.Data;
using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ecommerce.Pages
{
    public class NovoCliente : PageModel
    {
        public class Senhas{
            [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
            [StringLength(16, ErrorMessage = "O campo \"{0}\" deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Senha { get; set; }

            [Required(ErrorMessage = "O campo \"{0}\" é de preenchimento obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmação da Senha")]
            [Compare("Senha", ErrorMessage = "A confirmação da senha não confere com a senha informada.")]
            public string ConfirmacaoSenha { get; set; }
        }

        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public ClienteModel Cliente { get; set; }
        
        [BindProperty]
        public Senhas SenhasUsuario { get; set; }

        public NovoCliente(AppDbContext context,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
			//cria um novo objeto Cliente
			var cliente = new ClienteModel();
			cliente.Endereco = new EnderecoModel();

			//novos clientes sempre iniciam com essa situação
			cliente.Situacao = ClienteModel.SituacaoCliente.Cadastrado;

			var senhasUsuario = new Senhas();
			
			if (await TryUpdateModelAsync(senhasUsuario, senhasUsuario.GetType(), nameof(senhasUsuario))){
				return Page();
			}
            
			//tenta atualizar o novo objeto com os dados oriundos do formulário
			if (!await TryUpdateModelAsync(cliente, Cliente.GetType(), nameof(Cliente)))
			{
				//verifica se o perfil de usuário "cliente" existe
				if (!await _roleManager.RoleExistsAsync("cliente"))
				{
					await _roleManager.CreateAsync(new IdentityRole("cliente"));
				}

				//verifica se já existe um usuário com o e-mail informado
				var usuarioExistente = await _userManager.FindByEmailAsync(cliente.Email);
				if (usuarioExistente != null)
				{
					//adiciona um erro na propriedade Email do cliente para que o campo apresente o erro no formulário
					ModelState.AddModelError("Cliente.Email", "Já existe um cliente cadastrado com este e-mail.");
					return Page();
				}

				//cria o objeto usuário Identity e adiciona ao perfil "cliente"
				var usuario = new AppUser()
				{
					UserName = cliente.Email,
					Email = cliente.Email,
					PhoneNumber = cliente.Telefone,
					Nome = cliente.Nome
				};

				var result = await _userManager.CreateAsync(usuario, senhasUsuario.Senha);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(usuario, "cliente");

					//adiciona o novo objeto cliente ao contexto de banco de dados atual e salva no banco de dados
					_context.Cliente.Add(cliente);
					int afetados = await _context.SaveChangesAsync();
					//se salvou o cliente no banco de dados
					if (afetados > 0)
					{

						return RedirectToPage("/CadastroRealizado");
					}
					else
					{
						//exclui o usuário Identity criado
						await _userManager.DeleteAsync(usuario);
						ModelState.AddModelError("Cliente", "Não foi possível efetuar o cadastro. Verifique os dados " +
							"e tente novamente. Se o problema persistir, entre em contato conosco.");
						return Page();
					}
				}
				else
				{
					ModelState.AddModelError("Cliente.Email", "Não foi possível criar um usuário com este endereço de e-mail. " +
						"Use outro endereço de e-mail ou tente recuperar a senha deste.");
				}
			}
            
			return Page();
		}
	}
}
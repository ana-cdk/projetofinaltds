using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ecommerce.Pages.Cliente
{
    [Authorize(Policy = "isAdmin")]
    public class Incluir : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ClienteModel Cliente { get; set; }

        public Incluir(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cliente = new ClienteModel();
            cliente.Endereco = new EnderecoModel();
            //novos clientes sempre iniciam com essa situação
            cliente.Situacao = ClienteModel.SituacaoCliente.Cadastrado;

                _context.Cliente.Add(Cliente);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Listar");

        }

    }
}
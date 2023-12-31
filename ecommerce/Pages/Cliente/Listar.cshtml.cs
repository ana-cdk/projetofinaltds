using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Pages.Cliente
{
    //[Authorize(Policy = "isAdmin")]
    public class Listar : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IList<string> EmailsAdmins { get; set;}
        public IList<ClienteModel> Clientes { get; set; }

        public Listar(AppDbContext context, UserManager<AppUser> userManager,
         RoleManager<IdentityRole> roleManager)
        {
           this._context = context;
           this._userManager = userManager;
           this._roleManager = roleManager;
        }
        public async Task OnGetAsync()
        {
            EmailsAdmins = (await _userManager.GetUsersInRoleAsync("admin"))
            .Select(x => x.Email).ToList();
            Clientes = await _context.Cliente.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);

           if (cliente != null)
            {
                AppUser usuario = await _userManager.FindByNameAsync(cliente.Email);
                if (usuario != null)
                {
                    if (!await _roleManager.RoleExistsAsync("admin"))
                        await _roleManager.CreateAsync(new IdentityRole("admin"));

                    await _userManager.AddToRoleAsync(usuario, "admin");
                }
            }

            return RedirectToPage("./Listar");
        }
        public async Task<IActionResult> OnPostDelAdminAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);

           if (cliente != null)
            {
               AppUser usuario = await _userManager.FindByNameAsync(cliente.Email);
               if (usuario != null)
               {
                await _userManager.RemoveFromRoleAsync(usuario, "admin");
               }
            }

            return RedirectToPage("./Listar");
        }

        public async Task<IActionResult> OnPostSetAdminAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);

           if (cliente != null)
            {
               AppUser usuario = await _userManager.FindByNameAsync(cliente.Email);
               if (usuario != null)
               {
                    if(!await _roleManager.RoleExistsAsync("admin"))
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        
                    await _userManager.AddToRoleAsync(usuario, "admin");
               }
            }

            return RedirectToPage("./Listar");
        }
    }
}
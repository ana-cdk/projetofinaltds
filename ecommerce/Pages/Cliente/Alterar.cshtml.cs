using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ecommerce.Pages.Cliente
{
    [Authorize(Policy = "isAdmin")]
    public class Alterar : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public ClienteModel Cliente { get; set; }

        public Alterar(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.IdCliente == id);
            if (Cliente == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cliente = await _context.Cliente.Select(m => new {m.IdCliente, m.Email, m.CPF}).FirstOrDefaultAsync(m => m.IdCliente == Cliente.IdCliente);
            Cliente.Email = cliente.Email;
            Cliente.CPF = cliente.CPF;
            

            if (ModelState.Keys.Contains("Cliente.Email"))
            {
                ModelState["Cliente.Email"].Errors.Clear();
                ModelState.Remove("Cliente.Email");
            }
            if (ModelState.Keys.Contains("Cliente.CPF"))
            {
                ModelState["Cliente.CPF"].Errors.Clear();
                ModelState.Remove("Cliente.CPF");
            }
           
            _context.Attach(Cliente).State = EntityState.Modified;
            _context.Attach(Cliente.Endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteAindaExiste(Cliente.IdCliente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return RedirectToPage("./Listar");
        }
    private bool ClienteAindaExiste(int id)
    {
       return _context.Cliente.Any(m => m.IdCliente == id);
    }
    
    }
}
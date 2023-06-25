using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models
{
    [Owned]
    public class EnderecoModel
    {
        [MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? Logradouro { get; set; }

        [Display(Name = "Número")]
        [MaxLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? Numero { get; set; }

        [MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? Complemento { get; set; }

        [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? Bairro { get; set; }


        [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? Cidade { get; set; }

        [MaxLength(2, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? Estado { get; set;}

        [RegularExpression(@"[0-9]{8}$", ErrorMessage = "O campo {0} deve ser preenchido com um CEP válido")]
        [MaxLength(8, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        public string? CEP { get; set; }
        
        [MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres")]
        [Display(Name = "Referência")]
        public string? Referencia { get; set; }
    }
}
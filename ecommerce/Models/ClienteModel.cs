using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models
{
    public class ClienteModel
    {
        public enum SituacaoCliente
        {
            Bloqueado,
            Cadastrado,
            Aprovado,
            Especial
        }
        [Key]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(100, ErrorMessage ="O campo {0} deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo {0} deve conter uma data válida")]
        [DisplayName("Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [RegularExpression(@"[0-9]{11}$", ErrorMessage = "O campo {0} deve ser preenchido com 11 digítos numéricos")]
        [UIHint("_CpfTemplate")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression(@"([0-9]){11}$", ErrorMessage = "O campo \"{0}\" deve ser preenchido com 11 dígitos numéricos.")]
        [MaxLength(11, ErrorMessage = "O campo \"{0}\" deve conter no máximo {1} caracteres.")]
        [MinLength(11, ErrorMessage = "O campo \"{0}\" deve conter no mínimo {1} caracteres.")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "O campo {0} deve conter um endereço de e-mail válido")]
        [MaxLength(50, ErrorMessage ="O campo {0} deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Situação")]
        public SituacaoCliente Situacao { get; set; }
        public EnderecoModel Endereco { get; set; }
        public ICollection<PedidoModel> Pedidos { get; set; }
    }
}
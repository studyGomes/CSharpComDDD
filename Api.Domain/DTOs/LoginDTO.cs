using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é um campo Obrigatório para Login")]
        [EmailAddress(ErrorMessage = "Formato do Email Inválido")]
        [StringLength(100,ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }

        
    }
}

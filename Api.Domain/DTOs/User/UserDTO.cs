using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.User
{
    public class UserDTO
    {
        [Required (ErrorMessage = "Nome é Campo Obrigatório")]
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name{ get ; set;}
        
        [Required (ErrorMessage = "Email é Campo Obrigatório")]
        [EmailAddress (ErrorMessage = "Email em Formato Inválido")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Email{ get ; set;}
    }
}

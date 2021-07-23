using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.User
{
    // DTO para receber as informações pela Controller
    public class UserDTOCreate
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

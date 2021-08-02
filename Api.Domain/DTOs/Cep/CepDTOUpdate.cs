using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.Cep
{
    public class CepDTOUpdate
    {
        [Required(ErrorMessage = "Id de Cep é Campo Obrigatório")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Cep é Campo Obrigatório")]
        [MaxLength(10)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é Campo Obrigatório")]
        [MaxLength(60)]
        public string Logradouro { get; set; }        
        
        public string Numero { get; set; }

        [Required(ErrorMessage = "Municipio é Campo Obrigatório")]
        public Guid MunicipioId{ get; set;}        
    }
}

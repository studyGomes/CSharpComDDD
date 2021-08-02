using System;

namespace Api.Domain.DTOs.Cep
{
    public class CepDTOUpdateResult
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }        
        
        public string Numero { get; set; }

        public Guid MunicipioId{ get; set;}

        public DateTime UpdateAt { get; set; }          
    }
}

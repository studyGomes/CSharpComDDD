using System;

namespace Api.Domain.DTOs.Cep
{
    public class CepDTOCreateResult
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }        
        
        public string Numero { get; set; }

        public Guid MunicipioId{ get; set;}

        public DateTime CreateAt { get; set; }  
               
    }
}

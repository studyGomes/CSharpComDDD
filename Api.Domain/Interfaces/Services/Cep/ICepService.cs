using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDTO> Get (Guid id) ; 
        Task<CepDTO> Get (string cep) ; 
        Task<CepDTOCreateResult> Post (CepDTOCreate cep) ;
        Task<CepDTOUpdateResult> Put (CepDTOUpdate cep) ;
        Task<bool> Delete (Guid id) ;         
    }
}

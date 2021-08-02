using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.Municipio;

namespace Api.Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {

        Task<MunicipioDTO> Get (Guid id) ; 
        Task<MunicipioDTOCompleto>  GetCompleteById ();
        Task<MunicipioDTOCompleto>  GetCompleteByIBGE (int codIBGE);
        Task<IEnumerable<MunicipioDTO>> GetAll ();
        Task<MunicipioDTOCreateResult> Post (MunicipioDTOCreate municipio) ;
        Task<MunicipioDTOUpdateResult> Put (MunicipioDTOUpdate municipio) ;
        Task<bool> Delete (Guid id) ;          

    }
}

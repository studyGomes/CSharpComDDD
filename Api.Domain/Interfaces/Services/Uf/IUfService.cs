using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.Uf;

namespace Api.Domain.Interfaces.Services.Uf
{
    public interface IUfService
    {
        Task<UfDTO> Get (Guid id) ; 
        Task<IEnumerable<UfDTO>>  GetAll ();
        /*
        Task<UfDTOCreateResult> Post (UfDTOCreate uf) ;
        Task<UfDTOUpdateResult> Put (UfDTOUpdate uf) ;
        Task<bool> Delete (Guid id) ;    
        */     
    }
}

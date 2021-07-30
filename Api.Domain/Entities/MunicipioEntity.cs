using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class MunicipioEntity
    {
        [Required]
        [MaxLength(60)]
        public string Nome { get; set; }
        public string CodIBGE { get; set; }

        [Required]
        public Guid UfId { get; set; }
        public UfEntity Uf { get; set; }

        public IEnumerable<CepEntity> Ceps {get;set;}
    }
}

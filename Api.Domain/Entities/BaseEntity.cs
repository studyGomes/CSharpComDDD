using System;
using System.ComponentModel.DataAnnotations;

// Entidade Padrao para outras classes
namespace Api.Domain.Entities
{
    // Base de outra classe, abstract
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        private DateTime? _createAt;
        public DateTime? CreateAt
        {
            get { return _createAt; }
            set { _createAt = (value==null? DateTime.UtcNow : value ) ; }
        }
        
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        
        

    }
}

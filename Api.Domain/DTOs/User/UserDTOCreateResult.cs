using System;

namespace Api.Domain.DTOs.User
{
    // DTO para retornar as informações iniciadas pela Controller
    public class UserDTOCreateResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}

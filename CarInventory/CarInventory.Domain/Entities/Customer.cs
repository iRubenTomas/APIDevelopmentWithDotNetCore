﻿
namespace CarInventory.Domain.Entities
{
    public class Customer : BaseIdentity
    {     
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

using PhonkChief.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhonkChief.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public byte[] Avatar { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfReg { get; set; }
        public List<Loop> Loops { get; set; }
    }
}

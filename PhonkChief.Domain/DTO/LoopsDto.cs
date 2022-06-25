using PhonkChief.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhonkChief.Domain.DTO
{
    public class LoopsDto
    {
        public int UserId { get; set; }
        public byte[] Avatar { get; set; }
        public string NickName { get; set; }
        public byte[] File { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int Bpm { get; set; }
    }
}

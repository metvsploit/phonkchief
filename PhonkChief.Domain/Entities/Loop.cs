using PhonkChief.Domain.Enum;
using System;

namespace PhonkChief.Domain.Entities
{
    public class Loop:BaseEntity
    {
        public byte[] File { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int Bpm { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

using PhonkChief.Domain.DTO;
using System.Collections.Generic;

namespace PhonkChief.Domain.ViewModels
{
    public class LoopsViewModel
    {
        public List<LoopsDto> LoopsList { get; set; }
        public int NumOfPage { get; set; }
    }
}

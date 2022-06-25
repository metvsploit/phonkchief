using AutoMapper;
using PhonkChief.Domain.DTO;
using PhonkChief.Domain.Entities;
using PhonkChief.Domain.ViewModels;

namespace PhonkChief.Domain.Helpers
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile()
        {
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<LoopsDto, Loop>().ReverseMap();
        }
    }
}

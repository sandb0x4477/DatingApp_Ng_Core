using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<User, UserForListDto>()
      .ForMember(dest => dest.PhotoUrl, opt => {
        opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
      })
      .ForMember(dest => dest.Age, opt => {
        opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
      });

      CreateMap<User, UserForDetailsDto>()
      .ForMember(dest => dest.PhotoUrl, opt => {
        opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
      })
      .ForMember(dest => dest.Age, opt => {
        opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
      });

      CreateMap<UserForUpdateDto, User>();
      CreateMap<Photo, PhotoForDetailsDto>();
      CreateMap<Photo, PhotoForReturnDto>();
      CreateMap<PhotoForCreationDto, Photo>();
    }
  }
}

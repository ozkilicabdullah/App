using App.Core.Dto;
using App.Core.Models;
using AutoMapper;

namespace App.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<UserDto, UserCreateDto>().ReverseMap();
            CreateMap<UserDto, UserUpdateDto>().ReverseMap();

            CreateMap<UserRegisterHistory, UserRegisterHistoryDto>().ReverseMap();
            CreateMap<UserRegisterHistory, UserRegisterHistoryCreateDto>().ReverseMap();
            CreateMap<UserRegisterHistory, UserRegisterHistoryUpdateDto>().ReverseMap();
            CreateMap<UserRegisterHistoryDto, UserRegisterHistoryUpdateDto>().ReverseMap();
            CreateMap<UserRegisterHistoryDto, UserRegisterHistoryCreateDto>().ReverseMap();

            CreateMap<UserRefreshToken, UserRefreshTokenDto>().ReverseMap();

            CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();
            CreateMap<EmailSetting, EmailSettingDto>().ReverseMap();

        }
    }
}

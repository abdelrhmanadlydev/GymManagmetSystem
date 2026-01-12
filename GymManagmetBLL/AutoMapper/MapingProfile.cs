using AutoMapper;
using GymManagmetBLL.ViewModels.MemberViewModel;
using GymManagmetBLL.ViewModels.PlanViewModel;
using GymManagmetBLL.ViewModels.SessionViewModel;
using GymManagmetBLL.ViewModels.TrainerViewModel;
using GymManagmetDAL.Entities;
using System;

namespace GymManagmetBLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapTrainer();
            MapSession();
            MapMember();
            MapPlan();
        }

        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src =>
                        $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address.Street))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
                .ForMember(d => d.BuildingNumber, opt => opt.MapFrom(s => s.Address.BuildingNumber));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                    dest.UpdatedAt = DateTime.Now;
                });
        }

        private void MapSession()
        {
            CreateMap<CreateSessionVeiwModel, Session>();

            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
                .ForMember(dest => dest.TrainerName,
                    opt => opt.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest => dest.AvailableSlot, opt => opt.Ignore());

            CreateMap<UpdateSessionVeiwModel, Session>().ReverseMap();
        }

        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street
                }))
                .ForMember(dest => dest.HealthRecord,
                    opt => opt.MapFrom(src => src.HealthRecord));

            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<Member, MemberViewModels>()
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src =>
                        $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber,
                    opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street,
                    opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                    dest.UpdatedAt = DateTime.Now;
                });
        }

        private void MapPlan()
        {
            CreateMap<Plane, PlanViewModel>();

            CreateMap<Plane, UpdatePlanViewModel>()
                .ForMember(dest => dest.PlanName,
                    opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdatePlanViewModel, Plane>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}

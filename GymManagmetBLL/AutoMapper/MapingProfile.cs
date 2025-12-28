using AutoMapper;
using GymManagmetBLL.ViewModels.SessionViewModel;
using GymManagmetDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.AutoMapper
{
    public class MapingProfile : Profile
    {
        public MapingProfile()
        {
            // Create mapping configurations

            // dest = > Destination Member Options (session with CategoryName and TrainerName)
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName,option =>option.MapFrom(src =>src.SessionCategory.CategoryName))
                .ForMember(dest =>dest.TrainerName,option =>option.MapFrom(src =>src.SessionTrainer.Name))
                .ForMember(dest =>dest.AvailableSlot,option =>option.Ignore());

            CreateMap<CreateSessionVeiwModel, Session>();

            // to update mapping with use two maps
            //CreateMap<Session, UpdateSessionVeiwModel>();
            //CreateMap<UpdateSessionVeiwModel, Session>();
            // or use ReverseMap
            CreateMap<Session, UpdateSessionVeiwModel>().ReverseMap();

        }
    }
}

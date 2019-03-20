using AutoMapper;
using Consultant_API_ASP.NET_Core.Data.Entities;
using Consultant_API_ASP.NET_Core.Models;

namespace Consultant_API_ASP.NET_Core.Data
{
    public class ConsultantProfile : Profile
    {
        public ConsultantProfile()
        {
            this.CreateMap<Consultant, ConsultantViewModel>()
                .ForMember(c => c.Addresses, a => a.MapFrom(m => m.Addresses))
                .ReverseMap();

            this.CreateMap<Address, AddressViewModel>()
                .ForMember(a => a.consultant, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(a => a.ConsultantId, a => a.MapFrom(m => m.consultant.ConsultantId));
        }
    }
}

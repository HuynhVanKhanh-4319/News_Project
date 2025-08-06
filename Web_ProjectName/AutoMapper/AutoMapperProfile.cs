using AutoMapper;
using Newtonsoft.Json;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.ViewModels;
using static System.String;

namespace Web_ProjectName.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Home
            CreateMap<M_SupplierOffice, EM_Supplier>();
            CreateMap<M_Supplier, EM_Supplier>()
                .ForMember(destination => destination.name,
                options => options.MapFrom(source => source.addressObj.countryObj.name))
                .ForMember(destination => destination.name,
                options => options.MapFrom(source => source.addressObj.provinceObj.name));
            #endregion
        }
    }
}

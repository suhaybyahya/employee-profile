using AutoMapper;
using Employee_Profile.Models;
using Employee_Profile.ViewModel;
using System.Net;

namespace Employee_Profile.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(source => source.CreatedAt, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using Demo.NTI.BLL.ViewModel;
using Demo.NTI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.MyMapper
{
    public class DomainProfile : Profile
    {


        public DomainProfile()
        {
            //CreateMap<Department , DepartmentVM>(); // get
            //CreateMap<DepartmentVM, Department>(); // create , update , delete

            CreateMap<DepartmentVM, Department>().ReverseMap();

            CreateMap<EmployeeVM, Employee>().ReverseMap()
                 .ForPath(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                 .ForPath(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code));

            CreateMap<CountryVM, Country>().ReverseMap();

            CreateMap<CityVM, City>().ReverseMap();

        }


    }
}

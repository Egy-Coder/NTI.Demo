using AutoMapper;
using Demo.NTI.BLL.Repository;
using Demo.NTI.BLL.ViewModel;
using Demo.NTI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.Service
{
    public class CityService : ICityService
    {
        private readonly IGenericRepository<City> cityRepository;
        private readonly IMapper mapper;

        public CityService(IGenericRepository<City> cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public async Task<List<CityVM>> GetCities(Expression<Func<City, bool>>? filter = null)
        {
            var data = await cityRepository.GetAllAsync(filter);
            var result = mapper.Map<List<CityVM>>(data);
            return result;
        }

        public async Task<int> CountAsync()
        {
            var count = await cityRepository.CountAsync();
            return count;
        }
    }

    public interface ICityService
    {
        Task<List<CityVM>> GetCities(Expression<Func<City,bool>>? filter=null);
        Task<int> CountAsync();

    }
}

using AutoMapper;
using Demo.NTI.BLL.Repository;
using Demo.NTI.BLL.ViewModel;
using Demo.NTI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.Service
{
    public class CountryService : ICountryService
    {
        private readonly IGenericRepository<Country> countryRepository;
        private readonly IMapper mapper;

        public CountryService(IGenericRepository<Country> countryRepository , IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<List<CountryVM>> GetCountries()
        {
            var data = await countryRepository.GetAllAsync();
            var result = mapper.Map<List<CountryVM>>(data);
            return result;
        }

        public async Task<int> CountAsync()
        {
            var count = await countryRepository.CountAsync();
            return count;
        }
    }

    public interface ICountryService
    {
        Task<List<CountryVM>> GetCountries();
        Task<int> CountAsync();
    }
}

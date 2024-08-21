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
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IMapper mapper;

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<List<EmployeeVM>> GetAllAsync(
                                        Expression<Func<Employee, bool>>? filter = null,
                                        int? page = null,
                                        int pageSize = 0,
                                        Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null,
                                        List<Expression<Func<Employee, object>>>? includeProperties = null,
                                        bool noTrack = false)
        {
            var emps = await _employeeRepository.GetAllAsync(filter,page,pageSize,orderBy,includeProperties,false);

            // Mapping
            var result = mapper.Map<List<EmployeeVM>>(emps);
            return result;

        }


        public async Task<EmployeeVM> GetEmployeeByIdAsync(Expression<Func<Employee, bool>>? filter = null,
           List<Expression<Func<Employee, object>>>? includeProperties = null)
        {
            var emp = await _employeeRepository.GetByIdAsync(filter,includeProperties);

            // Mapping
            var result = mapper.Map<EmployeeVM>(emp);
            return result;
        }


        public void CreateEmployee(EmployeeVM department)
        {

            // Mapping
            var result = mapper.Map<Employee>(department);
            _employeeRepository.Create(result);
        }

        public void DeleteEmployee(EmployeeVM department)
        {
            // Mapping
            var result = mapper.Map<Employee>(department);
            result.IsActive = false;

            _employeeRepository.Update(result); // Soft Delete
        }


        public void UpdateEmployee(EmployeeVM department)
        {
            // Mapping
            var result = mapper.Map<Employee>(department);
            result.UpdateFlag = true; 

            _employeeRepository.Update(result);
        }

        public async Task<int> CountAsync()
        {
            var count = await _employeeRepository.CountAsync();
            return count;
        }
    }

    public interface IEmployeeService
    {
        //Task<List<EmployeeVM>> GetEmployeesAsync();
        public Task<List<EmployeeVM>> GetAllAsync(
                                        Expression<Func<Employee, bool>>? filter = null,
                                        int? page = null,
                                        int pageSize = 0,
                                        Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null,
                                        List<Expression<Func<Employee, object>>>? includeProperties = null,
                                        bool noTrack = false);
        Task<EmployeeVM> GetEmployeeByIdAsync(Expression<Func<Employee, bool>>? filter = null,
           List<Expression<Func<Employee, object>>>? includeProperties = null);
        void CreateEmployee(EmployeeVM employee);
        void UpdateEmployee(EmployeeVM employee);
        void DeleteEmployee(EmployeeVM employee);
        Task<int> CountAsync();

    }
}

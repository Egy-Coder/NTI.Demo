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
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper mapper;

        public DepartmentService(IGenericRepository<Department> departmentRepository , IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        public async Task<List<DepartmentVM>> GetDepartmentsAsync()
        {
            var depts = await _departmentRepository.GetAllAsync();

            // Mapping
            var result = mapper.Map<List<DepartmentVM>>(depts);
            return result;

        }


        public async Task<DepartmentVM> GetDepartmentByIdAsync(int id)
        {
            var depts = await _departmentRepository.GetByIdAsync(a => a.Id == id);

            // Mapping
            var result = mapper.Map<DepartmentVM>(depts);
            return result;
        }


        public void CreateDepartment(DepartmentVM department)
        {

            // Mapping
            var result = mapper.Map<Department>(department);
            _departmentRepository.Create(result);
        }

        public void DeleteDepartment(DepartmentVM department)
        {
            // Mapping
            var result = mapper.Map<Department>(department);
            _departmentRepository.Delete(result);
        }




        public void UpdateDepartment(DepartmentVM department)
        {
            // Mapping
            var result = mapper.Map<Department>(department);
            _departmentRepository.Update(result);
        }


        public async Task<int> CountAsync()
        {
            var count = await _departmentRepository.CountAsync();
            return count;
        }
    }


    public interface IDepartmentService
    {
        Task<List<DepartmentVM>> GetDepartmentsAsync();
        Task<DepartmentVM> GetDepartmentByIdAsync(int id);
        void CreateDepartment(DepartmentVM department);
        void UpdateDepartment(DepartmentVM department);
        void DeleteDepartment(DepartmentVM department);
        Task<int> CountAsync();

    }
}

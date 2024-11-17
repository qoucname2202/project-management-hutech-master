using AutoMapper;
using DeviceManagementSystem.Models;

namespace DeviceManagementSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<ProjectViewModel, Project>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<TaskViewModel, Task>();
            CreateMap<Task, TaskViewModel>();
        }
    }
}

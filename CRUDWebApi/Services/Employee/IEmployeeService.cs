using CRUDWebApi.Common;
using CRUDWebApi.DtoModels.Employee;

namespace CRUDWebApi.Services.Employee;

public interface IEmployeeService
{
    Task<ApiResponse<GetEmployeeDto>> CreateAsync(CreateEmployeeDto employee);
    Task<ApiResponse<GetEmployeeDto>> UpdateAsync(UpdateEmployeeDto employee);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<GetEmployeeDto>> GetAsync(int id);
    Task<ApiPaginationResponse<GetEmployeeDto>> GetAsync(int pageNumber, int pageSize);
}

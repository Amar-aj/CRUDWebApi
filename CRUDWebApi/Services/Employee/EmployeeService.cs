using CRUDWebApi.Common;
using CRUDWebApi.Context;
using CRUDWebApi.DtoModels.Employee;
using Dapper;
using System.Data;

namespace CRUDWebApi.Services.Employee;

public class EmployeeService : IEmployeeService
{
    private readonly DapperDbContext _context;

    public EmployeeService(DapperDbContext context)
    {
        this._context = context;
    }
    public async Task<ApiResponse<GetEmployeeDto>> CreateAsync(CreateEmployeeDto employee)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@ActionCode", "insert", DbType.String);
            parameters.Add("@FirstName", employee.FirstName, DbType.String);
            parameters.Add("@LastName", employee.LastName, DbType.String);
            parameters.Add("@Email", employee.Email, DbType.String);
            parameters.Add("@DateOfBirth", employee.DateOfBirth, DbType.DateTime);
            parameters.Add("@Salary", employee.Salary, DbType.Decimal);
            parameters.Add("@Department", employee.Department, DbType.String);
            parameters.Add("@IsActive", true, DbType.Boolean);

            using (var response = await connection.QueryMultipleAsync("sp_Employee", parameters, commandType: CommandType.StoredProcedure))
            {
                // Read the  data
                var data = response.Read<GetEmployeeDto>().FirstOrDefault();
                // Read the status code
                int statusCode = response.Read<int>().FirstOrDefault();
                // Read the message
                var message = response.Read<string>().FirstOrDefault();
                return new ApiResponse<GetEmployeeDto>(true, data, message, statusCode);
            }
        }
    }
    public async Task<ApiResponse<GetEmployeeDto>> UpdateAsync(UpdateEmployeeDto employee)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@ActionCode", "update", DbType.String);
            parameters.Add("@Id", employee.Id, DbType.Int32);
            parameters.Add("@FirstName", employee.FirstName, DbType.String);
            parameters.Add("@LastName", employee.LastName, DbType.String);
            parameters.Add("@Email", employee.Email, DbType.String);
            parameters.Add("@DateOfBirth", employee.DateOfBirth, DbType.DateTime);
            parameters.Add("@Salary", employee.Salary, DbType.Decimal);
            parameters.Add("@Department", employee.Department, DbType.String);
            parameters.Add("@IsActive", true, DbType.Boolean);

            using (var response = await connection.QueryMultipleAsync("sp_Employee", parameters, commandType: CommandType.StoredProcedure))
            {
                // Read the  data
                var data = response.Read<GetEmployeeDto>().FirstOrDefault();
                // Read the message
                int statusCode = response.Read<int>().FirstOrDefault();
                // Read the message
                var message = response.Read<string>().FirstOrDefault();
                return new ApiResponse<GetEmployeeDto>(true, data, message, statusCode);
            }
        }
    }
    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@ActionCode", "delete", DbType.String);
            parameters.Add("@Id", id, DbType.Int32);

            using (var response = await connection.QueryMultipleAsync("sp_Employee", parameters, commandType: CommandType.StoredProcedure))
            {
                // Read the status code
                int statusCode = response.Read<int>().FirstOrDefault();
                // Read the message
                var message = response.Read<string>().FirstOrDefault();
                return new ApiResponse<bool>(true, true, message, statusCode);
            }
        }
    }

    public async Task<ApiResponse<GetEmployeeDto>> GetAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@ActionCode", "fetchbyid", DbType.String);
            parameters.Add("@Id", id, DbType.Int32);

            using (var response = await connection.QueryMultipleAsync("sp_Employee", parameters, commandType: CommandType.StoredProcedure))
            {
                // Read the data
                var data = response.Read<GetEmployeeDto>().FirstOrDefault();
                // Read the status code
                int statusCode = response.Read<int>().FirstOrDefault();
                // Read the message
                var message = response.Read<string>().FirstOrDefault();
                return new ApiResponse<GetEmployeeDto>(true, data, message, statusCode);
            }
        }
    }

    public async Task<ApiPaginationResponse<GetEmployeeDto>> GetAsync(int pageNumber, int pageSize)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.Add("@ActionCode", "fetch", DbType.String);
            parameters.Add("@PageNumber", pageNumber, DbType.Int32);
            parameters.Add("@PageSize", pageSize, DbType.Int32);

            using (var response = await connection.QueryMultipleAsync("sp_Employee", parameters, commandType: CommandType.StoredProcedure))
            {
                // Read the data
                var data = response.Read<GetEmployeeDto>().ToList();
                // Read the total pages
                int totalPages = response.Read<int>().FirstOrDefault();
                // Read the status code
                int statusCode = response.Read<int>().FirstOrDefault();
                // Read the message
                var message = response.Read<string>().FirstOrDefault();
                return new ApiPaginationResponse<GetEmployeeDto>(true, data, message, pageNumber, pageSize, totalPages, statusCode: statusCode);
            }
        }
    }


}

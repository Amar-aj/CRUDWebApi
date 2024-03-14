using CRUDWebApi.Common;
using CRUDWebApi.DtoModels.Employee;
using CRUDWebApi.Services.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this._employeeService = employeeService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateEmployeeDto employee) => ApiResponseHandler.Handle(await _employeeService.CreateAsync(employee));
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateEmployeeDto employee)
    {
        employee.Id = id;
        var response = await _employeeService.UpdateAsync(employee);
        return ApiResponseHandler.Handle(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _employeeService.DeleteAsync(id);
        return ApiResponseHandler.Handle(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var response = await _employeeService.GetAsync(id);
        return ApiResponseHandler.Handle(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(int pageNumber, int pageSize)
    {
        var response = await _employeeService.GetAsync(pageNumber, pageSize);
        return ApiResponseHandler.Handle(response);
    }




}
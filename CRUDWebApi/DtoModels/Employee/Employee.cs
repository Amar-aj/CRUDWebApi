namespace CRUDWebApi.DtoModels.Employee;


public class GetEmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }
    public bool IsActive { get; set; }


}
public class CreateEmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }

}
public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }
    public bool IsActive { get; set; }

}
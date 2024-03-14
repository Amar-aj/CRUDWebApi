using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDWebApi.Context;

public class DapperDbContext
{

    private readonly IConfiguration _configuration;

    public DapperDbContext(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
}
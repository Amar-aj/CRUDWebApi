# CRUDWebApi

## System Requirements
- .NET Core 7
- SQL Server 2014

## Description
CRUDWebApi is a .NET Core Web API project that provides basic CRUD (Create, Read, Update, Delete) operations for managing employee data. It utilizes Dapper as the ORM (Object-Relational Mapper) for interacting with the SQL Server 2014 database.


## Pagination :
- Modify controller methods to accept `pageSize` and `pageNumber` parameters.
- Update repository methods to incorporate pagination using SQL's `OFFSET` and `FETCH` clauses.
- Return paginated data along with the total number of records and pages.

## HTTP Response Status Code :
- Implement proper status codes and messages:
  - `200 OK` if data is fetched successfully.
  - `404 Not Found` if no records are found.
  - `400 Bad Request` for invalid requests.
  - `500 Internal Server Error` for server errors.


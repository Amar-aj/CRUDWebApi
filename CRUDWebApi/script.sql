USE [CRUD_API]
GO
/****** Object:  Table [dbo].[tbl_Employee]    Script Date: 15-03-2024 11:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[Salary] [decimal](18, 0) NULL,
	[Department] [varchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tbl_Employee_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_Employee]    Script Date: 15-03-2024 11:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Employee]
(
      @ActionCode VARCHAR(50) = NULL
    , @Id INT = NULL
    , @FirstName NVARCHAR(50) = NULL
    , @LastName NVARCHAR(50) = NULL
    , @Email NVARCHAR(50) = NULL
    , @DateOfBirth DATETIME = NULL
    , @Salary DECIMAL = NULL
    , @Department NVARCHAR(50) = NULL
    , @IsActive BIT = NULL
    , @PageSize INT = NULL
    , @PageNumber INT = NULL
)
AS
BEGIN
  BEGIN TRAN t;

    BEGIN TRY

      IF @ActionCode = 'insert'
      BEGIN

	   IF (
            SELECT COUNT(*)
            FROM tbl_Employee
            WHERE FirstName = @FirstName and LastName=@LastName and Email=@Email
          ) > 0
        BEGIN
                  SELECT 409 AS StatusCode;
          SELECT 'Record Already Exist' AS Message;
        END
        ELSE
        BEGIN

		 INSERT INTO tbl_Employee (FirstName, LastName, Email, DateOfBirth, Salary, Department, IsActive)
        VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @Salary, @Department, @IsActive);

		 SELECT 201;
        SELECT 'Record Inserted Successfully';
        SELECT Id
             , FirstName
             , LastName
             , Email
             , DateOfBirth
             , Salary
             , Department
             , IsActive
        FROM tbl_Employee
        WHERE Id = SCOPE_IDENTITY();
       


        END


        
      END
      ELSE
      IF @ActionCode = 'fetch'
      BEGIN
        DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

        DECLARE @TotalRecords INT;
        SELECT @TotalRecords = COUNT(*)
        FROM tbl_Employee;

        IF @TotalRecords > 0
        BEGIN
          SELECT Id
               , FirstName
               , LastName
               , Email
               , DateOfBirth
               , Salary
               , Department
               , IsActive
          FROM tbl_Employee
          ORDER BY Id
          OFFSET @Offset ROWS
          FETCH NEXT @PageSize ROWS ONLY;

          DECLARE @TotalPages INT = CEILING(CONVERT(FLOAT, @TotalRecords) / @PageSize);
          SELECT @TotalPages AS TotalPages;
          SELECT 200 AS StatusCode;
          SELECT 'Record Fetched Successfully' AS Message;

        END
        ELSE
        BEGIN
          SELECT 404 AS StatusCode;
          SELECT 'Record Not Found' AS Message;
          SELECT 0 AS TotalPages; 
        END
      END

      ELSE
      IF @ActionCode = 'fetchbyid'
      BEGIN
        SELECT Id
             , FirstName
             , LastName
             , Email
             , DateOfBirth
             , Salary
             , Department
             , IsActive
        FROM tbl_Employee
        WHERE Id = @Id;
        IF (
            SELECT COUNT(*)
            FROM tbl_Employee
            WHERE Id = @Id
          ) > 0
        BEGIN
          SELECT 200 AS StatusCode;
          SELECT 'Record Fetched Successfully' AS Message;
        END
        ELSE
        BEGIN
          SELECT 404 AS StatusCode;
          SELECT 'Record Not Found' AS Message;
        END
      END
      ELSE
      IF @ActionCode = 'update'
      BEGIN
        UPDATE tbl_Employee
        SET FirstName = @FirstName
          , LastName = @LastName
          , Email = @Email
          , DateOfBirth = @DateOfBirth
          , Salary = @Salary
          , Department = @Department
          , IsActive = @IsActive
        WHERE Id = @Id;
        SELECT Id
             , FirstName
             , LastName
             , Email
             , DateOfBirth
             , Salary
             , Department
             , IsActive
        FROM tbl_Employee
        WHERE Id = @Id;
        IF (
            SELECT COUNT(*)
            FROM tbl_Employee
            WHERE Id = @Id
          ) > 0
        BEGIN
          SELECT 200 AS StatusCode;
          SELECT 'Record Updated Successfully' AS Message;
        END
        ELSE
        BEGIN
          SELECT 404 AS StatusCode;
          SELECT 'Record Not Found' AS Message;
        END
      END
      ELSE
      IF @ActionCode = 'delete'
      BEGIN


        IF (
            SELECT COUNT(*)
            FROM tbl_Employee
            WHERE Id = @Id
          ) > 0
        BEGIN
          DELETE FROM tbl_Employee
          WHERE Id = @Id;

          SELECT 200 AS StatusCode;
          SELECT 'Record Deleted Successfully' AS Message;
        END
        ELSE
        BEGIN
          SELECT 400 AS StatusCode;
          SELECT 'Invalid Id' AS Message;
        END
      END

    COMMIT TRAN t;
  END TRY
  BEGIN CATCH
    ROLLBACK TRAN t;
    SELECT 500;
    SELECT 'Failed: ' + ERROR_MESSAGE();
  END CATCH

END
GO

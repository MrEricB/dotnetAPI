using DotNetAPI.Data;
using DotNetAPI.Dtos;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        // Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("Users")]
    public ActionResult<IEnumerable<User>> GetUsers()
    {

        string sql = "SELECT * FROM APISchema.Users";
        

        try
        {
            IEnumerable<User> users = _dapper.LoadData<User>(sql);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while getting all users");       
        }
    }

    [HttpGet("UsersFull")]
    public ActionResult<IEnumerable<UserWithJobInfoDto>> GetFullUserData()
    {
        string sql = @"SELECT 
                    U.UserID,
                    U.FirstName,
                    U.LastName,
                    U.Email,
                    U.Active,
                    J.JobTitle,
                    J.Department,
                    J.Salary
                FROM 
                    APISchema.Users U
                JOIN 
                    APISchema.JobInfo J ON U.JobId = J.JobId;";
        
         try
        {
            IEnumerable<UserWithJobInfoDto> users = _dapper.LoadData<UserWithJobInfoDto>(sql);
            

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while getting all users");       
        }
    }

    [HttpGet("Users/{userId}")]
    // public IActionResult GetSingleUser(int userId)
    public ActionResult<User> GetSingleUser(int userId)
    {
        string sql = "SELECT * FROM APISchema.Users WHERE UserID = " + userId.ToString();

        try
        {
            User user = _dapper.LoadDataSingle<User>(sql);
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while geting user: " + userId);   
        }
    }

    [HttpGet("UsersFull/{userId}")]
    public ActionResult<IEnumerable<UserWithJobInfoDto>> GetFullSingleUserData(int userId)
    {
        string sql = @"SELECT 
                U.UserID,
                U.FirstName,
                U.LastName,
                U.Email,
                U.Active,
                J.JobTitle,
                J.Department,
                J.Salary
            FROM 
                APISchema.Users U
            JOIN 
                APISchema.JobInfo J ON U.JobId = J.JobId 
            WHERE U.UserID = " + userId.ToString();

        try
        {
            UserWithJobInfoDto user = _dapper.LoadDataSingle<UserWithJobInfoDto>(sql);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while geting user: " + userId);
        }
    }

    [HttpPut("Users/{userId}")]
    public IActionResult EditUser(UserToAddDto user, int userId)
    {
        string sql = @"
            UPDATE APISchema.Users
                SET [FirstName] = '" + user.FirstName + 
                "', [LastName] = '" + user.LastName + 
                "', [Email] = '" + user.Email + 
                "', [Active] = '" + user.Active +
                "', [JobId] = '" + user.JobId +
                "' WHERE UserId = " + userId;

        try
        {
            if (_dapper.ExecuteSql(sql))
            {
                return Ok(user);
            }
            return StatusCode(500, "Failed to update user" + userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while updating the user: " + userId);
        }

    }

    [HttpPost("Users")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"INSERT INTO APISchema.Users(
                    [FirstName],
                    [LastName],
                    [Email],
                    [Active],
                    [JobId]
                ) VALUES (" +
                    "'" + user.FirstName + 
                    "','" + user.LastName + 
                    "','" + user.Email + 
                    "','" + user.Active + 
                     "','" + user.JobId + 
                   "')";

        try
        {
            if (_dapper.ExecuteSql(sql))
            {
                return Ok(user);
            }
            return StatusCode(500, "Failed to add user");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while adding a new user");
        }
    }

    [HttpDelete("Users/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = "DELETE FROM APISchema.Users WHERE UserId = " + userId; 

        try
        {
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            return StatusCode(500, "Failed to delete user" + userId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while deleting the user: " + userId);           
        }

    }
} // end of controller class

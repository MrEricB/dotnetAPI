using DotNetAPI.Data;
using DotNetAPI.Dtos;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;
    public UserJobInfoController(IConfiguration config)
    {
        // Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("Jobs")]
    public ActionResult<IEnumerable<UserJobInfo>> GetJobs()
    {
        string sql = "SELECT * FROM APISchema.JobInfo";

        try
        {
            IEnumerable<UserJobInfo> jobs = _dapper.LoadData<UserJobInfo>(sql);

            if (jobs == null)
            {
                return NotFound();
            }

            return Ok(jobs);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while getting all jobss");       
        }

    }

    [HttpGet("Jobs/{jobId}")]
    public ActionResult<UserJobInfo> GetSingleJob(int jobId)
    {
        string sql = "SELECT * FROM APISchema.JobInfo WHERE JobId = " + jobId.ToString();

        try
        {
            UserJobInfo job = _dapper.LoadDataSingle<UserJobInfo>(sql);
            
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while geting job: " + jobId);   
        }
    }

    [HttpPut("Jobs/{jobId}")]
    public ActionResult<UserJobInfo> EditJob(UserJobInfoDto job, int jobId)
    {
        string sql = @"
            UPDATE APISchema.JobInfo
                SET [JobTitle] = '" + job.JobTitle + 
                "', [Department] = '" + job.Department + 
                "', [Salary] = '" + job.Salary + 
                "' WHERE JobId = " + jobId;

        try
        {
            if (_dapper.ExecuteSql(sql))
            {
                return Ok(job);
            }
            return StatusCode(500, "Failed to update user" + jobId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while updating the job: " + jobId);
        }

    }

    [HttpPost("Jobs")]
    public ActionResult<UserWithJobInfoDto> CreateJob(UserJobInfoDto job)
    {
        string sql = @"INSERT INTO APISchema.JobInfo(
                [JobTitle],
                [Department],
                [Salary]
                ) VALUES('" + job.JobTitle +"', '" + job.Department +"', "+ job.Salary +")";
    
        try
        {
            if (_dapper.ExecuteSql(sql))
            {
                return Ok(job);
            }
            return StatusCode(500, "Failed to add new Job");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "An error occurred while adding new job" );
        }
    }

    [HttpDelete("Jobs/{jobId}")]
    public ActionResult<UserJobInfo> DeleteJob(int jobId)
    {
        string sql = "DELETE FROM APISchema.JobInfo WHERE JobId = " + jobId;

        try
        {
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            return StatusCode(500, "Failed to delete job" + jobId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "Ann error occured while deleting jog; " + jobId);
        }
    }
} // end of controller class

using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AngularAuthYtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public JobsController(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }
        [Authorize]
        [HttpGet("Jobs")]
        public async Task<ActionResult<IEnumerable<jobs>>> GetAllJobs()
        {
            // Retrieve all jobs and return as ActionResult
            var jobs = await _Context.job.ToListAsync();
            return Ok(jobs);
        }







        [HttpPut("editjobsdata/{id}")]
        public async Task<IActionResult> EditJobsdata(int id, [FromBody] Models.jobs updatedJobsObj)
        {
            if (updatedJobsObj == null || id != updatedJobsObj.JobId)
                return BadRequest();



            var job = await _Context.job.FindAsync(id);
            if (job == null)
                return NotFound();



            job.CompanyName = updatedJobsObj.CompanyName;
            job.JobTitle = updatedJobsObj.JobTitle;
            job.Experience = updatedJobsObj.Experience;
            job.Skills = updatedJobsObj.Skills;
            job.JobType = updatedJobsObj.JobType;
            job.PostedDate = updatedJobsObj.PostedDate;
            job.Location = updatedJobsObj.Location;
            job.JobDescription = updatedJobsObj.JobDescription;






            await _Context.SaveChangesAsync();



            return Ok(new
            {
                Message = "Job Updated Successfully!"
            });
        }




        [HttpDelete("deletejobsdata/{id}")]
        public async Task<IActionResult> DeleteJobsdata(int id)
        {
            var job = await _Context.job.FindAsync(id);
            if (job == null)
                return NotFound();



            _Context.job.Remove(job);
            await _Context.SaveChangesAsync();



            return Ok(new
            {
                Message = "Job Deleted Successfully!"
            });
        }



        [HttpPost("Addjobsdata")]

        public async Task<IActionResult> AddJobsdata([FromBody] jobs jobsObj)

        {

            if (jobsObj == null)

                return BadRequest();



            await _Context.job.AddAsync(jobsObj);

            await _Context.SaveChangesAsync();



            return Ok(new

            {

                Message = "Job Added Successfully!"

            });

        }

        public class JobsRequest
        {
            public jobs JobsObj { get; set; }
        }


    }
}

using AngularAuthYtAPI.Context;
using AngularAuthYtAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AngularAuthYtAPI.Controllers.JobsController;

namespace AngularAuthYtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppliedController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("AppliedJobs")]
        public async Task<ActionResult<IEnumerable<Applied>>> GetAllAppliedJobs()
        {
            var appliedJobs = await _context.appliedjob.ToListAsync();
            return Ok(appliedJobs);
        }





        [HttpPost("ApplyForJob")]
        public async Task<ActionResult<Applied>> ApplyForJob(JobsRequest request)
        {




            var jobId = request.JobsObj.JobId;
            var job = await _context.job.FindAsync(jobId);

            if (job == null)
            {
                return NotFound();
            }

            job.ischecked = false;



            var appliedJob = new Applied
            {

                CompanyName = job.CompanyName,
                JobTitle = job.JobTitle,
                Experience = job.Experience,
                Skills = job.Skills,
                JobType = job.JobType,
                PostedDate = DateTime.Now,
                Location = job.Location,
                JobDescription = job.JobDescription,

            };

            _context.appliedjob.Add(appliedJob);
            await _context.SaveChangesAsync();


            return Ok(appliedJob);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentAgency.Dto;
using RecruitmentAgency.Models;

namespace RecruitmentAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyApplicationController : ControllerBase
    {
        private readonly ResruitmentAgancyDbContext _context;

        public VacancyApplicationController(ResruitmentAgancyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.VacancyApplications.Include(x => x.Vacancy).Include(x => x.Applicant).ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] VacancyApplicationDto newVacancyApplicationDto)
        {
            var newVacancyApplication = new VacancyApplication()
            {
                DateTime = DateTime.Now,
                ApplicantId = newVacancyApplicationDto.ApplicantId,
                VacancyId = newVacancyApplicationDto.VacancyId,
            };

            _context.VacancyApplications.Add(newVacancyApplication);
            _context.SaveChanges();

            return Ok(newVacancyApplication);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VacancyApplicationDto vacancyApplicationDto)
        {
            var existingVacancyApplication = _context.VacancyApplications.FirstOrDefault(a => a.Id == id);

            if (existingVacancyApplication == null)
                return NotFound();

            existingVacancyApplication.ApplicantId = vacancyApplicationDto.ApplicantId;
            existingVacancyApplication.VacancyId = vacancyApplicationDto.VacancyId;
            
            _context.SaveChanges();

            return Ok(existingVacancyApplication);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var vacancyApplicationToDelete = await _context.VacancyApplications.FindAsync(id);
            if (vacancyApplicationToDelete != null)
            {
                _context.VacancyApplications.Remove(vacancyApplicationToDelete);
                _context.SaveChanges();

                return Ok(vacancyApplicationToDelete);
            }
            else return NotFound();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentAgency.Dto;
using RecruitmentAgency.Models;

namespace RecruitmentAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ResruitmentAgancyDbContext _context;

        public ApplicantController(ResruitmentAgancyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Applicants.Include(a => a.VacancyApplications).ThenInclude(v => v.Vacancy).ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] ApplicantDto newApplicantDto)
        {
            var newApplicant = new Applicant()
            {
                Surname = newApplicantDto.Surname,
                Name = newApplicantDto.Name,
                Patronymic = newApplicantDto.Patronymic,
                Telephone = newApplicantDto.Telephone,
                Email = newApplicantDto.Email,
            };

            _context.Applicants.Add(newApplicant);
            _context.SaveChanges();

            return Ok(newApplicant);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApplicantDto applicantDto)
        {
            var existingApplicant = _context.Applicants.FirstOrDefault(a => a.Id == id);

            if (existingApplicant == null)
                return NotFound();

            existingApplicant.Surname = applicantDto.Surname;
            existingApplicant.Name = applicantDto.Name;
            existingApplicant.Patronymic = applicantDto.Patronymic;
            existingApplicant.Telephone = applicantDto.Telephone;
            existingApplicant.Email = applicantDto.Email;

            _context.SaveChanges();

            return Ok(existingApplicant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var applicantToDelete = await _context.Applicants.FindAsync(id);
            if (applicantToDelete != null)
            {
                _context.Applicants.Remove(applicantToDelete);
                _context.SaveChanges();

                return Ok(applicantToDelete);
            }
            else return NotFound();
        }
    }
}
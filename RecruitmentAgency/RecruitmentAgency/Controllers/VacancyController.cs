using Microsoft.AspNetCore.Mvc;
using RecruitmentAgency.Dto;
using RecruitmentAgency.Models;

namespace RecruitmentAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly ResruitmentAgancyDbContext _context;

        public VacancyController(ResruitmentAgancyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Vacancies.ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] VacancyDto newVacancyDto)
        {
            var newVacancy = new Vacancy()
            {
                Name = newVacancyDto.Name,
                Description = newVacancyDto.Description,
                Specialization = newVacancyDto.Specialization,
                Salary = newVacancyDto.Salary,
                DateTime = DateTime.Now,
            };

            _context.Vacancies.Add(newVacancy);
            _context.SaveChanges();

            return Ok(newVacancy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VacancyDto  vacancyDto)
        {
            var existingVacancy = _context.Vacancies.FirstOrDefault(v => v.Id == id);

            if (existingVacancy == null)
                return NotFound();

            existingVacancy.Name = vacancyDto.Name;
            existingVacancy.Description = vacancyDto.Description;
            existingVacancy.Specialization = vacancyDto.Specialization;
            existingVacancy.Salary = vacancyDto.Salary;

            _context.SaveChanges();

            return Ok(existingVacancy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var vacancyToDelete = await _context.Vacancies.FindAsync(id);

            if (vacancyToDelete != null)
            {
                _context.Vacancies.Remove(vacancyToDelete);
                _context.SaveChanges();

                return Ok(vacancyToDelete);
            }
            else return NotFound();
        }
    }
}
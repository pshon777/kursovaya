namespace RecruitmentAgency.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specialization { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<VacancyApplication> VacancyApplications { get; set; }
    }
}
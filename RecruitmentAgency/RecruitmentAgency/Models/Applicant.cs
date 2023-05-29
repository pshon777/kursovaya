namespace RecruitmentAgency.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public ICollection<VacancyApplication> VacancyApplications { get; set; } = new List<VacancyApplication>();
    }
}
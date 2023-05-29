namespace RecruitmentAgency.Models
{
    public class VacancyApplication
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using RecruitmentAgency.Models;

namespace RecruitmentAgency
{
    public class ResruitmentAgancyDbContext : DbContext
    {
        public ResruitmentAgancyDbContext(DbContextOptions<ResruitmentAgancyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyApplication> VacancyApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Applicant>(entity =>
            {
                entity.ToTable("Applicants");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Surname).IsRequired().HasMaxLength(30);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(30);
                entity.Property(p => p.Patronymic).IsRequired().HasMaxLength(30);
                entity.Property(p => p.Telephone).IsRequired().HasMaxLength(15);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(30);
            });

            builder.Entity<Vacancy>(entity =>
            {
                entity.ToTable("Vacancy");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Specialization);
                entity.Property(e => e.Salary);
                entity.Property(e => e.DateTime).IsRequired().HasDefaultValue(new DateTime());
            });

            builder.Entity<VacancyApplication>(entity =>
            {
                entity.ToTable("VacancyApplication");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.DateTime).IsRequired().HasDefaultValue(new DateTime());
                entity.HasOne(e => e.Vacancy)
                      .WithMany(e => e.VacancyApplications)
                      .HasForeignKey(e => e.VacancyId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Applicant)
                      .WithMany(e => e.VacancyApplications)
                      .HasForeignKey(e => e.ApplicantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Applicant>().HasData
            (
                new Applicant { Id = 90, Surname = "Иванов", Name = "Иван", Patronymic = "Иванович", Telephone = "7874574", Email = "Ivanov@mail.ru" },
                new Applicant { Id = 91, Surname = "Васичкин", Name = "Дмитрий", Patronymic = "Иванович", Telephone = "4596236", Email = "Vas@mail.ru" }
            );

            builder.Entity<Vacancy>().HasData
            (
                new Vacancy { Id = 90, Name = "Курьер", Description = "Частичная занятость, полный день. Возможна подработка: сменами по 4 - 6 часов, по выходным или по вечерам", Specialization = "опыт работы - не требйется", Salary = 800 },
                new Vacancy { Id = 91, Name = "Администратор фитнес-клуба", Description = "полная занятость, сменный график", Specialization = "требуемый опыт работы: 1–3 года", Salary = 1400 }
            );

            builder.Entity<VacancyApplication>().HasData
            (
                new VacancyApplication { Id = 90, ApplicantId = 90, VacancyId = 91 },
                new VacancyApplication { Id = 91, ApplicantId = 91, VacancyId = 90 }
             );
        }
    }
}
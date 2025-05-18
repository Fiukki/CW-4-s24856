using CW_4_s24856.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_4_s24856.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Prescription> Prescription { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
    public DbSet<Medicament> Medicament { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctor = new Doctor
        {
            IdDoctor = 1,
            Email = "doctor@example.com",
            FirstName = "Doctor",
            LastName = "Doctor"
        };

        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            Birthdate = new DateTime(1980, 1, 1)
        };

        var medicament1 = new Medicament
        {
            IdMedicament = 1,
            Name = "Paracetamol",
            Dose = "500mg",
            Type = "Tablet"
        };

        var medicament2 = new Medicament
        {
            IdMedicament = 2,
            Name = "Ibuprofen",
            Dose = "200mg",
            Type = "Tablet"
        };

        var prescription = new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(2023, 1, 1),
            DueDate = new DateTime(2023, 1, 10),
            IdDoctor = 1,
            IdPatient = 1
        };

        var prescriptionMedicament1 = new PrescriptionMedicament
        {
            IdPrescription = prescription.IdPrescription,
            IdMedicament = medicament1.IdMedicament,
            Dose = 2,
            Details = "Take after meal"
        };

        var prescriptionMedicament2 = new PrescriptionMedicament
        {
            IdPrescription = prescription.IdPrescription,
            IdMedicament = medicament2.IdMedicament,
            Dose = 1,
            Details = "Take twice a day"
        };

        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Medicament>().HasData(medicament1, medicament2);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament1, prescriptionMedicament2);
    }
}
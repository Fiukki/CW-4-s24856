using CW_4_s24856.Data;
using CW_4_s24856.DTOs;
using CW_4_s24856.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_4_s24856.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(AddPrescriptionRequestDto dto);
    Task<PatientDetailsDto> GetPatientDetailsAsync(int id);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _context;

    public PrescriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(AddPrescriptionRequestDto dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("Cannot add more than 10 medicaments.");

        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate must be >= Date.");

        var doctor = await _context.Doctors.FindAsync(dto.DoctorId)
                     ?? throw new Exception("Doctor not found.");

        var patient = _context.Patient.FirstOrDefault(p =>
            p.FirstName == dto.PatientFirstName &&
            p.LastName == dto.PatientLastName &&
            p.Birthdate == dto.PatientBirthdate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.PatientFirstName,
                LastName = dto.PatientLastName,
                Birthdate = dto.PatientBirthdate
            };
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient
        };

        _context.Prescription.Add(prescription);
        await _context.SaveChangesAsync();

        foreach (var m in dto.Medicaments)
        {
            var medicament = await _context.Medicament.FindAsync(m.MedicamentId);
            if (medicament == null)
                throw new ArgumentException($"Medicament ID {m.MedicamentId} not found.");

            _context.PrescriptionMedicament.Add(new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = m.MedicamentId,
                Dose = m.Dose,
                Details = m.Details
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task<PatientDetailsDto> GetPatientDetailsAsync(int id)
    {
        var patient = await _context.Patient
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicament)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null) throw new Exception("Patient not found.");

        return new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName
                    },
                    Medicaments = p.PrescriptionMedicament.Select(pm => new MedicamentDetailsDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Details = pm.Details
                    }).ToList()
                }).ToList()
        };
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CW_4_s24856.Migrations
{
    /// <inheritdoc />
    public partial class Createtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_DoctorIdDoctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientIdPatient",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_DoctorIdDoctor",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PatientIdPatient",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "DoctorIdDoctor",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PatientIdPatient",
                table: "Prescription");

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "doctor@example.com", "Doctor", "Doctor" });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Dose", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "500mg", "Paracetamol", "Tablet" },
                    { 2, "200mg", "Ibuprofen", "Tablet" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "Birthdate", "FirstName", "LastName" },
                values: new object[] { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jan", "Kowalski" });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[,]
                {
                    { 1, 1, "Take after meal", 2 },
                    { 2, 1, "Take twice a day", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescription",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescription",
                column: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription",
                column: "IdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescription");

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "DoctorIdDoctor",
                table: "Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientIdPatient",
                table: "Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DoctorIdDoctor",
                table: "Prescription",
                column: "DoctorIdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientIdPatient",
                table: "Prescription",
                column: "PatientIdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_DoctorIdDoctor",
                table: "Prescription",
                column: "DoctorIdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_PatientIdPatient",
                table: "Prescription",
                column: "PatientIdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

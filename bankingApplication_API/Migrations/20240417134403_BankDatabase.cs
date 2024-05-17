using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankingApplication_API.Migrations
{
    /// <inheritdoc />
    public partial class BankDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JuridicalPerson",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correspondenceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nip = table.Column<long>(type: "bigint", nullable: false),
                    regon = table.Column<long>(type: "bigint", nullable: false),
                    phone = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entryKRS = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    companyAgreement = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    representativeFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativeLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativeBirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    representativeBirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativePesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativePhone = table.Column<int>(type: "int", nullable: false),
                    representativeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativeIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    representativeIdScan = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verificationToken = table.Column<int>(type: "int", nullable: false),
                    customerNumber = table.Column<int>(type: "int", nullable: false),
                    creationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuridicalPerson", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NaturalPerson",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    birthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verificationToken = table.Column<int>(type: "int", nullable: false),
                    nip = table.Column<long>(type: "bigint", nullable: true),
                    regon = table.Column<long>(type: "bigint", nullable: true),
                    customerNumber = table.Column<int>(type: "int", nullable: false),
                    creationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalPerson", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JuridicalPerson");

            migrationBuilder.DropTable(
                name: "NaturalPerson");
        }
    }
}

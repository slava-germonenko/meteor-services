using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Meteor.Employees.Infrastructure.Migrations.Migrations
{
    public partial class InitialEmployeeSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "custom_fields",
                table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    required = table.Column<bool>(type: "boolean", nullable: false),
                    unique = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__custom_fields", x => x.id);
                    table.UniqueConstraint("uix__custom_fields_n_ame", x => x.name);
                });

            migrationBuilder.CreateTable(
                "employees",
                table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email_address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", maxLength: 200, nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", maxLength: 200, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__employees", x => x.id);
                    table.UniqueConstraint("uix__employees__email_address", x => x.email_address);
                    table.UniqueConstraint("uix__employees__phone_number", x => x.phone_number);
                });

            migrationBuilder.CreateTable(
                "employee_fields",
                table => new
                {
                    custom_field_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__employee_fields", x => new { x.custom_field_id, x.employee_id });
                    table.ForeignKey(
                        name: "fk__custom_fields__employee_fields",
                        column: x => x.custom_field_id,
                        principalTable: "custom_fields",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk__employees__employee_fields",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status_change_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason = table.Column<string>(type: "text", nullable: false),
                    source_status = table.Column<int>(type: "integer", nullable: false),
                    target_status = table.Column<int>(type: "integer", nullable: false),
                    change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__status_change_reasons", x => x.id);
                    table.ForeignKey(
                        name: "fk__employees__status_change_reasons",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("employee_fields");
            migrationBuilder.DropTable("status_change_reasons");
            migrationBuilder.DropTable("custom_fields");
            migrationBuilder.DropTable("employees");
        }
    }
}

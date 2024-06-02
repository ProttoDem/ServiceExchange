using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTaskSequences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Calendars_CalendarId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CalendarId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Calendars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_TaskId",
                table: "Calendars",
                column: "TaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Tasks_TaskId",
                table: "Calendars",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Tasks_TaskId",
                table: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Calendars_TaskId",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Calendars");

            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CalendarId",
                table: "Tasks",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Calendars_CalendarId",
                table: "Tasks",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

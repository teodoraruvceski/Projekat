﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadBalancer.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class m1 : Migration
    {
        [ExcludeFromCodeCoverage]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "One",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_One", x => x.Id);
                });
        }
        [ExcludeFromCodeCoverage]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "One");
        }
    }
}

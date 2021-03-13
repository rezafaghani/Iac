using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Iac.Api.Infrastructure.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "stack");

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "networks",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NetworkType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Hostname = table.Column<string>(type: "text", nullable: true),
                    DomainName = table.Column<string>(type: "text", nullable: true),
                    MacAddress = table.Column<string>(type: "text", nullable: true),
                    Ipv4Address = table.Column<string>(type: "text", nullable: true),
                    Ipv6Address = table.Column<string>(type: "text", nullable: true),
                    PrimaryDnsServer = table.Column<string>(type: "text", nullable: true),
                    SecondaryDnsServer = table.Column<string>(type: "text", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_networks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "oses",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_oses_locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "stack",
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stacks",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StackName = table.Column<string>(type: "text", nullable: false),
                    OsId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stacks_oses_OsId",
                        column: x => x.OsId,
                        principalSchema: "stack",
                        principalTable: "oses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stackitems",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ImageName = table.Column<string>(type: "text", nullable: true),
                    StackId = table.Column<long>(type: "bigint", nullable: false),
                    NetworkId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stackitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stackitems_networks_NetworkId",
                        column: x => x.NetworkId,
                        principalSchema: "stack",
                        principalTable: "networks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stackitems_stacks_StackId",
                        column: x => x.StackId,
                        principalSchema: "stack",
                        principalTable: "stacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stackitemvariables",
                schema: "stack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    StackItemId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stackitemvariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stackitemvariables_stackitems_StackItemId",
                        column: x => x.StackItemId,
                        principalSchema: "stack",
                        principalTable: "stackitems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_oses_LocationId",
                schema: "stack",
                table: "oses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_stackitems_NetworkId",
                schema: "stack",
                table: "stackitems",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_stackitems_StackId",
                schema: "stack",
                table: "stackitems",
                column: "StackId");

            migrationBuilder.CreateIndex(
                name: "IX_stackitemvariables_StackItemId",
                schema: "stack",
                table: "stackitemvariables",
                column: "StackItemId");

            migrationBuilder.CreateIndex(
                name: "IX_stacks_OsId",
                schema: "stack",
                table: "stacks",
                column: "OsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests",
                schema: "stack");

            migrationBuilder.DropTable(
                name: "stackitemvariables",
                schema: "stack");

            migrationBuilder.DropTable(
                name: "stackitems",
                schema: "stack");

            migrationBuilder.DropTable(
                name: "networks",
                schema: "stack");

            migrationBuilder.DropTable(
                name: "stacks",
                schema: "stack");

            migrationBuilder.DropTable(
                name: "oses",
                schema: "stack");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "stack");
        }
    }
}

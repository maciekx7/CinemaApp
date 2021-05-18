using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace CinemaServer.Model.Migrations
{
    public partial class AddedIdentityColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cinemadb");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "cinemadb",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    lastname = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    phone = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: true),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    dubbing = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    subtitles = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    producer = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    title = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    image_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reservation_type",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    discount = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    seat_row = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    seat_column = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "cinemadb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 85, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cinemadb",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "cinemadb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 85, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Client_UserId",
                        column: x => x.UserId,
                        principalSchema: "cinemadb",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "cinemadb",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Client_UserId",
                        column: x => x.UserId,
                        principalSchema: "cinemadb",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "cinemadb",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "cinemadb",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Client_UserId",
                        column: x => x.UserId,
                        principalSchema: "cinemadb",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "cinemadb",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    Name = table.Column<string>(type: "varchar(85)", maxLength: 85, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Client_UserId",
                        column: x => x.UserId,
                        principalSchema: "cinemadb",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screening",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    movie_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id, x.movie_id });
                    table.UniqueConstraint("AK_Screening_id", x => x.id);
                    table.ForeignKey(
                        name: "movie_id_screening_fk",
                        column: x => x.movie_id,
                        principalSchema: "cinemadb",
                        principalTable: "Movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    screening_id = table.Column<int>(type: "int", nullable: false),
                    reservation_type_id = table.Column<int>(type: "int", nullable: false),
                    Client_id = table.Column<string>(type: "varchar(85)", nullable: false),
                    price = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    qr_code = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id, x.screening_id, x.reservation_type_id, x.Client_id });
                    table.UniqueConstraint("AK_Ticket_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_Ticket_Client1",
                        column: x => x.Client_id,
                        principalSchema: "cinemadb",
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "reservationType_ticket_id_fk",
                        column: x => x.reservation_type_id,
                        principalSchema: "cinemadb",
                        principalTable: "Reservation_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "screening_id_ticket_fk",
                        column: x => x.screening_id,
                        principalSchema: "cinemadb",
                        principalTable: "Screening",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seat_reserved",
                schema: "cinemadb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    seat_id = table.Column<int>(type: "int", nullable: false),
                    screening_id = table.Column<int>(type: "int", nullable: false),
                    ticket_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id, x.ticket_id, x.screening_id, x.seat_id });
                    table.ForeignKey(
                        name: "screening_id_seatReservation_fk",
                        column: x => x.screening_id,
                        principalSchema: "cinemadb",
                        principalTable: "Screening",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "seat_id_seatReservation_fk",
                        column: x => x.seat_id,
                        principalSchema: "cinemadb",
                        principalTable: "Seat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ticket_id_seatReservation_fk",
                        column: x => x.ticket_id,
                        principalSchema: "cinemadb",
                        principalTable: "Ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "cinemadb",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "cinemadb",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "cinemadb",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "cinemadb",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "cinemadb",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "client_id_UNIQUE",
                schema: "cinemadb",
                table: "Client",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "email_UNIQUE",
                schema: "cinemadb",
                table: "Client",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "cinemadb",
                table: "Client",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "cinemadb",
                table: "Client",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "movie_id_UNIQUE",
                schema: "cinemadb",
                table: "Movie",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Screening_Movie1_idx",
                schema: "cinemadb",
                table: "Screening",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "screening_id_UNIQUE",
                schema: "cinemadb",
                table: "Screening",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                schema: "cinemadb",
                table: "Seat",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "screening_id_fk_idx",
                schema: "cinemadb",
                table: "Seat_reserved",
                column: "screening_id");

            migrationBuilder.CreateIndex(
                name: "seat_id_fk_idx",
                schema: "cinemadb",
                table: "Seat_reserved",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "ticket_id_fk_idx",
                schema: "cinemadb",
                table: "Seat_reserved",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "fk_Ticket_Client1_idx",
                schema: "cinemadb",
                table: "Ticket",
                column: "Client_id");

            migrationBuilder.CreateIndex(
                name: "qr_code_UNIQUE",
                schema: "cinemadb",
                table: "Ticket",
                column: "qr_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "reservation_type_id_fk_idx",
                schema: "cinemadb",
                table: "Ticket",
                column: "reservation_type_id");

            migrationBuilder.CreateIndex(
                name: "screening_id_idx",
                schema: "cinemadb",
                table: "Ticket",
                column: "screening_id");

            migrationBuilder.CreateIndex(
                name: "ticket_id_UNIQUE",
                schema: "cinemadb",
                table: "Ticket",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Seat_reserved",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Seat",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Reservation_type",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Screening",
                schema: "cinemadb");

            migrationBuilder.DropTable(
                name: "Movie",
                schema: "cinemadb");
        }
    }
}

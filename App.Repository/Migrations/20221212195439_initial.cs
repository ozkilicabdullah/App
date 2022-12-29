using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    ApiTransactionCode = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    SendingProtokol = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    MailConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForgetPaswordSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmationSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SendingContentType = table.Column<int>(type: "int", nullable: false),
                    DescriptionHTML = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSettingID = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_EmailSettings_EmailSettingID",
                        column: x => x.EmailSettingID,
                        principalTable: "EmailSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRegisterHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegisterHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRegisterHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmailSettings",
                columns: new[] { "Id", "ApiKey", "ApiTransactionCode", "CreatedBy", "CreatedOn", "EnableSsl", "Host", "ModifiedBy", "ModifiedOn", "Name", "Password", "Port", "SenderEmail", "SenderName", "SendingProtokol", "Status", "UserName" },
                values: new object[] { 1, "123", "123", null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7159), true, "smtp.office365.com", null, null, "Genel Smtp", "!applogin!99", 587, "digiapplogin@outlook.com.tr", "digiapplogin@outlook.com.tr", 0, 0, "digiapplogin@outlook.com.tr" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Email", "EmailConfirmationSecretKey", "ForgetPaswordSecretKey", "IsEmailConfirmed", "MailConfirmedDate", "ModifiedBy", "ModifiedOn", "Name", "Password", "ResetPasswordSecretKey", "Role", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7879), "manager1@manager.com", null, null, true, new DateTime(2022, 12, 12, 23, 54, 38, 897, DateTimeKind.Local).AddTicks(7880), null, null, "app.manager manager1", "57801fc3d556f5522d975545d20419b9", null, "Manager", 0 },
                    { 2, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7927), "user1@manager.com", null, null, false, new DateTime(2022, 12, 13, 0, 54, 38, 897, DateTimeKind.Local).AddTicks(7928), null, null, "app.user user1", "3872849effd186dbceb03cf0cf22e6a8", null, "user", 0 },
                    { 3, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7943), "user2@manager.com", null, null, true, new DateTime(2022, 12, 13, 1, 54, 38, 897, DateTimeKind.Local).AddTicks(7943), null, null, "app.user user2", "3872849effd186dbceb03cf0cf22e6a8", null, "user", 0 },
                    { 4, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7955), "user2@manager.com", null, null, false, null, null, null, "app.user user2", "3872849effd186dbceb03cf0cf22e6a8", null, "user", 0 },
                    { 5, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7971), "user3@manager.com", null, null, false, null, null, null, "app.user user3", "3872849effd186dbceb03cf0cf22e6a8", null, "user", 0 }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DescriptionHTML", "DescriptionText", "EmailSettingID", "ModifiedBy", "ModifiedOn", "Name", "SendingContentType", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7459), "<p>Üyeliğinizi tamamlamak için son adım. Üyeliğinizi onaylamak için <a href='MailConfirmationValidationSecretKey'>buraya tıklayınız.</a></p>", "Üyelik onay maili", 1, null, null, "New member", 0, 0 },
                    { 2, null, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(7464), "<p>Paraloanızı sıfırlamak için <a href='ResetPasswordSecretKey'>buraya tıklayınız.</a></p>", "Şifremi Unuttum", 1, null, null, "Foget my password", 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserRegisterHistories",
                columns: new[] { "Id", "ConfirmDate", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(8383), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, 1 },
                    { 2, new DateTime(2022, 12, 12, 22, 54, 38, 897, DateTimeKind.Local).AddTicks(8389), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailSettingID",
                table: "EmailTemplates",
                column: "EmailSettingID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegisterHistories_UserId",
                table: "UserRegisterHistories",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRegisterHistories");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

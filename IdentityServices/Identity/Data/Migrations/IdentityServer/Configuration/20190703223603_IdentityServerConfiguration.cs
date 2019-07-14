using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServices.Data.Migrations.IdentityServer.Configuration
{
    public partial class IdentityServerConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "idn_api_resources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_api_resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "idn_clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(nullable: false),
                    RequirePkce = table.Column<bool>(nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    BackChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenLifetime = table.Column<int>(nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false),
                    RefreshTokenUsage = table.Column<int>(nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false),
                    AccessTokenType = table.Column<int>(nullable: false),
                    EnableLocalLogin = table.Column<bool>(nullable: false),
                    IncludeJwtId = table.Column<bool>(nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    ClientClaimsPrefix = table.Column<string>(maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(nullable: false),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "idn_identity_resources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_identity_resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "idn_api_claims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_api_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_api_claims_idn_api_resources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "idn_api_resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_api_properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_api_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_api_properties_idn_api_resources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "idn_api_resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_api_scopes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_api_scopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_api_scopes_idn_api_resources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "idn_api_resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_api_secrets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_api_secrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_api_secrets_idn_api_resources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "idn_api_resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_claims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_claims_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_cors_origins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_cors_origins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_cors_origins_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_grant_types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GrantType = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_grant_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_grant_types_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_id_prestrictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_id_prestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_id_prestrictions_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_post_logout_redirect_uris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PostLogoutRedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_post_logout_redirect_uris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_post_logout_redirect_uris_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_properties_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_redirect_uris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_redirect_uris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_redirect_uris_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_scopes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Scope = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_scopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_scopes_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_client_secrets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_client_secrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_client_secrets_idn_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "idn_clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_identity_claims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_identity_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_identity_claims_idn_identity_resources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "idn_identity_resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_identity_properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_identity_properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_identity_properties_idn_identity_resources_IdentityResou~",
                        column: x => x.IdentityResourceId,
                        principalTable: "idn_identity_resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "idn_api_scope_claims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiScopeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idn_api_scope_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idn_api_scope_claims_idn_api_scopes_ApiScopeId",
                        column: x => x.ApiScopeId,
                        principalTable: "idn_api_scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_claims_ApiResourceId",
                table: "idn_api_claims",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_properties_ApiResourceId",
                table: "idn_api_properties",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_resources_Name",
                table: "idn_api_resources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_scope_claims_ApiScopeId",
                table: "idn_api_scope_claims",
                column: "ApiScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_scopes_ApiResourceId",
                table: "idn_api_scopes",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_scopes_Name",
                table: "idn_api_scopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_idn_api_secrets_ApiResourceId",
                table: "idn_api_secrets",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_claims_ClientId",
                table: "idn_client_claims",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_cors_origins_ClientId",
                table: "idn_client_cors_origins",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_grant_types_ClientId",
                table: "idn_client_grant_types",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_id_prestrictions_ClientId",
                table: "idn_client_id_prestrictions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_post_logout_redirect_uris_ClientId",
                table: "idn_client_post_logout_redirect_uris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_properties_ClientId",
                table: "idn_client_properties",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_redirect_uris_ClientId",
                table: "idn_client_redirect_uris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_scopes_ClientId",
                table: "idn_client_scopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_client_secrets_ClientId",
                table: "idn_client_secrets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_clients_ClientId",
                table: "idn_clients",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_idn_identity_claims_IdentityResourceId",
                table: "idn_identity_claims",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_identity_properties_IdentityResourceId",
                table: "idn_identity_properties",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_idn_identity_resources_Name",
                table: "idn_identity_resources",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "idn_api_claims");

            migrationBuilder.DropTable(
                name: "idn_api_properties");

            migrationBuilder.DropTable(
                name: "idn_api_scope_claims");

            migrationBuilder.DropTable(
                name: "idn_api_secrets");

            migrationBuilder.DropTable(
                name: "idn_client_claims");

            migrationBuilder.DropTable(
                name: "idn_client_cors_origins");

            migrationBuilder.DropTable(
                name: "idn_client_grant_types");

            migrationBuilder.DropTable(
                name: "idn_client_id_prestrictions");

            migrationBuilder.DropTable(
                name: "idn_client_post_logout_redirect_uris");

            migrationBuilder.DropTable(
                name: "idn_client_properties");

            migrationBuilder.DropTable(
                name: "idn_client_redirect_uris");

            migrationBuilder.DropTable(
                name: "idn_client_scopes");

            migrationBuilder.DropTable(
                name: "idn_client_secrets");

            migrationBuilder.DropTable(
                name: "idn_identity_claims");

            migrationBuilder.DropTable(
                name: "idn_identity_properties");

            migrationBuilder.DropTable(
                name: "idn_api_scopes");

            migrationBuilder.DropTable(
                name: "idn_clients");

            migrationBuilder.DropTable(
                name: "idn_identity_resources");

            migrationBuilder.DropTable(
                name: "idn_api_resources");
        }
    }
}

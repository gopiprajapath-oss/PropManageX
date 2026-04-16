using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PropManageX.Models.Context;
using PropManageX.Services.BillingReferenceAndAnalytics.Invoice;
using PropManageX.Services.BillingReferenceAndAnalytics.RevenueReport;
using PropManageX.Services.ContractsLeasesRenewal.Contracts;
using PropManageX.Services.ContractsLeasesRenewal.Renewal;
using PropManageX.Services.DocumentsAndComplianceManagement.Documents;
using PropManageX.Services.DocumentsAndComplianceManagement.Notification;
using PropManageX.Services.IdentityAndRoleManagement;
using PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceDeal;
using PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceLead;
using PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceSiteVist;
using PropManageX.Services.PropertyListingInventoryManagement.AmenityService;
using PropManageX.Services.PropertyListingInventoryManagement.PropertyService.PropertyService;
using PropManageX.Services.PropertyListingInventoryManagement.ServiceAmenity;
using PropManageX.Services.PropertyListingInventoryManagement.ServiceProperty;
using PropManageX.Services.PropertyListingInventoryManagement.ServiceUnit;
using PropManageX.Services.PropertyListingInventoryManagement.UnitService.UnitService;
using PropManageX.Services.TenantOperationsAndMaintainenceRequest;
using PropManageX.Services.TenantOperationsAndMaintainenceRequest.MaintenanceRequest;
using PropManageX.Services.TenantOperationsAndMaintainenceRequest.VendorAssignment;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Program.cs (or Startup)
builder.Services.AddDbContext<PropManageXContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IAmenityService,AmenityService>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<ISiteVistService, SiteVistService>();
builder.Services.AddScoped<IDealService, DealService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IRenewalService, RenewalService>();
builder.Services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();
builder.Services.AddScoped<IVendorAssignmentService, VendorAssignmentService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IRevenueReportService , RevenueReportService>();
builder.Services.AddScoped<IDocumentService , DocumentService>();
builder.Services.AddScoped<INotificationService , NotificationService>();
builder.Services.AddScoped<DashboardService>();    

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("TenantOnly", policy => policy.RequireRole("Tenant"));
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

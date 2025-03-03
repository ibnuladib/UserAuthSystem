using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4_UserAuth.Data;
using Task4_UserAuth.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UserAuthContextConnection") ?? throw new InvalidOperationException("Connection string 'UserAuthContextConnection' not found.");

builder.Services.AddDbContext<UserAuthContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;  
    options.Password.RequireDigit = false;          
    options.Password.RequireLowercase = false;      
    options.Password.RequireUppercase = false;      
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365 * 100);
    options.Lockout.MaxFailedAccessAttempts = 1;
})
.AddEntityFrameworkStores<UserAuthContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) => {
    if (context.User.Identity?.IsAuthenticated == true &&
        !context.Request.Path.StartsWithSegments("/Identity/Account"))
    {
        var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.GetUserAsync(context.User);

        if (user == null || await userManager.IsLockedOutAsync(user))
        {
            await context.SignOutAsync();
            context.Response.Redirect("/Identity/Account/Login");
            return;
        }
    }
    await next();
});

app.Run();

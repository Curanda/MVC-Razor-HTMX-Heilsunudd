using Heilsunudd.Data.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Heilsunudd.Intranet.Data;
using Heilsunudd.Intranet.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Heilsunudd.Intranet;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("HeilsunuddContext") ?? throw new InvalidOperationException("Connection string 'HeilsunuddContext' not found.")));

        builder.Services.AddIdentity<Users, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        }).AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();
        
        builder.Services.AddDbContext<HeilsunuddDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("HeilsunuddContext") ?? throw new InvalidOperationException("Connection string 'HeilsunuddContext' not found.")));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
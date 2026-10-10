
using Lab1.Models;
using Microsoft.AspNetCore.Identity;

namespace Lab1.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider
                .GetRequiredService<IConfiguration>();

            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            // Read passwords from secure configuration.
            string supervisorPassword =
                configuration["SeedUsers:SupervisorPassword"]
                ?? throw new InvalidOperationException(
                    "Supervisor seed password is not configured.");

            string employeePassword =
                configuration["SeedUsers:EmployeePassword"]
                ?? throw new InvalidOperationException(
                    "Employee seed password is not configured.");

            // Create roles if they do not exist.
            foreach (string role in new[] { "Supervisor", "Employee" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(
                        new IdentityRole(role));

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(
                            $"Could not create role {role}: " +
                            string.Join(", ",
                                result.Errors.Select(e => e.Description)));
                    }
                }
            }

            // Find or create the Supervisor account.
            string supervisorEmail = "supervisor@lab1.com";

            var supervisor = await userManager.FindByEmailAsync(
                supervisorEmail);

            if (supervisor == null)
            {
                supervisor = new ApplicationUser
                {
                    UserName = supervisorEmail,
                    Email = supervisorEmail,
                    FirstName = "Lab",
                    LastName = "Supervisor",
                    City = "Hamilton",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    supervisor, supervisorPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Could not create Supervisor: " +
                        string.Join(", ",
                            result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(
                supervisor, "Supervisor"))
            {
                await userManager.AddToRoleAsync(
                    supervisor, "Supervisor");
            }

            // Find or create the Employee account.
            string employeeEmail = "employee@lab1.com";

            var employee = await userManager.FindByEmailAsync(
                employeeEmail);

            if (employee == null)
            {
                employee = new ApplicationUser
                {
                    UserName = employeeEmail,
                    Email = employeeEmail,
                    FirstName = "Lab",
                    LastName = "Employee",
                    City = "Hamilton",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    employee, employeePassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Could not create Employee: " +
                        string.Join(", ",
                            result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(
                employee, "Employee"))
            {
                await userManager.AddToRoleAsync(
                    employee, "Employee");
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace GestionPersonal.Data;

public static class IdentitySeeder
{
    public static async Task InicializarAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var rol in new[] { RolesApp.Gerente, RolesApp.Colaborador })
        {
            if (!await roleManager.RoleExistsAsync(rol))
                await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }
}
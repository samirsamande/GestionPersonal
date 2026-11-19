using Microsoft.AspNetCore.Identity;

namespace GestionPersonal.Data;

public static class IdentitySeeder
{
    public static async Task InicializarAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var rol in new[] { RolesApp.Gerente, RolesApp.Colaborador })
        {
            if (!await roleManager.RoleExistsAsync(rol))
                await roleManager.CreateAsync(new IdentityRole(rol));
        }

        await CrearUsuarioAsync(userManager, "gerente@empresa.com", "Gerente123*", RolesApp.Gerente);
        await CrearUsuarioAsync(userManager, "colaborador@empresa.com", "Colaborador123*", RolesApp.Colaborador);
    }

    private static async Task CrearUsuarioAsync(
        UserManager<ApplicationUser> userManager, string email, string password, string rol)
    {
        var usuario = await userManager.FindByEmailAsync(email);
        
        if (usuario == null)
        {
            usuario = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true };
            var resultado = await userManager.CreateAsync(usuario, password);
            if (!resultado.Succeeded)
                throw new InvalidOperationException(
                    $"No se pudo crear {email}: {string.Join("; ", resultado.Errors.Select(e => e.Description))}");
        }

        if (!await userManager.IsInRoleAsync(usuario, rol))
            await userManager.AddToRoleAsync(usuario, rol);
    }
}
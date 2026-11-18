using System.ComponentModel.DataAnnotations;

namespace GestionPersonal.Models;

public class Departamento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del departamento es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(150, ErrorMessage = "La ubicación no puede superar los {1} caracteres.")]
    [Display(Name = "Ubicación")]
    public string? Ubicacion { get; set; }
}
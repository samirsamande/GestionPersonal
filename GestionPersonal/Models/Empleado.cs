using System.ComponentModel.DataAnnotations;

namespace GestionPersonal.Models;

public class Empleado
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El cargo es obligatorio.")]
    [StringLength(80, ErrorMessage = "El cargo no puede superar los {1} caracteres.")]
    public string Cargo { get; set; } = string.Empty;

    public decimal Salario { get; set; }
}
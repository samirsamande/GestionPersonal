using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonal.Models;

public class Empleado
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El cargo es obligatorio.")]
    [StringLength(80, ErrorMessage = "El cargo no puede superar los {1} caracteres.")]
    [Display(Name = "Cargo")]
    public string Cargo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El salario es obligatorio.")]
    [Range(typeof(decimal), "1", "100000", ErrorMessage = "El salario debe estar entre {1} y {2}.")]
    [Column(TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
    [Display(Name = "Salario")]
    public decimal Salario { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un departamento.")]
    [Display(Name = "Departamento")]
    public int DepartamentoId { get; set; }

    [Display(Name = "Departamento")]
    public Departamento? Departamento { get; set; }
}
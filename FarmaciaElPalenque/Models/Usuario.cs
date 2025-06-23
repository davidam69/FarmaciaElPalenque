namespace FarmaciaElPalenque.Models
{
    public class Usuario
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string? nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [Display(Name = "Apellido")]
        public string? apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato de email no es válido")]
        public string? email { get; set; }
        
        [NotMapped]
        [Required(ErrorMessage = "La confirmacion del correo es obligatoria")]
        [EmailAddress]
        [Compare("email", ErrorMessage = "Los correos electrónicos no coinciden")]
        [Display(Name = "Confirmar Email")]
        public string? confirmarEmail { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string? passwordHash { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "La confirmacion de la contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Compare("passwordHash", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string? confirmarPasswordHash { get; set; }

        [Display(Name = "Rol")]
        public string? rol { get; set; } = "Cliente";
    }
}

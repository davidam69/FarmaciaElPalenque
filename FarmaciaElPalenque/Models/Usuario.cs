namespace FarmaciaElPalenque.Models
{
    public class Usuario
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")] // El atributo requerido garantiza que el campo no esté vacío
        [Display(Name = "Nombre")] // Display(Name) se utiliza para mostrar un nombre más amigable en las vistas
        public string? nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [Display(Name = "Apellido")]
        public string? apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")] // ErrorMessage proporciona un mensaje personalizado si el campo no se completa    
        [EmailAddress(ErrorMessage = "El formato de email no es válido")]// EmailAddress valida que el formato del email sea correcto
        public string? email { get; set; }

        [NotMapped] // NotMapped indica que este campo no se debe mapear a la base de datos
        [Required(ErrorMessage = "La confirmacion del correo es obligatoria")]
        [EmailAddress]
        [Compare("email", ErrorMessage = "Los correos electrónicos no coinciden")] // Compare se utiliza para comparar dos campos, en este caso, el email y confirmarEmail
        [Display(Name = "Confirmar Email")]
        public string? confirmarEmail { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)] // DataType.Password indica que este campo es una contraseña y debe ocultar los caracteres ingresados
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

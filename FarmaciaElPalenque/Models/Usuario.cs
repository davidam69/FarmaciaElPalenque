namespace FarmaciaElPalenque.Models
{
    public class Usuario
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-ZñÑ]+(?: [a-zA-ZñÑ]+)*$", ErrorMessage = "El nombre solo puede contener letras, un espacio entre palabras y la letra ñ")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [RegularExpression(@"^[a-zA-ZñÑ]+(?: [a-zA-ZñÑ]+)*$", ErrorMessage = "El apellido solo puede contener letras, un espacio entre palabras y la letra ñ")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres")]
        [Display(Name = "Apellido")]
        public string  apellido { get; set; } = "";

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(252,ErrorMessage = "El email no puede excederse de 252 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El formato del email no es válido.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ser una dirección de email válida")]
        public string? email { get; set; }

        [NotMapped] // NotMapped indica que este campo no se debe mapear a la base de datos
        [Required(ErrorMessage = "La confirmacion del correo es obligatoria")]
        [Compare("email", ErrorMessage = "Los correos electrónicos no coinciden")] // Compare se utiliza para comparar dos campos, en este caso, el email y confirmarEmail
        [Display(Name = "Confirmar Email")]
        public string confirmarEmail { get; set; } = "";

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)] // DataType.Password indica que este campo es una contraseña y debe ocultar los caracteres ingresados
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string passwordHash { get; set; } = "";

        [NotMapped]
        [Required(ErrorMessage = "La confirmacion de la contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Compare("passwordHash", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string confirmarPasswordHash { get; set; } = "";

        [Display(Name = "Rol")]
        public string? rol { get; set; } = "Cliente";
    }
}

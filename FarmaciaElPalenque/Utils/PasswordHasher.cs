namespace FarmaciaElPalenque.Utils
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // ✅ VALIDAR que el password no sea null o vacío
            if (string.IsNullOrEmpty(password))
            {
                // Puedes lanzar una excepción más específica
                throw new ArgumentException("El password no puede estar vacío");
            }
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            var newHash = HashPassword(password);
            return newHash == hashedPassword;
        }
    }
}

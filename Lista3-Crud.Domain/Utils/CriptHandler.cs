namespace Lista3_Crud.Domain.Utils
{
    public static class CriptHandler
    {
        public static string Encrypt(string value)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }

        public static bool ValuesAreEqual(string value, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
    }
}
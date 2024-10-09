using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace MinhaLoja
{
    public class Settings
    {
        public static string Secret = "dfDF43$43982392394329842934b23929348fdf_23FDfdf$D#d43";

        public static string GenerateHash(string password)//passa a senha para o metodo
        {
            //cripto 
            byte[] salt = Encoding.ASCII.GetBytes("1234568973165");//criptografa passando o segredo

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password : password , 
                salt : salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount:100000,
                numBytesRequested:32
                ));
            return hashed;
        }

    }
}

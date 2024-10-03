using ApiAuth.Models;

namespace ApiAuth.Repositories
{
    // Classe para representar o banco de dados
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {

            var users = new List<User>
            {
                // Usuários com login, senha e role
                new User {Id = 1, UserName = "fulano", Password = "fulano", Role="gerente"},
                new User { Id = 2, UserName = "ciclano", Password = "ciclano", Role = "estagiario" }
            };

            return users.FirstOrDefault(x =>
                x.UserName.ToLower() == username.ToLower() &&
                x.Password.ToLower() == password.ToLower());
        }
    }
}
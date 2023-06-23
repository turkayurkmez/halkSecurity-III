using ClaimBasedAuth.Models;

namespace ClaimBasedAuth.Services
{
    public class UserService
    {

        private List<User> users = new List<User>
        {
            new User { Id = 1, UserName="turkay", Password="123", Role="admin", Email="abc@def.com"},
            new User { Id = 2, UserName="buse", Password="123", Role="editor", Email="abc@def.com"},
            new User { Id = 3, UserName="alperen", Password="123", Role="client", Email="abc@def.com"},

        };
        public User ValidateUser(string username, string password)
        {
            return users.SingleOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINancy_APIREST
{
    public partial class User
    {
        public static void UserCreation()
        {
            using (var context = new UserContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                List<User> users = new List<User>();
                Random random = new Random();
                for (int i = 1; i <= 20; i++)
                {
                    User currentUser = new User()
                    {
                        Login = "User" + i,
                        Password = "1234"
                    };
                    users.Add(currentUser);
                }

                context.AddRange(users);
                context.SaveChanges();
            }
        }


    }
}

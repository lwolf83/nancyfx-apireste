using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINancy_APIREST
{
    public class UserModule:NancyModule
    {

        public UserModule()
        {
            // Get JSON about a given user
            Get("/users/{UserId:Guid}", param => ReturnUserData(param.UserId));

            // Delete an existing user            
            Get("/users/delete/{UserGuid:Guid}", param => DeleteUser(param.UserGuid));

            // Add a new user
            Get("/users/new/login={login}&password={password}", param => PutNewUser(param.login, param.password));

            // Authentify a user
            Get("/authentify/login={login}&password={password}", param => AuthentifyUser(param.login, param.password));
        }



        public string ReturnUserData(Guid userId)
        {
            var context = new UserContext();
            User user = context.User.Where(u => u.UserId == userId).FirstOrDefault();

            string res;
            if(user == null)
            {
                res = JsonConvert.SerializeObject("Unknow user");
            }
            else
            {
                user.Password = "***";
                res = JsonConvert.SerializeObject(user);
            }
            
            return res;
        }

        public string DeleteUser(Guid userId)
        {
            var context = new UserContext();
            User user = context.User.Where(u => u.UserId == userId).FirstOrDefault();
            context.User.Remove(user);
            context.SaveChanges();

            string res = JsonConvert.SerializeObject("User " + userId + " deleted");
            return res;
        }

        public string PutNewUser(string login, string password)
        {
            var context = new UserContext();
            User newUser = new User { Login = login, Password = password };
            context.User.Add(newUser);
            context.SaveChanges();

            string res = JsonConvert.SerializeObject("User " + login + " added");
            return res;
        }

        public string AuthentifyUser(string login, string password)
        {
            var context = new UserContext();
            User currentUser = context.User.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            string response;
            if(currentUser == null)
            {
                response = "User " + login + " not authorized.";
            }
            else
            {
                response = "User " + login + " authorized.";
            }
            string res = JsonConvert.SerializeObject(response);
            return res;
        }
    }
}

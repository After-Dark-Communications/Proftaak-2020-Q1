using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Repository
{
    public class LoginRepository
    {
        private readonly IHttpContextAccessor contextAccessor;

        public LoginRepository()
        {

        }

        public LoginRepository(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void SetLoginSession(string username)
        {
            contextAccessor.HttpContext.Session.SetString("UserName", username);
        }

        public void RemoveLoginSession()
        {
            contextAccessor.HttpContext.Session.Remove("Username");
            contextAccessor.HttpContext.Session.Remove("UserId");
        }
    }
}

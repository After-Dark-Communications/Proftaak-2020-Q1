using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

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

        public void SetLoginSession(string username, string permission)
        {
            contextAccessor.HttpContext.Session.SetString("username", username);
            contextAccessor.HttpContext.Session.SetString("Permission", permission);
        }

        public void RemoveLoginSession()
        {
            contextAccessor.HttpContext.Session.Remove("Username");
            contextAccessor.HttpContext.Session.Remove("UserId");
        }

        public string GetLoginSession()
        {
             return contextAccessor.HttpContext.Session.GetString("Username");
        }
    }
}

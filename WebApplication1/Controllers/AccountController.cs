using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using AutoMapper;
using DTO;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        public AccountController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel UserModel)
        {
            UserDTO User = _mapper.Map<UserDTO>(UserModel);
            return View();
        }


    }
}
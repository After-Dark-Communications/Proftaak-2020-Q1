using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using AutoMapper;
using DTO;
using Logic;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly User _user;
        private readonly LoginRepository _loginRepository;
        private readonly UserCollection _userCollection;

        public AccountController(IMapper mapper, User user, LoginRepository login, UserCollection userCollection)
        {
            _mapper = mapper;
            _user = user;
            _loginRepository = login;
            _userCollection = userCollection;
        }

        public IActionResult Login()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel UserModel)
        {
            //TODO: exceptionHandling
            UserDTO User = _mapper.Map<UserDTO>(UserModel);
            ViewBag.ShowTopBar = false;
            if (ModelState.IsValid)
            {
                UserDTO result = _user.Login(User);

                if (result != null)
                {
                    _loginRepository.SetLoginSession(result.UserName, result.Permission);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LatestMessage = "Login mislukt: Gebruikersnaam of wachtwoord is incorrect.";
                    return View();
                }
            }
            ViewBag.LatestMessage = "Login Mislukt: één of meer Velden zijn niet correct ingevuld";
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        [HttpPost]

        public IActionResult Register(UserViewModel userModel)
        {
            ViewBag.ShowTopBar = false;
            UserDTO User = _mapper.Map<UserDTO>(userModel);
            if (ModelState.IsValid)
            {
                _userCollection.RegisterUser(User);
            }
            else
            {
                ViewBag.LatestMessage = "Nieuwe gebruiker registreren mislukt: verplichte velden zijn niet correct ingevuld.";
            }
            return View();
        }

        public IActionResult Logout()
        {
            _loginRepository.RemoveLoginSession();
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logic;
using WebApplication1.Models;
using DTO;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly Logic.RepairService _repairservice;
        private readonly Logic.CleaningService _cleaningservice;
        public ServiceController(IMapper mapper, RepairService repairservice, CleaningService cleaningservice)
        {
            _mapper = mapper;
            _repairservice = repairservice;
            _cleaningservice = cleaningservice;
        }

        public IActionResult Repairs()
        {
            ViewBag.CurrentPage = "Repairs";
            var repairLogs = _repairservice.GetRepairHistory();

            _mapper.Map<List<ServiceViewModel>>(repairLogs);
            return View(repairLogs);
        }

        public IActionResult Cleaning(ServiceViewModel svModel)
        {
            ViewBag.CurrentPage = "Cleaning";
            var cleaningLogs = _cleaningservice.GetCleaningLogs();

            svModel.AllServices = _mapper.Map<List<ServiceViewModel>>(cleaningLogs);
            return View(svModel);
        }
    }
}
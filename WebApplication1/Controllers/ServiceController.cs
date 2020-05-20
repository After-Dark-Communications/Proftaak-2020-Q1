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
            var repairLogs = _repairservice.GetRepairHistory();
            foreach(RepairLogDTO repair in repairLogs)
            {
                _mapper.Map<ServiceViewModel>(repair);
            }
            return View(repairLogs);
        }

        public IActionResult Cleaning()
        {
            var cleaningLogs = _cleaningservice.GetCleaningLogs();
            foreach (CleaningLogDTO clean in cleaningLogs)
            {
                _mapper.Map<ServiceViewModel>(clean);
            }
            return View(cleaningLogs);
        }
    }
}
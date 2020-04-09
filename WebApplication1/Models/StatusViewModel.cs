using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;

namespace WebApplication1.Models
{
    public class StatusViewModel
    {
        public int StatusId { get; set; }
        public TramStatus Status { get; set; }
        public string Description { get; set; }
        public StatusViewModel()
        {

        }
        public StatusViewModel(TramStatus status)
        {
            Status = status;
        }

        public StatusViewModel(TramStatus status, string description)
        {
            Status = status;
            Description = description;
        }
    }
}

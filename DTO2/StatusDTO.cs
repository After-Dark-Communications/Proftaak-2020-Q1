using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public TramStatus Status { get; set; }
        public string Description { get; set; }
        public StatusDTO()
        {

        }
        public StatusDTO(TramStatus status)
        {    
           Status = status;     
        }

        public StatusDTO(TramStatus status, string description)
        {
            Status = status;
            Description = description;
        }
    }
}

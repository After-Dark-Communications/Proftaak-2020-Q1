using System;
using System.Collections.Generic;
using System.Text;
using Services;

namespace DTO
{
    class StatusDTO
    {
        public TramStatus Status { get; private set; }
        public string Description { get; private set; }

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

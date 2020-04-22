using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PermissionDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

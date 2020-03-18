using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class TramDal
    {
        [Key]
        public int Id;
        public int SerialNumber;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TestClasses
{
    public class Sector
    {
        private List<Tram> _tramList;

        public List<Tram> Trams {  get { return _tramList; }  }

        public Sector()
        {
            _tramList = new List<Tram>();
        }

        public Tram GetTram(string tramNumber)
        {
            foreach (Tram tram in _tramList)
            {
                if (tram.Number == tramNumber)
                {
                    return tram;
                }
            }

            return null;
        }

        public void AddTram(Tram tram)
        {
            _tramList.Add(tram);
        }
    }
}

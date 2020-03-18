using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TestClasses
{
    public class TestSector
    {
        private List<TestTram> _tramList;

        public List<TestTram> Trams {  get { return _tramList; }  }

        public TestSector()
        {
            _tramList = new List<TestTram>();
        }

        public void AddTram(TestTram tram)
        {
            _tramList.Add(tram);
        }
    }
}

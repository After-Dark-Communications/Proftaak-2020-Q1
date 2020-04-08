using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace StoreTramInRemiseTests
{
    [TestClass]
    public class StoreTramTests
    {
        [TestMethod]
        public void AddTramToSector()
        {
            Sector sector = new Sector();
            Assert.IsTrue(sector.CheckIfSectorIsEmpty());
        }
        [TestMethod]
        public void AddTramToSectorWhileFull()
        {
            Tram tram = new Tram();
            Sector sector = new Sector();
            if (sector.CheckIfSectorIsEmpty())
            {
                sector.AddTram(tram);
            }
            Assert.IsFalse(sector.CheckIfSectorIsEmpty());
            
            
        }

        [TestMethod]
        public void checkIfTramGetsStored()
        {
            Tram tram = new Tram();
            Sector sector = new Sector();
            if (sector.CheckIfSectorIsEmpty())
            {
                sector.AddTram(tram);
            }

            Assert.AreEqual(tram, sector.GetTram());
        }

        [TestMethod]

        public void AddTramToSectorThroughTrack()
        {
            //check if a tram gets added to a sector through tram
        }

        [TestMethod]
        public void AddTramToSectorThroughAllocationManager()
        {
            //check if a tram gets added to a sector through the allocationmanager
        }

        [TestMethod]
        public void AddTramToSectorThroughDepot()
        {
            //check if a tram gets added to a sector through the depot
        }



    }
}

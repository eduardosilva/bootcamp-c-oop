using System.Collections.Generic;
using System.Linq;
using Exemplo;
using Exemplo.Enums;
using Exemplo.Models.Cinema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exemplo.tests
{
    [TestClass]
    public class FilmTest : TestBase
    {
        private Application applicationInstance;
        
        public FilmTest() : base()
        {
            Setup();
        }

        public override void Setup()
        {
            applicationInstance = new Application();
        }

        [TestMethod]
        public void AddFilm_ReturnsAddedFilm()
        {
            var titanic = new Film()
            {
                Title = "Titanic",
                Genres = new List<Genre>() { Genre.Drama },
                Film3D = Option3D.With3D,
                MinimumAge = 8,
                ReleaseDate = new System.DateTime(1998, 1, 16),
                RunningTime = new System.TimeSpan(3, 15, 0)
            };

            applicationInstance.AddFilm(titanic);

            Assert.IsTrue(applicationInstance.Films.Count() == 1);
            Assert.AreEqual(applicationInstance.Films.FirstOrDefault(), titanic);
        }
    }
}

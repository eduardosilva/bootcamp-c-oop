using System.Collections.Generic;
using System.Linq;
using Exemplo;
using Exemplo.Enums;
using Exemplo.Models.Cinema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exemplo.tests
{
    [TestClass]
    public class FilmTest
    {
        [TestMethod]
        public void AddFilm_ReturnsAddedFilm()
        {
            var instance = new Application();
            var titanic = new Film()
            {
                Title = "Titanic",
                Genres = new List<Genre>(){Genre.Drama},
                Film3D = Option3D.With3D,
                MinimumAge = 8,
                ReleaseDate = new System.DateTime(1998,1,16),
                RunningTime = new System.TimeSpan(3,15,0)
            };
            
            instance.AddFilm(titanic);

            Assert.IsTrue(instance.Films.Count() == 1);
            Assert.AreEqual(instance.Films.FirstOrDefault(), titanic);
        }
    }
}

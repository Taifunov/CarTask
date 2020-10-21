using System;
using System.Collections.Generic;
using CarTestTask.Models;
using CarTestTask.Tests.DatabaseContext;

namespace CarTestTask.Tests.DbContext
{
    public static class DbContextExtensions
    {
        public static void Seed(this DataContext dbContext)
        {
            // Add entities for DbContext instance
            dbContext.AddRange(GetTestCars());
           
            dbContext.SaveChanges();
        }

        private static List<Car> GetTestCars()
        {
            #region Test Cars
            return new List<Car>()
            {
                new Car
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Cadillac",
                    Description = "Cadillac was named after the great French explorer Antoine Laumet de La Mothe, Sieur de Cadillac, who founded Detroit, Michigan in 1701.\r\n\r\nCadillac is one of the earliest car brands in the US. Founded in 1902, it is known for manufacturing luxury cars. However, the company was later taken over by General Motors in 1909."
                },
                new Car
                {
                    Id = "5f8d5cd949413db2654df1db",
                    Name = "Volvo v.33",
                    Description = "Best car"
                },
                new Car
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Audi",
                    Description = "German Engineer August Horch founded the company August Horch & Cie. Motorwagenwerke AG, in 1904. Due to misunderstandings among partners, Horch left the company and formed his own company August Horch Automobilwerke GmbH in 1909. After a German court pointing Horch of trademark infringement, he was forced to change the name."
                },
                new Car
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "BMW",
                    Description = "BMW started its operation in 1912, is formed with the merger of 3 German companies.\r\n\r\nThe company’s name stands for Bayerische Motoren Werke AG, which means Bavarian Motor Works in German. After easing World War I restrictions, it became a full-fledged automobile company by manufacturing its first car “Dixi”, under license from the Austin Motor Company."
                },
                new Car
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Datsun",
                    Description = "Datsun is a Japanese company associated with the production of small and affordable cars.\r\n\r\nEstablished in 1931, the name “Datson” was picked from surname initials of 3 partners- Kenjiro Den, Rokuro Aoyama, Meitaro Takeuchi. After 3 years, the company was taken over by Nissan. It has dropped “son”, as it means ‘loss’ in Japan and added “sun”- which has positive associations.\r\n\r\nNissan phased out Datsun in 1986 and in 2013 successfully re-launched the brand in developing and emerging economies."
                },
                new Car
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Ferrari",
                    Description = "Everyone loves to own a Ferrari, a stylish and aerodynamically designed sleek sports car. The name Ferrari is christened after its Italian founder Enzo Ferrari, who was an official Alto race driver in 1924. In 1939, he quitted racing to build his own company.\r\n\r\nWithin one year, he built 1500 cm3 8-Cylinder 815 Spider, winning its first Grand Prix 1947. Today, Ferrari is synonymous with design, quality, style, and luxury, for both its commercial and Formula One cars."
                },
                new Car
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Ford",
                    Description = "Henry Ford founded the Ford Motors Company in 1903, in Detroit, Michigan.\r\n\r\nFord has left his first company Cadillac, and started his own car company with $28,000 investment.\r\n\r\nHe perfected the mass production of cars by introducing moving assembly lines. This gave him the edge to cut the cost and offer an affordable car to the American middle class. His famous mass production car of 1908, Model T, sold more than millions over the next 20 years.\r\n\r\nLater on, Ford made several acquisitions, including Volvo, Troller and FPV brands."
                }
            };
            #endregion
        }
    }
}

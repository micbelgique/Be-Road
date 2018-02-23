using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PublicService.Dal
{
    public class PSDbInitializer : DropCreateDatabaseAlways<PSContext>
    {
        private Random rand = new Random();
        private string[] names = { "Arlen", "Artie", "Gray", "Guard", "Ladden", "Mace", "Mark", "Seno", "Jana", "Kim", "Lydia" };
        private string[] reasons = { "Speeding", "Alcohol test", "Crazy driving", "Fun", "Abuse of power" };

        protected override void Seed(PSContext context)
        {
            context.Cars.Add(GenerateNewCar("Michaël", "Fiat", "1-KGG-695"));
            context.Cars.Add(GenerateNewCar("Wilson", "Nissan", "1-EBD-684"));
            context.Cars.Add(GenerateNewCar("Pierre", "Porche", "1-OZA-014"));
            context.Cars.Add(GenerateNewCar("Raph", "Citroën", "1-VNK-646"));
            context.Cars.Add(GenerateNewCar("Fred", "Mercedes", "1-ZAR-755"));
            context.Cars.Add(GenerateNewCar("Thomas", "BMW", "1-PER-124"));
            context.Cars.Add(GenerateNewCar("Martine", "Audi", "1-DFV-862"));
            context.Cars.Add(GenerateNewCar("Timo", "Dacia", "1-DSF-356"));
            context.Cars.Add(GenerateNewCar("Ludoo", "Peugot", "1-SON-562"));
        }

        private Car GenerateNewCar(string owner, string brand, string numberPlate)
        {
            return new Car()
            {
                Owner = new Data()
                {
                    Name = owner,
                    AccessInfos = GenerateRandomAccessInfo(20)
                },
                Brand = new Data() {
                    Name = brand,
                    AccessInfos = GenerateRandomAccessInfo(50)
                },
                NumberPlate = new Data()
                {
                    Name = numberPlate,
                    AccessInfos = GenerateRandomAccessInfo(80)
                }
            };
        }

        private List<AccessInfo> GenerateRandomAccessInfo(int probability)
        {
            var infos = new List<AccessInfo>();
            for(int i = 0; i < 3; i++)
            {
                if (rand.Next(100) < probability)
                {
                    infos.Add(new AccessInfo() {
                        Name = names[rand.Next(names.Length)],
                        Date = new DateTime(rand.Next(2000, 2018), rand.Next(1, 12), rand.Next(1, 28)),
                        Reason =  reasons[rand.Next(reasons.Length)]
                    });
                }
            }
            return infos;
        }
    }
}
﻿using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PublicService.Dal
{
    public class PSDbInitializer : DropCreateDatabaseIfModelChanges<PSContext>
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


            context.MicTrainees.Add(GenerateNewTrainee("Komsomolets", "Rosy", "Gumb", "Kazakhstan", "25"));
            context.MicTrainees.Add(GenerateNewTrainee("Yanggu", "Vito", "Mossop", "South Korea", "22"));
            context.MicTrainees.Add(GenerateNewTrainee("Foso", "Callie", "Glaze", "Ghana", "18"));
            context.MicTrainees.Add(GenerateNewTrainee("Gostagayevskaya", "Kyla", "Turfin", "Russia", "30"));
            context.MicTrainees.Add(GenerateNewTrainee("Tabon", "Eula", "Jenney", "Philippines", "31"));
            context.MicTrainees.Add(GenerateNewTrainee("Bamusso", "Lyn", "Navarro", "Cameroon", "46"));
            context.MicTrainees.Add(GenerateNewTrainee("Roi Et", "Byrann", "Antal", "Thailand", "13"));
            context.MicTrainees.Add(GenerateNewTrainee("Ortigueira", "Rossie", "Marcussen", "Brazil", "16"));
            context.MicTrainees.Add(GenerateNewTrainee("Sakākā", "Lila", "Cartlidge", "Saudi Arabia", "56"));
            context.MicTrainees.Add(GenerateNewTrainee("Ikey", "Frances", "Maneylaws", "Russia", "35"));
            context.MicTrainees.Add(GenerateNewTrainee("Trojanów", "Carmen", "Shay", "Poland", "57"));
            context.MicTrainees.Add(GenerateNewTrainee("Muyinga", "Virgie", "Monkton", "Burundi", "35"));
            context.MicTrainees.Add(GenerateNewTrainee("Longquan", "Amandi", "Rieger", "China", "68"));
            context.MicTrainees.Add(GenerateNewTrainee("Belyye Stolby", "Chan", "Roston", "Russia", "5"));
            context.MicTrainees.Add(GenerateNewTrainee("Brasília", "Glen", "Reignould", "Brazil", "54"));
            context.MicTrainees.Add(GenerateNewTrainee("Fagersta", "Carrissa", "Dibnah", "Sweden", "43"));
            context.MicTrainees.Add(GenerateNewTrainee("Greytown", "Gwendolin", "Dikles", "South Africa", "57"));
            context.MicTrainees.Add(GenerateNewTrainee("Mairiporã", "Kasper", "Fleg", "Brazil", "24"));
            context.MicTrainees.Add(GenerateNewTrainee("Castanheira de Pêra", "Zebadiah", "Di Roberto", "Portugal", "64"));
            context.MicTrainees.Add(GenerateNewTrainee("San Roque", "Tommy", "Oppery", "Philippines", "34"));
            context.MicTrainees.Add(GenerateNewTrainee("Socorro", "Traver", "Milland", "Brazil", "54"));
            context.MicTrainees.Add(GenerateNewTrainee("Goyty", "Sapphire", "Dunbobbin", "Russia", "54"));
            context.MicTrainees.Add(GenerateNewTrainee("Qaşr al Farāfirah", "Jaquith", "Andrieu", "Egypt", "34"));
            context.MicTrainees.Add(GenerateNewTrainee("Skövde", "Dougy", "Petrashov", "Sweden", "75"));
            context.MicTrainees.Add(GenerateNewTrainee("António Enes", "Noe", "Sapson", "Mozambique", "24"));
            context.MicTrainees.Add(GenerateNewTrainee("Puck", "Ettie", "Figgess", "Poland", "87"));
            context.MicTrainees.Add(GenerateNewTrainee("Ngadri", "Danielle", "Pinor", "Indonesia", "35"));
        }

        private MicTrainee GenerateNewTrainee(string ln, string fn, string loc,  string nat, string age)
        {
            return new MicTrainee()
            {
                Age = new Data() { Name = age },
                FirstName = new Data() { Name = fn },
                LastName = new Data() { Name = ln },
                Locality = new Data() { Name = loc },
                Nationality = new Data() { Name = nat }
            };
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
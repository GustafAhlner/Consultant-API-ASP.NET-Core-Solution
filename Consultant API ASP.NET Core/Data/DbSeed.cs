using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consultant_API_ASP.NET_Core.Data.Entities;

namespace Consultant_API_ASP.NET_Core.Data
{
    public class DbSeed
    {
        public static void Seed(ConsultantContext context)
        {
            context.Database.EnsureCreated();

            if (context.consultants.Any())
            {
                return;   // DB has been seeded
            }
            List<Competence> newCompetences = CreateFakeToDatabaseCompetences();
            context.competences.AddRange(newCompetences);
            context.SaveChanges();

            List<Consultant> newConsultants = CreateFakeToDatabaseConsultants();
            context.consultants.AddRange(newConsultants);
            context.SaveChanges();

            List<ConsultantCompetence> newCompetenceConsultants = CreateFakeToDatabaseCompetenceConsultant(newCompetences, newConsultants);
            context.ConsultantCompetences.AddRange(newCompetenceConsultants);
            context.SaveChanges();
        }

        private static List<ConsultantCompetence> CreateFakeToDatabaseCompetenceConsultant
                        (List<Competence> newCompetences, List<Consultant> newConsultants)
        {
            List<ConsultantCompetence> newList = new List<ConsultantCompetence>();
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[0].ConsultantId, CompetenceId = newCompetences[0].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[0].ConsultantId, CompetenceId = newCompetences[1].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[1].ConsultantId, CompetenceId = newCompetences[2].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[2].ConsultantId, CompetenceId = newCompetences[2].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[2].ConsultantId, CompetenceId = newCompetences[3].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[2].ConsultantId, CompetenceId = newCompetences[4].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[2].ConsultantId, CompetenceId = newCompetences[5].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[4].ConsultantId, CompetenceId = newCompetences[1].CompetenceId});
            newList.Add(new ConsultantCompetence { ConsultantId = newConsultants[4].ConsultantId, CompetenceId = newCompetences[6].CompetenceId});
            return newList;
        }

        private static List<Consultant> CreateFakeToDatabaseConsultants()
        {
            List<Address> newAddresses = CreateFakeToDatabaseAddresses();
            List<Consultant> newList = new List<Consultant>();
            newList.Add(new Consultant
            {
                NameFirst = "Alfabet",
                NameSecond = "Betapet",
                Telephone = "+46731112233",
                Email = "abc@def.ghi",
                ImageURL = "consultant1.jpg",
                BirthDate = new DateTime(1985, 3, 1),
                Addresses =
                {
                    newAddresses[0]
                }
            });
            newList.Add(new Consultant
            {
                NameFirst = "Harry",
                NameSecond = "Potter",
                Telephone = "+51731112233",
                Email = "hp@mor.hgwts",
                ImageURL = "consultant2.jpg",
                BirthDate = new DateTime(1981, 7, 11),
                Addresses =
                {
                    newAddresses[1]
                },
            });
            newList.Add(new Consultant
            {
                NameFirst = "Nemo",
                NameSecond = "",
                Telephone = "",
                Email = "fish@def.ghi",
                ImageURL = "consultant3.jpg",
                BirthDate = new DateTime(2005, 3, 1),
                Addresses =
                {
                    newAddresses[2]
                }
            });
            newList.Add(new Consultant
            {
                NameFirst = "Fatima",
                NameSecond = "Andersson",
                Telephone = "+46733332233",
                Email = "fatima.andersson@mail.se",
                ImageURL = "consultant4.jpg",
                BirthDate = new DateTime(1977, 1, 6),
                Addresses =
                {
                    newAddresses[3]
                }
            });
            newList.Add(new Consultant
            {
                NameFirst = "Pelle",
                NameSecond = "Pellson",
                Telephone = "",
                Email = "",
                ImageURL = "consultant5.jpg",
                BirthDate = new DateTime(1999, 12, 31),
                Addresses =
                {
                    newAddresses[4]
                }
            });
            return newList;
        }
        private static List<Address> CreateFakeToDatabaseAddresses()
        {
            List<Address> newList = new List<Address>();
            newList.Add(new Address
            {
                AddressLine = "Moscow Street 1",
                City = "Moscow",
                CountryRegion = "Russia"
            });
            newList.Add(new Address
            {
                AddressLine = "Fish Bowl Alley",
                City = "Sydney",
                CountryRegion = "Australia"
            });
            newList.Add(new Address
            {
                AddressLine = "Great Coral Reef 75",
                City = "The Sea",
                CountryRegion = "Australia"
            });
            newList.Add(new Address
            {
                AddressLine = "4 Privet Drive",
                City = "Little Whinging",
                CountryRegion = "England"
            });
            newList.Add(new Address
            {
                AddressLine = "105; DROP TABLE Contacts",
                City = "",
                CountryRegion = ""
            });
            return newList;
        }

        private static List<Competence> CreateFakeToDatabaseCompetences()
        {
            List<Competence> newList = new List<Competence>();
            newList.Add(new Competence
            {
                CompetenceName = "C++",
                CompetenceLevel = 2
            });
            newList.Add(new Competence
            {
                CompetenceName = "Angular",
                CompetenceLevel = 1
            });
            newList.Add(new Competence
            {
                CompetenceName = "Entity Framework Core",
                CompetenceLevel = 2
            });
            newList.Add(new Competence
            {
                CompetenceName = "C#",
                CompetenceLevel = 3
            });
            newList.Add(new Competence
            {
                CompetenceName = "Front-End Development",
                CompetenceLevel = 1
            });
            newList.Add(new Competence
            {
                CompetenceName = "Front-End Development",
                CompetenceLevel = 2
            });
            newList.Add(new Competence
            {
                CompetenceName = "Front-End Development",
                CompetenceLevel = 3
            });
            return newList;
        }
    }
}

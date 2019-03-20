using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consultant_API_ASP.NET_Core.Data.Entities;

namespace Consultant_API_ASP.NET_Core.Data
{
    public interface IConsultantRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Consultant
        Task<Consultant[]> GetAllConsultantsAsync(bool includeAddresses = false);
        Task<Consultant> GetConsultantAsync(int id, bool includeAddresses = false);

        // Address
        Task<Address> GetAddressAsync(int addressid);
        Task<Address[]> GetAllAddressesAsync();

        // Competence
        Task<Competence> GetCompetenceAsync(int competenceid);
        Task<Competence[]> GetAllCompetencesAsync();
        Task<Competence[]> GetAllCompetencesForConsultantAsync(int consultantid);
    }
}

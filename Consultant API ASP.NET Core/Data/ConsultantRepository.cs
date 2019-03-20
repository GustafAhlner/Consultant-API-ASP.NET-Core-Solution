using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consultant_API_ASP.NET_Core.Data.Entities;

namespace Consultant_API_ASP.NET_Core.Data
{
    public class ConsultantRepository : IConsultantRepository
    {
        private readonly ConsultantContext context;

        public ConsultantRepository(ConsultantContext context)
        {
            this.context = context;
        }

        //--------- General --------
        
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }

        //--------- Consultant --------

        public async Task<Consultant[]> GetAllConsultantsAsync(bool includeAddress = false)
        {
            IQueryable<Consultant> query = context.consultants;
            if (includeAddress) query = query.Include(c => c.Addresses);
            return await query.ToArrayAsync();
        }

        public async Task<Consultant> GetConsultantAsync(int id, bool includeAddress = false)
        {
            var query = context.consultants
                .Where(t => t.ConsultantId == id);
            if (includeAddress) query = query.Include(c => c.Addresses);
            return await query.FirstOrDefaultAsync();
        }

        //--------- Address --------

        public async Task<Address[]> GetAllAddressesAsync()
        {
            IQueryable<Address> query = context.addresses;
            return await query.ToArrayAsync();
        }

        public async Task<Address> GetAddressAsync(int addressid)
        {
            var query = context.addresses
                 .Where(t => t.AddressId == addressid);
            return await query.FirstOrDefaultAsync();
        }

        //--------- Competence --------

        public async Task<Competence[]> GetAllCompetencesAsync()
        {
            IQueryable<Competence> query = context.competences;
            return await query.ToArrayAsync();
        }

        public async Task<Competence[]> GetAllCompetencesForConsultantAsync(int consultantid)
        {
            var competenceids = await context.ConsultantCompetences
                .Where(x => x.ConsultantId == consultantid)
                .Select(x => x.CompetenceId)
                .ToListAsync();

            IQueryable<Competence> query = context.competences
               .Where(x => competenceids.Contains(x.CompetenceId));
            return await query.ToArrayAsync();
        }

        public async Task<Competence> GetCompetenceAsync(int competenceid)
        {
            var query = context.competences
                 .Where(t => t.CompetenceId == competenceid);
            return await query.FirstOrDefaultAsync();
        }
    }
}

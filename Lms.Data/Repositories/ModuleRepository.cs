using Lms.Core.Repositories;
using Lms.Core.Entities;
using Lms.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{
    public class ModuleRepository : IModuleRepository {
        private LmsApiContext db;

        public ModuleRepository(LmsApiContext context) {
            this.db = context;
        }

        /*
         * The Module DbSet is declared as nullable in LmsApiContext
         */
        public async Task<IEnumerable<Module>?> GetAllModules() {
            if (db.Module == null) return null;
           
            return await db.Module.ToListAsync();
        }

        public async Task<Module?> GetModule(int? id) {
            if (db.Module == null) return null;
            
            return await db.Module.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Module?> GetModuleByName(string moduleName) {
            if (db.Module == null) return null;

            return await db.Module.FirstOrDefaultAsync(m => m.Title == moduleName);
        }

        /*
         * Both GetModule and FindAsync takes an id and returns a Module, what's the difference ??
         */
        public Task<Module?> FindAsync(int? id) {
            return null!;
        }

        public async Task<bool> AnyAsync(int? id) {
            if (db.Module == null) return false;

            return await db.Module.AnyAsync(m => m.Id == id);
        }

        public void Add(Module module) {
            if (db.Module == null) return;

            db.Module.Add(module);
        }

        public void Update(Module module) {
            if (db.Module == null) return;

            db.Module.Update(module);
            db.SaveChanges();
        }

        public void Remove(Module module) {
            if (db.Module == null) return;

            db.Module.Remove(module);
            db.SaveChanges();
        }
    }
}

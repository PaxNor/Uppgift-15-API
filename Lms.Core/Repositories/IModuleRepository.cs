using Lms.Core.Entities;

namespace Lms.Core.Repositories
{
    public interface IModuleRepository
    {
        public Task<IEnumerable<Module>?> GetAllModules();
        public Task<Module?> GetModule(int? id);
        public Task<Module?> FindAsync(int? id);
        public Task<bool> AnyAsync(int? id);
        public void Add(Module module);
        public void Update(Module module);
        public void Remove(Module module);
    }
}

using Caalinder.Controllers.Interfaces;
using Caalinder.Data.Context;
using Caalinder.Models;

namespace Caalinder.Data.Repositories
{
    public class UsuarioRepository : GenericRepository<UserModel>, IUsuarioRepository
    {
        private ApplicationContext _context;
        public UsuarioRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
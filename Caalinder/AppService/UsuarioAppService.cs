using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Caalinder.Models;
using Caalinder.Controllers.Interfaces;
using Caalinder.Service;

namespace Caalinder.AppService
{
    public class UsuarioAppService : GenericAppService, IUsuarioAppService
    {
        private readonly IGenericService<UserModel> _usuarioService;

        public UsuarioAppService(IUnitOfWork uow, IGenericService<UserModel> usuarioService)
            : base(uow)
        {
            _usuarioService = usuarioService;
        }

        public List<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegisterViewModel> Get(Expression<Func<RegisterViewModel, bool>> filter = null, Expression<Func<IQueryable<RegisterViewModel>, IOrderedQueryable<RegisterViewModel>>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegisterViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public RegisterViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> Insert(RegisterViewModel obj)
        {
            throw new NotImplementedException();
        }

        public List<string> Update(RegisterViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
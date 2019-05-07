﻿using Caalinder.Models;
using Caalinder.AppService.Interfaces;
using Caalinder.Controllers.Interfaces;
using Caalinder.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Caalinder.AppService
{
    public class HorseAppService : GenericAppService, IHorseAppService
    {
        private readonly IGenericService<HorseModel> _horseService;
        private readonly IGenericService<UserModel> _userService;

        public HorseAppService(IUnitOfWork uow, IGenericService<HorseModel> horseService, IGenericService<UserModel> userService)
            : base(uow)
        {
            _horseService = horseService;
            _userService = userService;
        }

        public List<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HorseViewModel> Get(Expression<Func<HorseViewModel, bool>> filter = null, Expression<Func<IQueryable<HorseViewModel>, IOrderedQueryable<HorseViewModel>>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HorseViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public HorseViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserByName(string name)
        {
            Expression<Func<UserModel, bool>> filter = (UserModel p) => p.name == name;
            var result = _userService.Get(filter, null, "");
            var user = result.FirstOrDefault();
            return ((user == null) ? null : (user));
        }
        public List<string> Insert(HorseViewModel obj)
        {
            List<string> errors = new List<string>();
            try
            {
                HorseModel horse = AutoMapper.Mapper.Map<HorseViewModel, HorseModel>(obj);
                UserModel user = new UserModel();
                user.name = HttpContext.Current.User.Identity.Name;
                user = GetUserByName(user.name);
                horse.User = user;
                if (errors?.Count > 0)
                {
                    return errors;
                }
                else
                {
                    BeginTransaction();
                    errors = _horseService.Insert(horse);
                    if (errors?.Count() == 0)
                    {
                        SaveChanges();
                        Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Rollback();
                errors.Add("Ocorreu um erro no Cadastro");
            }
            return errors;
        }

        public List<string> Update(HorseViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
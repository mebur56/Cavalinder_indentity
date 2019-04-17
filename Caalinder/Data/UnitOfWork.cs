using Caalinder.Data.Context;
using Caalinder.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Caalinder.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private bool _disposed = false;
        private DbContextTransaction _transaction;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            if (!_disposed)
                this._transaction = _context.Database.BeginTransaction();
            _disposed = false;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Commit(bool dispose = true)
        {
            if (_transaction != null)
                _transaction.Commit();
            _transaction = null;
            if (dispose)
                Dispose();
        }

        public void Rollback(bool dispose = true)
        {
            if (_transaction != null)
                _transaction.Rollback();
            _transaction = null;
            if (dispose)
                Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
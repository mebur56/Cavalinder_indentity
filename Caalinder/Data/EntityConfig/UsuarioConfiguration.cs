using Caalinder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Caalinder.Data.EntityConfig
{
    public class UsuarioConfiguration : EntityTypeConfiguration<UserModel>
    {
        public UsuarioConfiguration()
        {
            
        }
    }
}
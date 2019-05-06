using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Data.Context
{ 
    public class ContextConfig
{
    public const string SchemeName = "Caalinder";
    public const string DatabaseName = "Cavalinder";
    public const string Pass = "admin";
    public const string User_Id = "admin";

    public void EditSchemaOnBuilding()
    {
        System.Configuration.Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
        System.Configuration.ConnectionStringsSection objSectionSettings =
            (System.Configuration.ConnectionStringsSection)objConfig.GetSection("connectionStrings");
        var conn = string.Format("DATA SOURCE = {0}; PASSWORD= {1}; USER ID={2};", DatabaseName, Pass, User_Id);
        //Edit
        if (objSectionSettings != null && !(objSectionSettings.ConnectionStrings["sqlconnection"].ConnectionString == conn))
        {
            objSectionSettings.ConnectionStrings["sqlconnection"].ConnectionString = conn;
            objSectionSettings.ConnectionStrings["DbLogErrorContext"].ConnectionString = conn;
            objConfig.Save();
        }
    }
}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDock.DataServices
{
    public class AppSettings
    {
        public static string Find(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }


        public static string DatabaseName
        {
            get
            {
                return Find("DatabaseName");
            }
        }


        public static string DatabasePath
        {
            get
            {
                return Find("DatabasePath");
            }
        }


        public static string ConnectionString
        {
            get
            {
                return string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}\{1};Persist Security Info=False;", DatabasePath, DatabaseName);
            }
        }


    }
}

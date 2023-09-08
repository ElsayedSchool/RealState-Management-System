using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string Updater = "Updater";
        public const string Remover = "Remover";
        public const string User = "User";
        public const string Uploader = "Uploader";
        public const string Exporter = "Exporter";

        public static List<string> GetAppRoles()
        {
            var list = new List<string>()
            {
                Updater, Remover, Uploader, Exporter
            };

            return list;
        }
    }

    public enum AppRole
    {
        Admin,
        Updater,
        Remover,
        User
    }
}

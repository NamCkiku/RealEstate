using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Config
{
    public class Config
    {
        public static string SmtpServer { get; set; }
        public static bool EnableSsl { get; set; }
        public static string SmtpPort { get; set; }
        public static string SmtpUser { get; set; }
        public static string SmtpPass { get; set; }
        public static string AdminEmail { get; set; }

        public Config()
        {
            ConfigurationSetting();
        }

        private void ConfigurationSetting()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            PropertyInfo[] propertyInfos = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (config.AppSettings.Settings[propertyInfo.Name] == null ||
                    string.IsNullOrEmpty(config.AppSettings.Settings[propertyInfo.Name].Value))
                {
                    continue;
                }

                if (propertyInfo.PropertyType == typeof(TimeSpan))
                {
                    object obj = TimeSpan.Parse(config.AppSettings.Settings[propertyInfo.Name].Value);
                    propertyInfo.SetValue(this, obj, null);
                }
                else
                {
                    object obj = Convert.ChangeType(config.AppSettings.Settings[propertyInfo.Name].Value,
                                                propertyInfo.PropertyType);
                    propertyInfo.SetValue(this, obj, null);
                }
            }
        }

    }
}

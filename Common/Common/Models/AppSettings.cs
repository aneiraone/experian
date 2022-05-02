using Microsoft.Extensions.Configuration;

namespace Common
{
    public class AppSettings
    {
        private AppSettings() { }

        private static AppSettings _instance;

        public static AppSettings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppSettings();
            }
            return _instance;
        }

        [ConfigurationKeyName("EmailConfiguration")]
        public EmailConfiguration Email { get; set; }

    }
}
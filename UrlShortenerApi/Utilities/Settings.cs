using System;
using System.Configuration;
using UrlShortenerApi.Properties;

namespace UrlShortenerApi.Utilities
{
    public static class Settings
    {
        public static string GetSettingFromConfig(string key) => ConfigurationManager.AppSettings.Get(key);

        public static int TokenGenerator_MaxLength => Convert.ToInt32(GetSettingFromConfig(Resources.TokenGenerator_MaxLength_Key));
        public static int TokenGenerator_MinLength => Convert.ToInt32(GetSettingFromConfig(Resources.TokenGenerator_MinLength_Key));
        public static int TokenGenerator_ExpireDays => Convert.ToInt32(GetSettingFromConfig(Resources.TokenGenerator_ExpireDays_key));

        public static string AppName => GetSettingFromConfig(Resources.AppName_Key) ?? Resources.AppDefaultName;

        public static int BearerAuthTokenExpireInMinutes => Convert.ToInt32(GetSettingFromConfig(Resources.BearerAuthTokenExpireInMinutes_Key));
    }
}
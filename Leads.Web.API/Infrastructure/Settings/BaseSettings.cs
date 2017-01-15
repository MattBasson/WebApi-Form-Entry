using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leads.Web.APInfrastructure.Settings
{
    public abstract class BaseSettings
    {
        private NameValueCollection _settings;

        private string _keyPrefix;
        protected virtual string GetKeyPrefix()
        {
            if (_keyPrefix == null)
            {
                _keyPrefix = GetType().Name.Replace("Settings", "");
            }
            return _keyPrefix;
        }

        protected BaseSettings()
            : this(ConfigurationManager.AppSettings)
        {
        }

        protected BaseSettings(NameValueCollection settings)
        {
            _settings = settings;
        }

        private string GetValue(string name)
        {
            return _settings[GetKey(name)];
        }

        private string GetKey(string name)
        {
            var prefix = GetKeyPrefix();
            var key = name;
            if (prefix.IsNotEmpty())
                key = prefix + "." + key;
            return key;
        }

        protected T GetSetting<T>(string name)
            where T : IConvertible
        {
            return GetSetting(name, default(T));
        }

        protected void SetSetting(string name, string value)
        {
            _settings[GetKey(name)] = value;
        }

        protected T GetSetting<T>(string name, T defaultValue) where T : IConvertible
        {
            var value = GetValue(name);

            return value == null ? defaultValue : (T)Convert.ChangeType(value, typeof(T));
        }
    }
}

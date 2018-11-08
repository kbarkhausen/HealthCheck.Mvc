using System;
using System.Configuration;
using HealthCheck.Mvc.Models;

namespace HealthCheck.Mvc
{
    public abstract class HealthTest
    {
        public abstract TestResult Run();

        private string _testName;

        public HealthTest()
        {
            _testName = GetType().Name;
        }

        public virtual string TestName
        {
            get { return _testName; }
            set { _testName = value; }
        }

        public string GetTestSetting(string key)
        {
            string settingValue = null;

            var testSettingKeyName = ("HealthCheck:" + GetType().Name + ":" + key).ToLower();

            try
            {
                settingValue = ConfigurationManager.AppSettings[testSettingKeyName];
            }
            catch (Exception)
            {
                throw new Exception(string.Format("An error occurred attempting to read AppSetting '{0}'.", testSettingKeyName));
            }

            if (string.IsNullOrEmpty(settingValue))
                throw new Exception(string.Format("The AppSetting '{0}' is missing, empty or invalid.", testSettingKeyName));

            return settingValue;
        }
    }
}

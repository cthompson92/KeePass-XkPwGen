using System;
using System.IO;
using Newtonsoft.Json;

namespace XKPwGen.Core
{
    public class OptionsManager
    {
        public static string GetOptionsFileName(string profileName)
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return Path.GetFullPath(root + "/XkPwGen/" + profileName + ".json");
        }

        public static void SaveOptions(PasswordGeneratorOptions options, string profileName)
        {
            var fileName = GetOptionsFileName(profileName);
            var file = new FileInfo(fileName);
            if (!file.Exists && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            File.WriteAllText(fileName, JsonConvert.SerializeObject(options));
        }

        public static PasswordGeneratorOptions LoadOptions(string profileName)
        {
            var fileName = GetOptionsFileName(profileName);
            if (!File.Exists(fileName))
            {
                return null;
            }

            var json = File.ReadAllText(fileName);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<PasswordGeneratorOptions>(json);
            }
            catch (Exception ex)
            {
                //TODO: handle exceptions
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XKPwGen.SharedKernel
{
    public static class OptionsManager
    {
        public static string GetOptionsFileName(string profileName)
        {
            return Path.GetFullPath(GetAppDataPathRoot() + profileName + ".json");
        }

        public static string GetAppDataPathRoot()
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return Path.GetFullPath(root + "/XkPwGen/");
        }

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter(),
            },
            Formatting = Formatting.None,
        };

        public static void SaveOptions(PasswordGeneratorOptions options, string profileName)
        {
            var fileName = GetOptionsFileName(profileName);
            var file = new FileInfo(fileName);
            if (!file.Exists && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            File.WriteAllText(fileName, JsonConvert.SerializeObject(options, SerializerSettings));
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

            return JsonConvert.DeserializeObject<PasswordGeneratorOptions>(json, SerializerSettings);
        }

        private static readonly Lazy<DirectoryInfo> ProfilesDirectory = new Lazy<DirectoryInfo>(() => new DirectoryInfo(GetAppDataPathRoot()));

        public static DirectoryInfo GetProfilesDirectory()
        {
            return ProfilesDirectory.Value;
        }

        public static IEnumerable<FileInfo> GetProfileFiles()
        {
            if (!ProfilesDirectory.Value.Exists)
            {
                return new FileInfo[0];
            }

            return ProfilesDirectory.Value.EnumerateFiles("*.json");
        }
    }
}
using System;
using System.IO;
using Newtonsoft.Json;

namespace XKPwGen
{
    public static class Logger
    {
        public static void LogError(Exception ex)
        {
#if DEBUG
            File.AppendAllText(
                    string.Format(
                        "C:\\git\\Darthruneis\\XKPwGen.Net5\\XKPwGen\\logs\\{0}.log",
                        DateTime.Now.Date.ToString("yyyy-MM-dd")),
                    Environment.NewLine + JsonConvert.SerializeObject(ex));
#endif
        }
    }
}

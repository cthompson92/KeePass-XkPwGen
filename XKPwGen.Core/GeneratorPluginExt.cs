using KeePass.Plugins;

namespace XKPwGen.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// KeePass has strict requirements around some aspects of plugin development,
    /// which can be read about <see href="https://keepass.info/help/v2_dev/plg_index.html">here</see>.
    /// </remarks>
    public class GeneratorPluginExt : Plugin
    {
        private IPluginHost m_host = null;

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
            {
                return false;
            }

            m_host = host;
            return true;
        }

        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/darthruneis/XKPwGen/master/"; }
        }
    }
}
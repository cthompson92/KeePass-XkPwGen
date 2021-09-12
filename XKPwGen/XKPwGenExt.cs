using KeePass.Plugins;

namespace XKPwGen
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// KeePass has strict requirements around some aspects of plugin development,
    /// which can be read about <see href="https://keepass.info/help/v2_dev/plg_index.html">here</see>.
    /// </remarks>
    public class XKPwGenExt : Plugin
    {
        private IPluginHost m_host = null;
        private PasswordGenerator m_gen = null;

        public override bool Initialize(IPluginHost host)
        {
            Terminate();

            if (host == null)
            {
                return false;
            }

            m_host = host;
            m_gen = new PasswordGenerator();
            m_host.PwGeneratorPool.Add(m_gen);

            return true;
        }

        public override void Terminate()
        {
            if (m_host == null) return;

            m_host.PwGeneratorPool.Remove(m_gen.Uuid);

            m_gen = null;
            m_host = null;
        }

        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/darthruneis/XKPwGen/master/"; }
        }
    }
}
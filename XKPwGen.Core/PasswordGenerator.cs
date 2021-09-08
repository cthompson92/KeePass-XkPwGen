using System;
using KeePassLib;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;

namespace XKPwGen.Core
{
    public class PasswordGenerator : CustomPwGenerator
    {
        private static readonly PwUuid _guid = new PwUuid(new byte[]
        {
            0x23, 0x9B, 0x8C, 0x92,
            0x33, 0x9A, 0xFF, 0x4B,
            0xA6, 0x4C, 0xC2, 0xAA,
            0x2B, 0x08, 0x40, 0x54,
        });

        /// <summary>Password generation function.</summary>
        /// <param name="prf">Password generation options chosen
        /// by the user. This may be <c>null</c>, if the default
        /// options should be used.</param>
        /// <param name="crsRandomSource">Source that the algorithm
        /// can use to generate random numbers.</param>
        /// <returns>Generated password or <c>null</c> in case
        /// of failure. If returning <c>null</c>, the caller assumes
        /// that an error message has already been shown to the user.</returns>
        public override ProtectedString Generate(PwProfile prf, CryptoRandomStream crsRandomSource)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Each custom password generation algorithm must have
        /// its own unique UUID.
        /// </summary>
        public override PwUuid Uuid
        {
            get { return _guid; }
        }

        /// <summary>
        /// Displayable name of the password generation algorithm.
        /// </summary>
        public override string Name
        {
            get { return "XKPwGen"; }
        }

        public override bool SupportsOptions
        {
            get { return true; }
        }

        public override string GetOptions(string strCurrentOptions)
        {
            return base.GetOptions(strCurrentOptions);
        }
    }
}

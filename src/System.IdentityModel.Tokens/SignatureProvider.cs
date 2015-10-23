//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using Microsoft.IdentityModel.Logging;

namespace System.IdentityModel.Tokens
{
    /// <summary>
    /// Provides signature services, signing and verifying.
    /// </summary>
    public abstract class SignatureProvider : IDisposable
    {
        protected SignatureProvider(SecurityKey key, string algorithm)
        {
            if (key == null)
                throw LogHelper.LogException<ArgumentNullException>("key");

            if (string.IsNullOrEmpty(algorithm))
                throw LogHelper.LogException<ArgumentException>(LogMessages.IDX10000, "algorithm");

            Key = key;
            Algorithm = algorithm;
        }

        /// <summary>
        /// Gets or sets a user context for a <see cref="SignatureProvider"/>.
        /// </summary>
        public string Context
        {
            get;
            set;
        }

        /// <summary>
        /// Answers if this <see cref="SignatureProvider"/> supports the key, algorithm pair.
        /// </summary>
        /// <param name="algorithm">the crypto algorithm to use.</param>
        /// <returns>'true' if algorithm is supported.</returns>
        public abstract bool IsSupportedAlgorithm(string algorithm);

        public SecurityKey Key { get; private set; }

        public string Algorithm { get; private set; }

        /// <summary>
        /// Produces a signature over the 'input'
        /// </summary>
        /// <param name="input">bytes to sign.</param>
        /// <returns>signed bytes</returns>
        public abstract byte[] Sign(byte[] input);

        /// <summary>
        /// Verifies that a signature created over the 'input' matches the signature.
        /// </summary>
        /// <param name="input">bytes to verify.</param>
        /// <param name="signature">signature to compare against.</param>
        /// <returns>true if the computed signature matches the signature parameter, false otherwise.</returns>
        public abstract bool Verify(byte[] input, byte[] signature);

        #region IDisposable Members

        /// <summary>
        /// Calls <see cref="Dispose(bool)"/> and <see cref="GC.SuppressFinalize"/>
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Can be over written in descendants to dispose of internal components.
        /// </summary>
        /// <param name="disposing">true, if called from Dispose(), false, if invoked inside a finalizer</param>     
        protected abstract void Dispose(bool disposing);

        #endregion
    }
}

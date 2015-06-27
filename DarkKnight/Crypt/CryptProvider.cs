using System;

#region License Information
/* ************************************************************
 * 
 *    @author AntonioJr <antonio@emplehstudios.com.br>
 *    @copyright 2015 Empleh Studios, Inc
 * 
 * 	  Project Folder: https://github.com/antoniojoaojr/DarkKnight
 * 
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion

namespace DarkKnight.Crypt
{
    public abstract class CryptProvider
    {
        /// <summary>
        /// The crypt class registered
        /// </summary>
        private Object _crypt;

        /// <summary>
        /// Indication crypt class is registed if is true
        /// </summary>
        private bool _registed = false;

        /// <summary>
        /// Try decode a package in a registered crypt class
        /// </summary>
        /// <param name="packet">array of byte to decode</param>
        /// <returns>if no error, the array of byte decoded, if generate a error return the original array of byte</returns>
        protected byte[] _decode(byte[] packet)
        {
            // if no have registed crypt class, return the packet without decoding
            if (!_registed)
                return packet;

            try
            {
                // Try decode and return the packet decoded
                return (byte[])_crypt.GetType().
                    GetMethod("decode").
                    Invoke(_crypt, new object[] { packet });
            }
            catch
            {
                Console.WriteLine("[ERROR] class " + _crypt.GetType().Name + " responsable to decrypt generate a error and the package received is not descripted");
                // if the packet not be complet the decode, return the original packet from param
                return packet;
            }
        }

        /// <summary>
        /// Trye to encode a packet in a registered crypt class
        /// </summary>
        /// <param name="packet">array of byte to encode</param>
        /// <returns>if no error, the array of byte encoded, if a error return the original array of byte</returns>
        protected byte[] _encode(byte[] packet)
        {
            // if no have registed crypt class, return the packet without encoding
            if (!_registed)
                return packet;

            try
            {
                // Try encode and return the packet encoded
                return (byte[])_crypt.GetType().
                    GetMethod("encode").
                    Invoke(_crypt, new object[] { packet }); ;
            }
            catch
            {
                Console.WriteLine("[ERROR] class " + _crypt.GetType().Name + " responsable to encrypt generate a error and the package to send is not cryptographed");
                // if the packet not be complet the encoded, return the original packet from param
                return packet;
            }
        }

        /// <summary>
        /// Return true if crypt class is registed
        /// otherwise false
        /// </summary>
        protected bool _cryptRegisted
        {
            get { return _registed; }
        }

        /// <summary>
        /// Register a crypt class in the memory
        /// </summary>
        /// <param name="crypt">The AbstractCrypt extended class</param>
        protected void _registerCrypt<T>(T crypt)
        {
            _crypt = crypt;
            _registed = true;
        }
    }
}

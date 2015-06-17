using DarkKnight.Network;
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
    public abstract class AbstractCrypt
    {
        /// <summary>
        /// Encode a package with your own encrypts algorithm
        /// </summary>
        /// <param name="package">The packet to be encrypted</param>
        /// <returns>The packet encoded</returns>
        public abstract byte[] encode(byte[] package);

        /// <summary>
        /// Decode a package with you own decrypt algorithm
        /// </summary>
        /// <param name="package">The packet to be decoded</param>
        /// <returns>The packet decoded</returns>
        public abstract byte[] decode(byte[] package);

        /// <summary>
        /// Register the class to be a crypt provider for client passing by param
        /// </summary>
        /// <typeparam name="T">Class to encode and decode package</typeparam>
        /// <param name="client">Client when this object process</param>
        /// <param name="crypt">object of classe to encode and decode</param>
        protected void register<T>(Client client, T crypt)
        {
            if (!crypt.GetType().IsSubclassOf(typeof(AbstractCrypt)))
                throw new Exception("The crypt param is a invalid class crypt for the server");

            core.Clients.Register.Add(client.Id, core.Clients.RegisterType.crypt, crypt);
        }
    }
}

using System;
using System.Collections.Generic;

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

namespace DarkKnight.Utils
{
    class PacketDictionary
    {
        private static Dictionary<byte[], DKAbstractReceiver> packetMapped = new Dictionary<byte[], DKAbstractReceiver>();

        /// <summary>
        /// We try to recover a DKAbstractReceiver object with the specified byte format
        /// </summary>
        /// <param name="packetFormat">array of byte specified</param>
        /// <returns>The DKAbstractReceiver object</returns>
        public static DKAbstractReceiver getmappin(byte[] packetFormat)
        {
            if (!packetMapped.ContainsKey(packetFormat))
                return null;

            return packetMapped[packetFormat];
        }

        /// <summary>
        /// Adds a new DKAbstractReceiver object with the specified key in byte format
        /// </summary>
        /// <param name="packetFormat">The array of byte to the key</param>
        /// <param name="callback">The DKAbstractReceiver</param>
        public static void mappin(byte[] packetFormat, DKAbstractReceiver callback)
        {
            packetMapped.Add(packetFormat, callback);
        }

        /// <summary>
        /// Gets the Dictionary of packetMapped
        /// </summary>
        public static Dictionary<byte[], DKAbstractReceiver> getAllMapping
        {
            get { return packetMapped; }
        }
    }
}

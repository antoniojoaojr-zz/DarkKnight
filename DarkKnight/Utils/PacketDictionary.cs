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
        private static Dictionary<string, DKAbstractReceivable> packetMapped = new Dictionary<string, DKAbstractReceivable>();

        public static DKAbstractReceivable getmappin(string packetName)
        {
            if (!packetMapped.ContainsKey(packetName))
                return null;

            return packetMapped[packetName];
        }


        public static void mappin(string packetName, DKAbstractReceivable callback)
        {
            if (packetName.Length > 3)
            {
                throw new Exception("Packages has only three characters name");
            }

            packetMapped.Add(packetName, callback);
        }

        public static Dictionary<string, DKAbstractReceivable> getAllMapping
        {
            get { return packetMapped; }
        }
    }
}

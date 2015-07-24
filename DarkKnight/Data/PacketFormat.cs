using DarkKnight.core.Packets;
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

namespace DarkKnight.Data
{
    /// <summary>
    /// Class represents the format of a packet
    /// </summary>
    public class PacketFormat
    {
        /// <summary>
        /// Register a enum format in the server
        /// </summary>
        /// <param name="enumObject">Enum object format</param>
        /// <exception cref="System.Exception">Enum format as been registered previously</exception>
        public static void registerEnum(Enum enumObject)
        {
            FormatController.registerEnumFormat(enumObject);
        }

        /// <summary>
        /// Register a Dictionary<string,int> for a format in the server
        /// </summary>
        /// <param name="dicObject">System.Collection.Generic.Dictionary<string,int> object</param>
        /// <exception cref="System.Exception">Dictionary format as been registered previously</exception>
        public static void registerDictionary(Dictionary<string, int> dicObject)
        {
            FormatController.registerDictionaryFormat(dicObject);
        }

    }
}

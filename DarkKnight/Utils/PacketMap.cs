using DarkKnight.Data;
using System;
using System.Text;

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
    public class PacketMap
    {
        /// <summary>
        /// Add a specific package to be received in a specific class
        /// This means that every time a client sends this packet to the server, 
        /// we will call the class specified to process the packet. 
        /// The call will be in ReceivedPacket(DKClient, Packet) method that belongs to DKAbstractReceiver class that must be the parent class.
        /// After the call ReceivedPacket(DKClient, Packet), the server calls the run() method. 
        /// So the application can work with the specified package received in a specified class
        /// </summary>
        /// <param name="format">The format of the package</param>
        /// <param name="callback">The class to be call</param>
        public static void add(PacketFormat format, DKAbstractReceiver callback)
        {
            PacketDictionary.getAllMapping.Add(Encoding.UTF8.GetBytes(format.getStringFormat), callback);
        }
    }
}

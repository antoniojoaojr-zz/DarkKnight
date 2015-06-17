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

namespace DarkKnight.Network
{
    public class Packet
    {
        /// <summary>
        /// The name of this packet
        /// </summary>
        protected string _name = "???";
        /// <summary>
        /// The length of this packet
        /// </summary>
        protected int _length = 0;
        /// <summary>
        /// The data to process of this packet
        /// </summary>
        protected byte[] _packet;

        /// <summary>
        /// Get the name of this packet
        /// </summary>
        public string name
        {
            get { return _name; }
        }

        /// <summary>
        /// Get the length of this packet
        /// </summary>
        public int length
        {
            get { return _length; }
        }

        /// <summary>
        /// Get the byte array of data to process of this packet
        /// </summary>
        public byte[] packet
        {
            get { return _packet; }
        }
    }
}

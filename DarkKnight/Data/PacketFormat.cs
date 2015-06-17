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

namespace DarkKnight.Data
{
    /// <summary>
    /// Class represents the format of a packet
    /// </summary>
    public abstract class PacketFormat
    {
        protected string format = null;

        /// <summary>
        /// Gets the packet format in a string
        /// </summary>
        public string getStringFormat
        {
            get
            {
                if (format == null)
                    throw new Exception("The format is not setted");

                return format;
            }
        }

        /// <summary>
        /// Gets the packet format in a array of char
        /// </summary>
        public char[] getCharArrayFormat
        {
            get
            {
                if (format == null)
                    throw new Exception("the format is not setted");

                return format.ToCharArray();
            }
        }
    }
}

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

namespace DarkKnight.Data
{
    /// <summary>
    /// Formats that are used internally by DarkKnight
    /// </summary>
    public enum DarkKnightFormat
    {
        /// <summary>
        /// It is a serialized object in json
        /// </summary>
        dk_JStreamObj
    }

    /// <summary>
    /// Class represents the format of a packet
    /// </summary>
    public class PacketFormat
    {
        /// <summary>
        /// Get a name of a DarkKnight packet format
        /// </summary>
        /// <param name="format">DarkKnight.Data.DarkKnightFormat enum format</param>
        /// <returns>string of name of enum</returns>
        public static string DarkKnightFormat(DarkKnightFormat format)
        {
            return Enum.GetName(typeof(Data.DarkKnightFormat), format);
        }

        private string format = null;

        /// <summary>
        /// Sets the format of a packet passing as parameter string
        /// </summary>
        /// <param name="name">The name of format</param>
        public PacketFormat(string name)
        {
            if (name.Length < 3)
                throw new Exception("The min length of name is 3");

            format = name;
        }

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

        /// <summary>
        /// Gets the packet format in a array of byte
        /// </summary>
        public byte[] getByteArrayFormat
        {
            get
            {
                if (format == null)
                    throw new Exception("the format is not setted");

                return Encoding.UTF8.GetBytes(format);
            }
        }
    }
}

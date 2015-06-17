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
    public class PacketFormatController : PacketFormat
    {
        /// <summary>
        /// The characters is valid for the format in a byte represetantion
        /// </summary>
        private byte[] format_accept = {

             97,98,99,100,101,102,103,55,
             104,106,107,109,110,112,56,
             87,88,89,90,113,114,115,57,
             116,117,118,119,120,121,63,
             122,65,66,67,51,52,53,54,
             68,69,70,71,72,74,75,77,50,
             78,80,81,82,83,84,85,86
                                       };


        /// <summary>
        /// Sets the format of a packet passing as parameter three characteres.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public PacketFormatController(char x, char y, char z)
        {
            setFormat(x, y, z);
        }

        /// <summary>
        /// Sets the format passing as parameter three characters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void setFormat(char x, char y, char z)
        {
            set(x, y, z);
        }

        /// <summary>
        /// Set the format passing a string with 3 caracteres
        /// </summary>
        /// <param name="str">string with 3 caracteres</param>
        /// <exception cref="Exception">If the string length not equals 3</exception>
        public void setFormat(string str)
        {
            if (str.Length > 3)
                throw new Exception("Invalid size of format");

            char[] _format = str.ToCharArray();

            set(_format[0], _format[1], _format[2]);
        }
        private void set(char x, char y, char z)
        {
            if (!isValid((byte)x) || !isValid((byte)y) || !isValid((byte)z))
                throw new Exception("the data to set format is invalid");

            format = new String(new char[3] { x, y, z });
        }


        private bool isValid(byte c)
        {
            foreach (byte accept in format_accept)
            {
                if (c == accept)
                    return true;
            }

            return false;
        }

    }
}

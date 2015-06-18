using System;
using System.Collections.Generic;
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
    class PacketCreator
    {
        /// <summary>
        /// The data formated in default package DarkKnight
        /// </summary>
        private byte[] _data;

        /// <summary>
        /// Gets the data formated
        /// </summary>
        public byte[] data
        {
            get { return _data; }
        }

        /// <summary>
        /// Creator a data formated with not format
        /// </summary>
        /// <param name="data">array of bytes data</param>
        public PacketCreator(byte[] data)
        {
            // format data positions
            _data = getData(data);
        }

        /// <summary>
        /// Create data formated with a format
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormatController object</param>
        /// <param name="data">array of bytes data</param>
        public PacketCreator(PacketFormat format, byte[] data)
        {
            // format data positions
            _data = getData(data);

            // get format in byte array
            byte[] formatData = format.getByteArrayFormat;

            Array.Copy(formatData, _data, formatData.Length);
        }

        /// <summary>
        /// Get the data formated with a format of DarkKnight package
        /// </summary>
        /// <param name="data">the byte of array for format</param>
        /// <returns>the data formated</returns>
        private byte[] getData(byte[] data)
        {
            // if data length is zero, just return 3 position to store formating type
            if (data.Length == 0)
                return new byte[3];

            // first we get the length of data to sending
            int length = data.Length;
            // we create a dynamic array to store a length information in packet
            List<byte> lengthData = new List<byte>();

            // we storing the length of data when, length is more than zero
            while (length > 0)
            {
                // getting the length to store
                byte lengthStore = (length > 127) ? (byte)127 : (byte)length;

                // store the length
                lengthData.Add(lengthStore);

                // discount in length
                length -= lengthStore;
            }

            // we create a dataFormated with length:
            // 3 (length of format packet informatio) + length of length data + length of length data information + 1 (for position final length information) 
            byte[] dataFormat = new byte[3 + data.Length + lengthData.Count + 1];

            // convert the dynamic list to the dataFormated
            for (int i = 0; i < lengthData.Count; i++)
                dataFormat[i + 3] = lengthData[i];

            // add the final length information
            dataFormat[3 + lengthData.Count] = 0;

            // getting position to start data package
            int startIndex = 3 + lengthData.Count + 1;

            // copy the data to data formated
            Array.Copy(data, 0, dataFormat, startIndex, data.Length);

            // return the data formated
            return dataFormat;
        }
    }
}

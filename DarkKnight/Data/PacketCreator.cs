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
            Creator(new PacketFormat("???"), data);
        }

        /// <summary>
        /// Create data formated with a format
        /// </summary>
        /// <param name="format">DarkKnight.Data.PacketFormat object</param>
        /// <param name="data">array of bytes data</param>
        public PacketCreator(PacketFormat format, byte[] data)
        {
            Creator(format, data);
        }

        private void Creator(PacketFormat format, byte[] data)
        {
            // create format
            byte[] packetFormat = formatingPacket(format.getByteArrayFormat);

            // create data
            byte[] packetData = formatingPacket(data);

            // setting length of packet
            _data = new byte[packetFormat.Length + packetData.Length];

            // copy format to packet
            Array.Copy(packetFormat, _data, packetFormat.Length);

            // copy data to packet
            Array.Copy(packetData, 0, _data, packetFormat.Length, packetData.Length);
        }

        /// <summary>
        /// Get the data formated with a format of DarkKnight package
        /// </summary>
        /// <param name="data">the byte of array for format</param>
        /// <returns>the data formated</returns>
        private byte[] formatingPacket(byte[] data)
        {
            // we get a dynamic array stored a length information in packet
            List<byte> lengthData = lengthList(data.Length);

            // we create a dataFormated with length:
            // length of length data + length of length data information + 1 (for position final length information) 
            byte[] packetData = new byte[data.Length + lengthData.Count + 1];

            // convert the dynamic list to the dataFormated
            for (int i = 0; i < lengthData.Count; i++)
                packetData[i] = lengthData[i];

            // add the final length information
            packetData[lengthData.Count] = 0;

            // if length is one, return
            if (packetData.Length < 2)
                return packetData;

            // copy the data to data formated
            Array.Copy(data, 0, packetData, lengthData.Count + 1, data.Length);

            // return the data formated
            return packetData;
        }

        private List<byte> lengthList(int _length)
        {
            int length = _length;
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

            return lengthData;
        }
    }
}

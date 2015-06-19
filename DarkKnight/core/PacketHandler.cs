using DarkKnight.Crypt;
using DarkKnight.Data;
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

namespace DarkKnight.core
{
    class PacketHandler : Packet
    {
        /// <summary>
        /// The real package received from a client socket
        /// </summary>
        private byte[] originalPacket;

        /// <summary>
        /// The position of length information end
        /// </summary>
        private int lengthPosition = 3;

        /// <summary>
        /// The length of the data to process the packet
        /// </summary>
        private int length = 0;

        public PacketHandler(byte[] packet)
        {
            // we get original packet
            originalPacket = packet;

            // if handler the packet
            if (handler())
            {
                // filter the packet for a readable packege
                filter();
            }
        }

        /// <summary>
        /// We check whether the packet is a valid packet seeing its reporting rules
        /// First we find the minimum size
        /// Then also we check to see if it is greater than the minimum size, and if this is true
        /// We verify that it correctly informs the data size that the package stores to be processed.
        /// If all this corretado then qualify the package as valid to go to apply and be treated as the application requires.
        /// </summary>
        /// <returns>True if packet is valid otherwise false</returns>
        private bool handler()
        {
            // the min length of the packet is 3
            // the first 3 position in the packet define the packet name or format
            if (originalPacket.Length < 3)
                return false;

            // if the packet have length is more than 3
            // the packet have data to process and not just command
            if (originalPacket.Length > 3)
            {
                // start the read length of the data to process
                // when the packet in the lengthPosition is bigger than 0, have more length to process in next point
                // the length information finish when the packet in lengthPosition is equal 0
                do
                {
                    length += originalPacket[lengthPosition];
                    lengthPosition++;

                    // if the lengthPosition is equal or bigger of packet length
                    // is a invalid packet, we discard the packet returning false
                    if (lengthPosition >= originalPacket.Length)
                        return false;

                } while (originalPacket[lengthPosition] > 0);

                // we check if the package have the same length readable
                // if not, the packet is invalid and discard
                if (originalPacket.Length != length + lengthPosition++)
                    return false;
            }


            // if the sums of the format positions is nonzero, we have a new format for packet
            if (originalPacket[0] + originalPacket[1] + originalPacket[2] != 0)
            {
                try
                {
                    // here we try to give the new format to the package
                    PacketFormat format = new PacketFormatController((char)originalPacket[0], (char)originalPacket[1], (char)originalPacket[2]);

                    // we got here without exception the format is valid
                    _format = format;
                }
                catch
                {
                    // if we can not give the new format to the package, then we define the package is invalid
                    return false;
                }
            }

            // the packet received have the specifications to go to the process data
            return true;
        }

        /// <summary>
        /// According to the handler() information, we feed the Packet class with the data
        /// </summary>
        private void filter()
        {
            // if length is zero, we have nothing to process here
            if (length == 0)
                return;

            // we create a array of bytes in the size of the length to read
            _packet = new byte[length];

            // converting the original packet to the data of a packet to process
            Array.Copy(originalPacket, lengthPosition, _packet, 0, _packet.Length);
        }
    }
}

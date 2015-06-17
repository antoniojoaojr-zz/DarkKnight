using DarkKnight.Network;
using DarkKnight.Network;
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

        public PacketHandler(byte[] packet)
        {
            originalPacket = packet;


            // if handler the packet
            if (handler())
            {
                // filter the packet for a readable packege
                filter();
            }
        }


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
                    _length += originalPacket[lengthPosition];
                    lengthPosition++;

                    // if the lengthPosition is equal or bigger of packet length
                    // have a error in the packet, discard the packet returning false
                    if (lengthPosition >= originalPacket.Length)
                        return false;

                } while (originalPacket[lengthPosition] > 0);

                // we check if the package have the same length readable
                // if not the packet is invalid and discard then
                if (originalPacket.Length != _length + lengthPosition++)
                    return false;
            }

            // the packet received have the specifications to go to the process data
            return true;
        }

        private void filter()
        {
            // if the sums of the name positions is nonzero, we have a new name for packet
            if (originalPacket[0] + originalPacket[1] + originalPacket[2] != 0)
            {
                _name = Encoding.UTF8.GetString(originalPacket, 0, 3);
            }

            // we create a array of bytes in the size of the length to read
            _packet = new byte[_length];

            // converting the original packet to the data of a packet to process
            for (int i = 0; i < _packet.Length; i++)
                _packet[i] = originalPacket[1 - lengthPosition++];
        }
    }
}

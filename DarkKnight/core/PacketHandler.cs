using DarkKnight.Crypt;
using DarkKnight.Data;
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

namespace DarkKnight.core
{
    class PacketHandler : Packet
    {
        public string invalidData = null;

        public List<Packet> packetHandled = new List<Packet>();

        public PacketHandler(byte[] _packet)
        {
            // try handler packet
            if (!handlerPacket(_packet))
            {
                invalidData = "";
                for (int i = 0; i < _packet.Length; i++)
                    invalidData += _packet[i] + " ";

                _format = new PacketFormat("???");
                _packet = new byte[] { };
            }
        }

        /// <summary>
        /// Here we filter the received packet in bytes to turn into a readable package for the Packet class.
        /// We took the package format and also took the given package.
        /// </summary>
        /// <returns>true if packet is handled with success</returns>
        private bool handlerPacket(byte[] packet)
        {
            int lengthFormat = 0;
            int lengthFormatPosition = 0;

            do
            {
                lengthFormat += packet[lengthFormatPosition];
                lengthFormatPosition++;
                if (lengthFormatPosition >= packet.Length || lengthFormat > packet.Length)
                    return false;

            } while (packet[lengthFormatPosition] > 0);

            int lengthData = 0;
            int lengthDataPosition = lengthFormat + lengthFormatPosition + 1;

            if (lengthDataPosition >= packet.Length)
                return false;

            do
            {
                lengthData += packet[lengthDataPosition];
                lengthDataPosition++;
                if (lengthDataPosition >= packet.Length || lengthData > packet.Length)
                    return false;

            } while (packet[lengthDataPosition] > 0);


            int lengthDataLengthInformation = lengthDataPosition - (lengthFormat + lengthFormatPosition + 1);
            int totalDataLength = lengthData + lengthFormat + lengthFormatPosition + lengthDataLengthInformation + 2;

            if (packet.Length < totalDataLength)
                return false;

            try
            {
                byte[] format = new byte[lengthFormat];
                _packet = new byte[lengthData];

                Array.Copy(packet, lengthFormatPosition + 1, format, 0, lengthFormat);
                Array.Copy(packet, lengthDataPosition + 1, _packet, 0, lengthData);

                _format = new PacketFormat(Encoding.UTF8.GetString(format));

                packetHandled.Add(this);

                if (packet.Length == totalDataLength)
                    return true;

                byte[] morePacket = new byte[packet.Length - totalDataLength];
                Array.Copy(packet, totalDataLength, morePacket, 0, packet.Length - totalDataLength);

                return handlerPacket(morePacket);
            }
            catch
            {
                return false;
            }
        }
    }
}

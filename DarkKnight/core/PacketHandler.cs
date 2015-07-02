using DarkKnight.Crypt;
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

namespace DarkKnight.core
{
    class PacketHandler : Packet
    {
        /// <summary>
        /// The real package received from a client socket
        /// </summary>
        private byte[] originalPacket;


        public PacketHandler(byte[] packet)
        {
            // we get original packet
            originalPacket = packet;

            // try handler packet
            if (!handlerPacket())
            {
                // if handler is fail, set a default invalid packet
                _format = new PacketFormat("???");
                _packet = new byte[] { };
                _tryReceived = originalPacket;
            }
        }

        /// <summary>
        /// Here we filter the received packet in bytes to turn into a readable package for the Packet class.
        /// We took the package format and also took the given package.
        /// </summary>
        /// <returns>true if packet is handled with success</returns>
        private bool handlerPacket()
        {
            int lengthFormat = 0;
            int lengthFormatPosition = 0;

            do
            {
                lengthFormat += originalPacket[lengthFormatPosition];
                lengthFormatPosition++;
                if (lengthFormatPosition >= originalPacket.Length || lengthFormat > originalPacket.Length)
                    return false;

            } while (originalPacket[lengthFormatPosition] > 0);

            int lengthData = 0;
            int lengthDataPosition = lengthFormat + lengthFormatPosition + 1;

            if (lengthDataPosition >= originalPacket.Length)
                return false;

            do
            {
                lengthData += originalPacket[lengthDataPosition];
                lengthDataPosition++;
                if (lengthDataPosition >= originalPacket.Length || lengthData > originalPacket.Length)
                    return false;

            } while (originalPacket[lengthDataPosition] > 0);


            int lengthDataLengthInformation = lengthDataPosition - (lengthFormat + lengthFormatPosition + 1);

            if (originalPacket.Length != lengthData + lengthFormat + lengthFormatPosition + lengthDataLengthInformation + 2)
                return false;

            try
            {
                byte[] format = new byte[lengthFormat];
                _packet = new byte[lengthData];

                Array.Copy(originalPacket, lengthFormatPosition + 1, format, 0, lengthFormat);
                Array.Copy(originalPacket, lengthDataPosition + 1, _packet, 0, lengthData);

                _format = new PacketFormat(Encoding.UTF8.GetString(format));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

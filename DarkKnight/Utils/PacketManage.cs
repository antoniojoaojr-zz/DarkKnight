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

namespace DarkKnight.Utils
{
    class PacketManage : Packet
    {
        private int _startPosition;

        public PacketManage(byte[] packet)
        {
            if (!CheckPackage(packet))
            {
                _key = "????";
                return;
            }

            _key = Encoding.UTF8.GetString(packet, 0, 3);
            if (packet.Length > 3)
            {
                for (int i = 0; i < _length; i++)
                {
                    _packet[i] = packet[_startPosition++];
                }
            }
        }


        private bool CheckPackage(byte[] packet)
        {
            if (packet.Length < 3)
                return false;

            if (packet.Length > 3)
            {
                int length = 0;
                int lPosition = 3;

                do
                {
                    length += packet[lPosition];
                    lPosition++;

                    if (lPosition >= packet.Length)
                        return false;

                } while (packet[lPosition] == 127);

                if (packet.Length < length + lPosition + 1)
                {
                    return false;
                }

                _length = length;
                _startPosition = lPosition;
            }

            return true;
        }
    }
}

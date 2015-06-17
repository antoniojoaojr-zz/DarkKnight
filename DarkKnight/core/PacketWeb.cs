using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

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
    class PacketWeb
    {
        /// <summary>
        /// Default guid of the webscokets
        /// </summary>
        private static string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        /// <summary>
        /// This method check the package received is a WebSocket packet with auth information
        /// If true, we captured the key sent by websocket and do authentication, return the packet authentication into a byte array
        /// </summary>
        /// <param name="_packet"></param>
        /// <returns>array of bytes</returns>
        public static byte[] auth(byte[] _packet)
        {
            string receivable = Encoding.UTF8.GetString(_packet, 0, _packet.Length);

            if (receivable.Any(text => "Sec-WebSocket-Key:".Contains(text)))
            {
                string key = receivable.Replace("Sec-WebSocket-Key:", "`").Split('`')[1].Replace("\r", "").Split('\n')[0].Trim();
                string acceptKey = AcceptKey(ref key);
                string toResponse = "HTTP/1.1 101 Switching Protocols\r\n"
                         + "Upgrade: websocket\r\n"
                         + "Connection: Upgrade\r\n"
                         + "Sec-WebSocket-Accept: " + acceptKey + "\r\n\r\n";

                return Encoding.UTF8.GetBytes(toResponse);

            }
            return new byte[1];

        }

        /// <summary>
        /// We decoded a packet reveived by a websocket
        /// </summary>
        /// <param name="_packet">array of byte to the decode</param>
        /// <returns>array of byte of the packet decoded</returns>
        public static byte[] decode(byte[] _packet)
        {
            int second = _packet[1] & 127;
            int maskIndex = 0;

            if (second < 126)
                maskIndex = 2;
            else if (second == 126)
                maskIndex = 4;
            else if (second == 127)
                maskIndex = 10;

            byte[] mask = {
                              _packet[maskIndex],
                              _packet[maskIndex+1],
                              _packet[maskIndex+2],
                              _packet[maskIndex+3]
                          };
            int contentIndex = maskIndex + 4;

            byte[] decode = new byte[_packet.Length - contentIndex];
            for (int i = contentIndex, k = 0; i < _packet.Length; i++, k++)
                decode[k] = (byte)(_packet[i] ^ mask[k % 4]);

            return decode;
        }

        /// <summary>
        /// We encode one byte array to be sent to a websocket
        /// </summary>
        /// <param name="_packet">array of byte to the encode</param>
        /// <returns>array of byte of the packet encoded</returns>
        public static byte[] encode(byte[] _packet)
        {
            byte[] send;

            int indexData = -1;

            if (_packet.Length <= 125)
            {
                send = new byte[2 + _packet.Length];
                send[0] = 129;
                send[1] = (byte)_packet.Length;
                indexData = 2;
            }
            else if (_packet.Length >= 126 && _packet.Length <= 65535)
            {
                send = new byte[4 + _packet.Length];
                send[0] = 129;
                send[1] = 126;
                send[2] = Convert.ToByte((_packet.Length >> 8) & 255);
                send[3] = Convert.ToByte(_packet.Length & 255);
                indexData = 4;
            }
            else
            {
                send = new byte[10 + _packet.Length];
                send[0] = 129;
                send[1] = 127;
                send[2] = Convert.ToByte((_packet.Length >> 56) & 255);
                send[3] = Convert.ToByte((_packet.Length >> 48) & 255);
                send[4] = Convert.ToByte((_packet.Length >> 40) & 255);
                send[5] = Convert.ToByte((_packet.Length >> 32) & 255);
                send[6] = Convert.ToByte((_packet.Length >> 24) & 255);
                send[7] = Convert.ToByte((_packet.Length >> 16) & 255);
                send[8] = Convert.ToByte((_packet.Length >> 8) & 255);
                send[9] = Convert.ToByte((_packet.Length) & 255);

                indexData = 10;
            }

            Array.Copy(_packet, 0, send, indexData, _packet.Length);

            return send;
        }

        private static string AcceptKey(ref string key)
        {
            string longKey = key + guid;
            byte[] hashBytes = ComputeHash(longKey);
            return Convert.ToBase64String(hashBytes);
        }

        private static SHA1 sha1 = SHA1CryptoServiceProvider.Create();
        private static byte[] ComputeHash(string longKey)
        {
            return sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(longKey));
        }

    }
}

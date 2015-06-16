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

namespace DarkKnight
{
    public abstract class DKBuffer
    {
        private byte[] bBuffer = new byte[65535];
        private int bPosition = 0;
        private int bLength = 0;

        public int position()
        {
            return bPosition;
        }

        public int position(int position)
        {
            bPosition = position;
            return bPosition;
        }

        public void get(byte[] buffer)
        {
            get(buffer, 0, buffer.Length);
        }

        public void get(byte[] buffer, int startIndex, int bufferLength)
        {
            if (startIndex >= buffer.Length)
                throw new Exception("The startIndex is bigger of buffer length");

            if (bufferLength > buffer.Length || startIndex + bufferLength > bufferLength)
                throw new Exception("The length is bigger of buffer length");


            for (int i = startIndex; i < startIndex + bufferLength; i++)
            {
                buffer[i] = bBuffer[bPosition];
                bPosition++;
            }

        }

        /// <summary>
        /// Gets the current position of the buffer until the end position in a string
        /// </summary>
        /// <returns>The string of the buffer</returns>
        public string getString()
        {
            return getString(bPosition, bLength);
        }

        private string getString(int startIndex, int length)
        {
            return Encoding.UTF8.GetString(bBuffer, startIndex, length);
        }

        protected void setBuffer(byte[] buffer, int size)
        {
            for (int i = 0; i < size; i++)
            {
                bBuffer[i] = buffer[i];
            }

            bLength = size;
        }

    }
}

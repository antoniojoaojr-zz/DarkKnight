using DarkKnight.Network;
using System;
using System.Threading;

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
    class DarkKnightDelegate
    {
        private static bool delegateSetted = false;
        private static DKService _callback;

        public static void setDelegate(DKService setCallback)
        {
            if (delegateSetted)
                throw new Exception("only one DKService inheritance is allowed");

            _callback = setCallback;

            new Server().open(2104);
        }

        public static DKService callback
        {
            get { return _callback; }
        }
    }
}

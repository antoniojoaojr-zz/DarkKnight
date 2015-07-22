using DarkKnight.Network;
using System.Collections.Generic;

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

namespace DarkKnight.core.Network
{
    class ObjectController : ObjectViewer
    {
        private static int object_id = 1;

        /// <summary>
        /// Return a object Id controller
        /// </summary>
        public static int objectInstance
        {
            get
            {
                return object_id++;
            }
        }

        private List<ObjectMethods> _methods;

        public ObjectController(List<ObjectMethods> methods)
        {
            _methods = methods;
        }

        /// <summary>
        /// Update the object to the client
        /// </summary>
        public void Update()
        {
            Dictionary<string, object> radiusSend = new Dictionary<string, object>();
            Dictionary<string, object> viewSend = new Dictionary<string, object>();
            Dictionary<string, object> targetSend = new Dictionary<string, object>();

            foreach (ObjectMethods method in _methods)
            {
                if (method.Update())
                {
                    switch (method.networkAccess)
                    {
                        case toPlayer.Viewing:
                            viewSend[method.name] = method.data;
                            break;
                        case toPlayer.Radius:
                            radiusSend[method.name] = method.data;
                            break;
                        case toPlayer.Target:
                            targetSend[method.name] = method.data;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (radiusSend.Count > 0)
                UpdateRadius(radiusSend);

            if (viewSend.Count > 0)
                UpdateView(viewSend);

            if (targetSend.Count > 0)
                UpdateTarget(targetSend);
        }
    }
}

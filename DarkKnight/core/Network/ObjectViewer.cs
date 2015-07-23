using DarkKnight.Data;
using DarkKnight.Network;
using DarkKnight.Utils;
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
    abstract class ObjectViewer
    {
        private int Id = ObjectController.objectInstance;
        private Dictionary<string, object> _dataView = new Dictionary<string, object>();
        private Dictionary<string, object> _dataRadius = new Dictionary<string, object>();
        private Dictionary<string, object> _dataTarget = new Dictionary<string, object>();
        private ClientCollections Viewer = new ClientCollections();
        private ClientCollections Radius = new ClientCollections();
        private ClientCollections Target = new ClientCollections();

        /// <summary>
        /// Called when new client in the radius of this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void EnterRadius(Client client)
        {
            client.SendFormatedObject(PacketFormat.Format(DefaultFormat.JRadiusStream), new ObjectStream() { Id = Id, ObjectData = _dataRadius });

            Radius.Add(client);
        }

        /// <summary>
        /// Called when a client exit from radius of this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void ExitRadius(Client client)
        {
            Radius.Remove(client);
            ExitTarget(client);
        }

        /// <summary>
        /// Called when a object update data for the clients in the radius
        /// </summary>
        /// <param name="dataRadius"></param>
        protected void UpdateRadius(Dictionary<string, object> dataRadius)
        {
            Radius.SendFormatedObject(PacketFormat.Format(DefaultFormat.JRadiusStream), new ObjectStream() { Id = Id, ObjectData = UpdateMethods(ref _dataRadius, dataRadius) });
        }

        /// <summary>
        /// Called when a new client view this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void EnterView(Client client)
        {
            client.SendFormatedObject(PacketFormat.Format(DefaultFormat.JViewStream), new ObjectStream() { Id = Id, ObjectData = _dataView });

            Viewer.Add(client);
        }

        /// <summary>
        /// Called when a client exit view this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void ExitView(Client client)
        {
            Viewer.Remove(client);
        }

        /// <summary>
        /// Called when a object update data for the clients view this
        /// </summary>
        /// <param name="dataView"></param>
        protected void UpdateView(Dictionary<string, object> dataView)
        {
            Viewer.SendFormatedObject(PacketFormat.Format(DefaultFormat.JViewStream), new ObjectStream() { Id = Id, ObjectData = UpdateMethods(ref _dataView, dataView) });
        }

        /// <summary>
        /// Called when a new client target this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void EnterTarget(Client client)
        {
            client.SendFormatedObject(PacketFormat.Format(DefaultFormat.JTargetStream), new ObjectStream() { Id = Id, ObjectData = _dataTarget });

            Target.Add(client);
        }

        /// <summary>
        /// Called when client cancel target in this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void ExitTarget(Client client)
        {
            if (Target.Contains(client))
                client.SendFormatedObject(PacketFormat.Format(DefaultFormat.JExitTargetStream), new ObjectStream() { Id = Id });

            Target.Remove(client);
        }

        /// <summary>
        /// Called when a object update data for the clients in target
        /// </summary>
        /// <param name="dataTarget"></param>
        public void UpdateTarget(Dictionary<string, object> dataTarget)
        {
            Target.SendFormatedObject(PacketFormat.Format(DefaultFormat.JTargetStream), new ObjectStream() { Id = Id, ObjectData = UpdateMethods(ref _dataTarget, dataTarget) });
        }

        private Dictionary<string, object> UpdateMethods(ref Dictionary<string, object> objectStart, Dictionary<string, object> objectCompare)
        {
            Dictionary<string, object> objectReturn = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> data in objectCompare)
            {
                if (!objectStart.ContainsKey(data.Key))
                {
                    objectStart[data.Key] = data.Value;
                    objectReturn[data.Key] = data.Value;
                }
                else if (data.Value != objectStart[data.Key])
                {
                    objectStart[data.Key] = data.Value;
                    objectReturn[data.Key] = data.Value;
                }
            }

            return objectReturn;
        }
    }
}

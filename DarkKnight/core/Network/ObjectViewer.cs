using DarkKnight.Network;
using DarkKnight.Utils;
using Newtonsoft.Json;
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
        private ClientCollections clientView = new ClientCollections();
        private ClientCollections clientRadius = new ClientCollections();

        /// <summary>
        /// Called when new client in the radius of this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void EnterRadius(Client client)
        {
            client.SendFormatedString(new Data.PacketFormat(Data.PacketFormat.DarkKnightFormat(Data.DarkKnightFormat.dk_JStreamObj)),
                JsonConvert.SerializeObject(new ObjectStream() { Id = Id, Methods = _dataRadius }));

            clientRadius.Add(client);
        }

        /// <summary>
        /// Called when a client exit from radius of this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void ExitRadius(Client client)
        {
            clientRadius.Remove(client);
        }

        /// <summary>
        /// Called when a object update data for the clients in the radius
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataRadius"></param>
        protected void UpdateRadius(Dictionary<string, object> dataRadius)
        {
            clientRadius.SendFormatedString(new Data.PacketFormat(Data.PacketFormat.DarkKnightFormat(Data.DarkKnightFormat.dk_JStreamObj)),
                JsonConvert.SerializeObject(new ObjectStream() { Id = Id, Methods = UpdateMethods(ref _dataRadius, dataRadius) }));
        }

        /// <summary>
        /// Called when a new client view this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void EnterView(Client client)
        {
            client.SendFormatedString(new Data.PacketFormat(Data.PacketFormat.DarkKnightFormat(Data.DarkKnightFormat.dk_JStreamObj)),
                JsonConvert.SerializeObject(new ObjectStream() { Id = Id, Methods = _dataView }));

            clientView.Add(client);
        }

        /// <summary>
        /// Called when a client exit view this object
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client object</param>
        public void ExitView(Client client)
        {
            clientView.Remove(client);
        }

        /// <summary>
        /// Called when a object update data for the clients view this
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataView"></param>
        protected void UpdateView(Dictionary<string, object> dataView)
        {
            clientView.SendFormatedString(new Data.PacketFormat(Data.PacketFormat.DarkKnightFormat(Data.DarkKnightFormat.dk_JStreamObj)),
                JsonConvert.SerializeObject(new ObjectStream() { Id = Id, Methods = UpdateMethods(ref _dataView, dataView) }));
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

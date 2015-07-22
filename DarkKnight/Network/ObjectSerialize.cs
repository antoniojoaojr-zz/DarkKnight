using DarkKnight.core.Network;

namespace DarkKnight.Network
{
    public abstract class ObjectSerialize
    {
        private int _objectId = ObjectController.objectInstance;

        /// <summary>
        /// Gets the unique id of this instance in the server
        /// </summary>
        public int objectId
        {
            get { return _objectId; }
        }

    }
}

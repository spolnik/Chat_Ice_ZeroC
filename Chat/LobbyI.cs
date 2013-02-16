using System;

namespace Chat
{
    public class LobbyI : LobbyDisp_
    {
        public LobbyI(Ice.ObjectAdapter adapter)
        {
            RoomFactory.Instance.Adapter = adapter;
        }

        public override RoomAccess Join(string nick, string topic, Ice.Identity listenerIdentity, Ice.Current current)
        {
            lock (this)
            {
                if (string.IsNullOrEmpty(nick))
                    throw new IChatException("Nick cannot be null");
                if (string.IsNullOrEmpty(topic))
                    throw new IChatException("Topic cannot be null");
                
                ListenerPrx listener = ListenerPrxHelper.uncheckedCast(current.con.createProxy(listenerIdentity));
        
                RoomPrx roomProxy = RoomFactory.Instance.GetRoom(nick, topic, listener);
                
                Console.WriteLine("Nick: {0}, topic: {1}, listenerIdentity: {2}, {3}", nick, topic, listenerIdentity.name, listenerIdentity.category);

                return new RoomAccess(roomProxy, topic);
            }
        }
    }
}
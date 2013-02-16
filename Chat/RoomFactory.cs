using System.Collections.Generic;
using Ice;
using Object = Ice.Object;

namespace Chat
{
    public interface IRoomFactory
    {
        RoomPrx GetRoom(string nick, string topic, ListenerPrx listener);
        ObjectAdapter Adapter { get; set; }
        void LeaveRoom(string author, string topic);
    }

    public class RoomFactory : IRoomFactory
    {
        private readonly Dictionary<string, RoomPrx> _rooms;
        

        private RoomFactory()
        {
            this._rooms = new Dictionary<string, RoomPrx>();
        }

        public static IRoomFactory Instance
        {
            get { return SingletonCreator.Inst; }
        }

        #region IRoomFactory Members

        public RoomPrx GetRoom(string nick, string topic, ListenerPrx listener)
        {
            if (!this._rooms.ContainsKey(topic)) 
                return this.CreateNewRoomProxies(nick, topic, listener);
            
            var facets = this.Adapter.findAllFacets(this._rooms[topic].ice_getIdentity());

            foreach (Object room in facets.Values)
            {
                if (room is RoomAdmTie_)
                {
                    RoomAdmTie_ roomAdmTie = (RoomAdmTie_) room;
                    RoomI roomAdm = (RoomI) roomAdmTie.ice_delegate();
                    roomAdm.AddListener(nick, listener);
                }
                else if (room is RoomI)
                {
                    RoomI roomI = (RoomI) room;
                    roomI.AddListener(nick, listener);
                }
                else if (room is RoomV2)
                {
                    RoomV2I roomV2I = (RoomV2I) room;
                    roomV2I.AddListener(nick, listener);
                }
            }
            
            return this._rooms[topic];
        }

        public ObjectAdapter Adapter
        {
            get; set;
        }

        public void LeaveRoom(string author, string topic)
        {
            var facets = this.Adapter.findAllFacets(this._rooms[topic].ice_getIdentity());

            foreach (Object room in facets.Values)
            {
                if (room is RoomAdmTie_)
                {
                    RoomAdmTie_ roomAdmTie = (RoomAdmTie_)room;
                    RoomI roomAdm = (RoomI)roomAdmTie.ice_delegate();
                    roomAdm.RemoveListener(author);
                }
                else if (room is RoomI)
                {
                    RoomI roomI = (RoomI)room;
                    roomI.RemoveListener(author);
                }
                else if (room is RoomV2)
                {
                    RoomV2I roomV2I = (RoomV2I)room;
                    roomV2I.RemoveListener(author);
                }
            }
        }

        private RoomPrx CreateNewRoomProxies(string nick, string topic, ListenerPrx listener)
        {
            RoomI room = new RoomI(topic);
            room.AddListener(nick, listener);
            RoomPrx roomPrx = RoomPrxHelper.uncheckedCast(this.Adapter.addWithUUID(room));

            RoomAdmI roomAdm = new RoomAdmI(topic);
            roomAdm.AddListener(nick, listener);
            RoomAdmTie_ roomAdmTie = new RoomAdmTie_(roomAdm);
            this.Adapter.addFacet(roomAdmTie, roomPrx.ice_getIdentity(), "V1");
    
            RoomV2I roomV2 = new RoomV2I(topic);
            roomV2.AddListener(nick, listener);
            this.Adapter.addFacet(roomV2, roomPrx.ice_getIdentity(), "V2");

            this._rooms.Add(topic, roomPrx);
            return roomPrx;
        }

        #endregion

        #region Nested type: SingletonCreator

        private class SingletonCreator
        {
            internal static readonly RoomFactory Inst;

            static SingletonCreator()
            {
                Inst = new RoomFactory();
            }
        }

        #endregion
    }
}
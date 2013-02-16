using System.Collections.Generic;
using System.Linq;
using Ice;

namespace Chat
{
    public class RoomAdmI : RoomI, RoomAdmOperations_
    {
        public RoomAdmI(string name) : base(name)
        {
        }

        public void Kick(string nick, Current current)
        {
            lock (this)
            {
                string topic = current.ctx[SESSIONCTXPROP.value];

                if (this.ListenerPrxs.ContainsKey(nick))
                    RoomFactory.Instance.LeaveRoom(nick, topic);
                else
                    throw new IChatException(string.Concat("Member not exist in room: ", nick));
            }
        }

        public List<string> GetParticipants(Current current)
        {
            lock (this)
            {
                return this.ListenerPrxs.Keys.Select(key => key).ToList();
            }
        }
    }
}
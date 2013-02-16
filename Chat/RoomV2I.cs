using System.Collections.Generic;
using Ice;

namespace Chat
{
    public class RoomV2I : RoomV2Disp_
    {
        private readonly string _topic;
        private readonly Dictionary<string, ListenerPrx> _listenerPrxs;
        
        public RoomV2I(string topic) 
        {
            this._topic = topic;
            this._listenerPrxs = new Dictionary<string, ListenerPrx>();
        }

        internal void AddListener(string nick, ListenerPrx listener)
        {
            this._listenerPrxs.Add(nick, listener);
        }

        internal void RemoveListener(string nick)
        {
            this._listenerPrxs.Remove(nick);
        }

        public override string GetRoomName(Current current)
        {
            return this._topic;
        }

        public override void Say(string message, long time, Current current)
        {
            lock (this)
            {
                string author = current.ctx[AUTHORCTXPROP.value];

                if (!this._listenerPrxs.ContainsKey(author))
                    throw new IllegalChatSessionException("You are not a member", current.ctx[SESSIONCTXPROP.value]);

                Post post = new Post(current.ctx[AUTHORCTXPROP.value], message, time);

                foreach (string key in this._listenerPrxs.Keys)
                {
                    IListenerCallback listenerCallback = new ListenerCallback();
                    this._listenerPrxs[key].begin_NotifyPost(post).whenCompleted(listenerCallback.SayCallback,
                                                                     listenerCallback.FailureCallback);
                }
            }
        }

        public override void LeaveRoom(Current current)
        {
            lock (this)
            {
                string author = current.ctx[AUTHORCTXPROP.value];
                string topic = current.ctx[SESSIONCTXPROP.value];

                if (!this._listenerPrxs.ContainsKey(author))
                    throw new IllegalChatSessionException("Your session is expired or deleted", author);

                RoomFactory.Instance.LeaveRoom(author, topic);
            }
        }
    }
}
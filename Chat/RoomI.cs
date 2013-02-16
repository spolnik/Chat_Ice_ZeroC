using System.Collections.Generic;
using Ice;

namespace Chat
{
    public class RoomI : RoomDisp_
    {
        protected string Topic;
        protected readonly Dictionary<string, ListenerPrx> ListenerPrxs;

        public RoomI(string topic)
        {
            this.Topic = topic;
            this.ListenerPrxs = new Dictionary<string, ListenerPrx>();
        }

        internal void AddListener(string nick, ListenerPrx listener)
        {
            this.ListenerPrxs.Add(nick, listener);
        }

        internal void RemoveListener(string nick)
        {
            this.ListenerPrxs.Remove(nick);
        }

        public override string GetRoomName(Current current)
        {
            return this.Topic;
        }

        public override void Say(string message, Current current)
        {
            lock (this)
            {
                string author = current.ctx[AUTHORCTXPROP.value];

                if (!this.ListenerPrxs.ContainsKey(author))
                    throw new IllegalChatSessionException("You are not a member", current.ctx[SESSIONCTXPROP.value]);

                Post post = new Post(current.ctx[AUTHORCTXPROP.value], message, 0);

                foreach (string key in this.ListenerPrxs.Keys)
                {
                    IListenerCallback listenerCallback = new ListenerCallback();
                    this.ListenerPrxs[key].begin_NotifyPost(post).whenCompleted(listenerCallback.SayCallback,
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

                if (!this.ListenerPrxs.ContainsKey(author))
                    throw new IllegalChatSessionException("Your session is expired or deleted", author);

                RoomFactory.Instance.LeaveRoom(author, topic);
            }
        }
    }
}
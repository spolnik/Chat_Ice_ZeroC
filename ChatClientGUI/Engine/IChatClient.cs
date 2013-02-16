using System.Collections.Generic;

namespace Chat.Engine
{
    internal interface IChatClient
    {
        Ice.Communicator Communicator { get; }
        void Join(string name, string topic, RoomVersion roomVersion);
        void Say(string message);
        void Leave();
        List<string> GetParticipants();
        void Kick(string nick);
    }
}
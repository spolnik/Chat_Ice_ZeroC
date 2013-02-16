#include <Ice/Identity.ice>

module Chat {
	const string SESSIONCTXPROP = "Chat_CTX_2007";
	const string AUTHORCTXPROP = "Chat_Post_Author_CTX_2007";

	exception IChatException {
		string Reason;
	};

	exception IllegalChatSessionException extends IChatException {
		string SessionId;
	};

	["clr:property"] struct Post {
		string Author;
		string Message;
		long time;
	};

	interface Listener {
		[ "amd" ] void NotifyPost(Post newPost);
	};

	interface Room {
		idempotent string GetRoomName();
		void Say(string message) throws IllegalChatSessionException;
		void LeaveRoom() throws IllegalChatSessionException;
	};

	interface RoomV2 {
		idempotent string GetRoomName();
		void Say(string message, long time) throws IllegalChatSessionException;
		void LeaveRoom() throws IllegalChatSessionException;
	};

	["clr:generic:List"] sequence<string> Participants;

	interface RoomAdm extends Room {
		void Kick(string nick) throws IChatException;
		idempotent Participants GetParticipants();
	};

	["clr:property"] struct RoomAccess {
		Room* RoomProxy;
		string SessionId;
	};	

	interface Lobby {
		RoomAccess Join(string nick, string topic, Ice::Identity listenerIdentity) throws IChatException;
	};
};
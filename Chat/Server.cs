using System;
using Ice;
using Object = Ice.Object;

namespace Chat
{
    class Server : Application
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            InitializationData initializationData = new InitializationData {properties = Util.createProperties()};
            initializationData.properties.setProperty("Ice.ThreadPool.Client.Size", "1");
            initializationData.properties.setProperty("Ice.ThreadPool.Client.SizeMax", "10");
            initializationData.properties.setProperty("Ice.ThreadPool.Server.Size", "1");
            initializationData.properties.setProperty("Ice.ThreadPool.Server.SizeMax", "10");
            initializationData.properties.setProperty("Ice.ACM.Client", "0");

            Environment.Exit(server.main(args, initializationData));
        }

        public override int run(string[] args)
        {
            int status = 0;
            try
            {
                ObjectAdapter adapter = communicator().createObjectAdapterWithEndpoints("ChatLobbyAdapter", "default -p 12321");
                
                Object obj = new LobbyI(adapter);

                adapter.add(obj, communicator().stringToIdentity("ChatLobby"));
                adapter.activate();
                communicator().waitForShutdown();
            }
            catch (Ice.Exception ex)
            {
                Console.Error.WriteLine(ex);
                status = 1;
            }

            if (interrupted())
                Console.Error.WriteLine("{0}: terminating", appName());

            return status;
        }
    }
}

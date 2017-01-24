# DarkKnight
Is a free and open sourcer 'server' writen in C# .NET prepared to support a massive virtual world

You will be able to build your application using the DarkKnight that will proportional scalability to manage all connections and packages that your application can receive.

### Package documentation
At the moment the focus is on branch "dev" is where you'll find better information

### Soon a complete documentation

Example to use:

```c#
    /// <summary>
    /// your classe needs be implement DarkKnight.DKService
    /// </summary>
    class Example : DarkKnight.DKService
    {
        public Example()
        {
            // In the class constructor, you can call the setApplication method by passing the class itself, 
            // which means that the server will always fetch its class as a control controller for new connected, 
            // disconnected, and incoming packates from clients.
            setApplication(this);
        }
        
        /// <summary>
        /// This method is called automatic when the client connection is closed
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client obj</param>
        public override void connectionClosed(Client client)
        {
            // you customize your code here
            // example
            // remove the client from your controller clients connecteds
        }

        /// <summary>
        /// This method is called automatic when a new client connected with the server
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client obj</param>
        public override void connectionOpened(Client client)
        {
            // you customize your code here
            // example
            // add the Client object in your controller of clients connecteds
        }

        /// <summary>
        /// This method is called automatic when a client send a new package to the server
        /// The Client object is client owner of package
        /// The Packet object is packet data received from client
        /// </summary>
        /// <param name="client">DarkKnight.Network.Client obj</param>
        /// <param name="packet">DarkKnight.Data.Packet obj</param>
        public override void ReceivedPacket(Client client, Packet packet)
        {
            // you customize your code here
            // example
            // validate the package received from the client is valid
            // and process and response the client
            // like
            if(packet.dataString == "CONNECTION")
            {
                client.SendFormatedString(new PacketFormat("Hello"), "World!");
            }
        }
    }
 ```

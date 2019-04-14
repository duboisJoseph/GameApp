using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GameApp
{
  public class AsynchronousSocketListener
  {
    public static ManualResetEvent allDone = new ManualResetEvent(false);
    static List<StateObject> Handlers = new  List<StateObject>();

    public AsynchronousSocketListener(IPAddress IP, int port, int handlerPool)
    {
      StartListening(IP, port, handlerPool);
    }

    public List<StateObject> GetStateObjects()
    {
      return Handlers;
    }

    bool StartListening(IPAddress IP, int port, int handlerPool)
    {
      IPEndPoint localEndPoint = new IPEndPoint(IP, port);
      Socket ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      int handlerCount = 0;
      try
      {
        ListenerSocket.Bind(localEndPoint);
        ListenerSocket.Listen(100);

        bool keepListening = true;
        while (keepListening)
        {
          //Set event to nonsignaled state.
          allDone.Reset();

          //Start socket listening for connection
    
          Console.WriteLine("Waiting for a connection...");
          ListenerSocket.BeginAccept( new AsyncCallback(AcceptCallback), ListenerSocket);

          //wait for connection to be complete
          allDone.WaitOne();
          handlerCount++;
          if (handlerCount == handlerPool)
          {
            keepListening = false;
          }
          Application.DoEvents();
        }
      } catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        return false;
      }
      ListenerSocket.Dispose();
      return true;
    }

    private static void AcceptCallback(IAsyncResult ar)
    {
      // Signal the main thread to continue.  
      allDone.Set();

      // Get the socket that handles the client request.  
      Socket listener = (Socket)ar.AsyncState;
      Socket handler = listener.EndAccept(ar);

      // Create the state object.  
      StateObject state = new StateObject
      {
        workSocket = handler
      };
      Handlers.Add(state);
      //listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);  
    }

    private static void EndAsyncListener(Socket handler)
    {
      handler.Shutdown(SocketShutdown.Both);
      handler.Close();
    }
  }
}

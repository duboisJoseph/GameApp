using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace GameApp
{
  public class AsynchronousClient
  {
    // ManualResetEvent instances signal completion.  
    public static ManualResetEvent connectDone = new ManualResetEvent(false);
    public static ManualResetEvent sendDone = new ManualResetEvent(false);
    public static ManualResetEvent receiveDone = new ManualResetEvent(false);

    private static String response = string.Empty;

    public AsynchronousClient() { }

    public string getResponse(Socket client)
    {
      Receive(client);
      receiveDone.WaitOne();
      return response;
    }

    public static Socket StartClient(IPAddress IP, int Port)
    {
      //Begin Connection
      try
      {
        //Define IPendpoint
        IPEndPoint remoteEP = new IPEndPoint(IP, Port);


        //Create Socket
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
        connectDone.WaitOne();
        Console.WriteLine("Moved past ConnectDone");
        
        /*
          // Send test data to the remote device.  
          Send(client, "This is a test<EOF>");
          sendDone.WaitOne();
     
          // Receive the response from the remote device.  
          Receive(client);
          receiveDone.WaitOne();

          // Write the response to the console.  
          Console.WriteLine("Response received : {0}", response);
        */
        // Release the socket.  
        return client;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        return null;
      }  
    }

    public static void EndClient(Socket client)
    {
      // Release the socket.  
      client.Shutdown(SocketShutdown.Both);
      client.Close();
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
      try
      {
        // Retrieve the socket from the state object.  
        Socket client = (Socket)ar.AsyncState;

        // Complete the connection.  
        client.EndConnect(ar);

        Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

        // Signal that the connection has been made.  
        connectDone.Set();

        Console.WriteLine("ConnectDoneSet");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    public static void Receive(Socket client)
    {
      Console.WriteLine("Begun Receive");
      try
      {
        receiveDone.Reset();
        // Create the state object.  
        StateObject state = new StateObject();
        state.workSocket = client;

        // Begin receiving the data from the remote device.  
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
      try
      {
        Console.WriteLine("ReceiveCallBAckEntered");
        // Retrieve the state object and the client socket   
        // from the asynchronous state object.  
        StateObject state = (StateObject)ar.AsyncState;
        Socket client = state.workSocket;

        // Read data from the remote device.  
        Console.WriteLine("Stuck 1");
        int bytesRead = client.EndReceive(ar);
        Console.WriteLine("Stuck 2");
        if (bytesRead > 0)
        {
          Console.WriteLine("Went Here 1");
          // There might be more data, so store the data received so far.  
          state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
          Console.WriteLine("Stuck 3");

          // Get the rest of the data.  
          client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
          Console.WriteLine("Stuck 4");

        }
        else
        {
          Console.WriteLine("Went Here 2");
          // All the data has arrived; put it in response.  
          if (state.sb.Length > 1)
          {
            Console.WriteLine("Went Here 3");
            response = state.sb.ToString();
            Console.WriteLine("readMessage" + response);
          }
          // Signal that all bytes have been received.  
          receiveDone.Set();
          Console.WriteLine("All Finished Receiving");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
      Console.WriteLine("Stuck 5");
    }

    public static void Send(Socket client, String data)
    {

      Console.WriteLine("Begun Send");
      // Convert the string data to byte data using ASCII encoding.  
      byte[] byteData = Encoding.ASCII.GetBytes(data);

      // Begin sending the data to the remote device.  
      client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
      Console.WriteLine("Completed Send");
    }

    private static void SendCallback(IAsyncResult ar)
    {
      Console.WriteLine("Send Call Back");
      try
      {
        // Retrieve the socket from the state object.  
        Socket client = (Socket)ar.AsyncState;

        // Complete sending the data to the remote device.  
        int bytesSent = client.EndSend(ar);

        Console.WriteLine("Sent data");
        Console.WriteLine("Sent {0} bytes to server.", bytesSent);

        // Signal that all bytes have been sent.  
        sendDone.Set();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
      Console.WriteLine("End Send Call Back");
    }
  }
}
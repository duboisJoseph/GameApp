using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GameApp
{
  public partial class TicTacToePlayer : Form
  {
    public bool IpValidated { get; private set; }
    public bool PortValidated { get; private set; }
    public bool IAmHost { get; private set; }

    private Socket SocketConnectedToLobby;
    private Socket ListenerSocket;
    private IPEndPoint HostIPEndPoint;
    private Socket PeerSocket1;
    private int playerCount;
    private string LobbyOwner;
    private List<string> players = new List<string>();

    public TicTacToePlayer()
    {
      InitializeComponent();
    }
    private void SendMove(int v)
    {

      string moveString = "I the pressed ";
      if(IAmHost)
      {
        //Store Move locally
        switch (v)
        {
          case 1:
            {
              moveString += "ROCK";
              break;
            }
          case 2:
            {
              moveString += "PAPER";
              break;
            }
          case 3:
            {
              moveString += "SCISSORS";
              break;
            }
          default:
            {
              break;
            }
        }
        LogBox.Text += "\n " + moveString;
        //Wait for player moves

        //Get Player moves

        //Calculate Victor

        //Send Results to players
      } else
      {
        //Craft Move String
        switch (v)
        {
          case 1:
            {
              moveString += "ROCK";
              break;
            }
          case 2:
            {
              moveString += "PAPER";
              break;
            }
          case 3:
            {
              moveString += "SCISSORS";
              break;
            }
          default:
            {
              break;
            }
        }
        LogBox.Text += "\n " + moveString;
        
        //Send Move

        //Recive Ack
      }
    }

    private int TransmitString(Socket socket, string msg)
    {
      byte[] sendBuffer = Encoding.UTF8.GetBytes(msg);
      int bytesSent = socket.Send(sendBuffer);
      return bytesSent;
    }

    private string ReceiveString(Socket socket)
    {
      byte[] receiveBuffer = new byte[1024];
      int bytesReceived = socket.Receive(receiveBuffer);
      return Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
    }

    private int TransmitFile(Socket socket, string filePath, string fileName)
    {
      TransmitString(socket, "!fi");

      bool clientNotReady = true;
      while (clientNotReady)
      {
        if (ReceiveString(socket) == "!go")
        {
          clientNotReady = false;
          socket.SendFile(filePath + fileName);
        }
      }
      return 1;
    }

    private int ReceiveFile(Socket socket, string completePath)
    {
      byte[] fileBytes = new byte[8192];
      
      TransmitString(socket, "!go");

      int numBytes = socket.Receive(fileBytes);

      File.WriteAllBytes(completePath,fileBytes);

      return numBytes;
    }

    private void HostLobby()
    {
      playerCount = 0;

      IAmHost = true;

      //Create Listener
      ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      HostIPEndPoint = new IPEndPoint(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text));
      ListenerSocket.Bind(HostIPEndPoint);

      //Listen
      ListenerSocket.Listen(40);
      StatusLbl.Text = "Waiting for players";
      bool keepListening = true;

      //Accept Socket
      while (keepListening)
      {
        if (ListenerSocket.Poll(100, SelectMode.SelectRead))
        {
          PeerSocket1 = ListenerSocket.Accept();
          playerCount++;
          keepListening = false;
        }
        Application.DoEvents();
      }

      //Send Greeting
      int bytesSent = TransmitString(PeerSocket1, PlayerNameBox.Text);

      //Receive Response
      string connectedPlayer = ReceiveString(PeerSocket1);

      LogBox.Text += "\n" + connectedPlayer + " joined the game.";

      players.Add(connectedPlayer);

      StatusLbl.Text = playerCount + " connected players";

      keepListening = true;

      while (keepListening)
      {
        if (PeerSocket1.Poll(100, SelectMode.SelectRead))
        {
          LogBox.Text += "\n FileRequest: " + ReceiveString(PeerSocket1);
          playerCount++;
          keepListening = false;
        }
        Application.DoEvents();
      } 
      TransmitFile(PeerSocket1, @"C:\Users\joe\Desktop\Test1\", "TestFile.txt");
              
      LogBox.Text += "\n Completed all tests!";
    }

    private void JoinLobby()
    {
      IAmHost = false;
      //Create Socket
      SocketConnectedToLobby = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      HostIPEndPoint = new IPEndPoint(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text));

      //Connect To Lobby
      StatusLbl.Text = "Connecting to game";
      SocketConnectedToLobby.Connect(HostIPEndPoint);

      //Receive Greeting
      LobbyOwner = ReceiveString(SocketConnectedToLobby);

      LogBox.Text += "\n Joined " + LobbyOwner + "'s game.";
      StatusLbl.Text = "Connected";

      //Send player name
      int bytesSent = TransmitString(SocketConnectedToLobby, PlayerNameBox.Text);

      TransmitString(SocketConnectedToLobby, "?fi");
      LogBox.Text += "\n Transmitted FileRequest";

      bool keepWaitingForFile = true;

      while (keepWaitingForFile)
      {
        ReceiveFile(SocketConnectedToLobby, @"C:\Users\joe\Desktop\Test2\TestFileRec.txt");
      }
    }

    private bool ValidateIP(string text)
    {
      if (text.Length > 6)
        return IPAddress.TryParse(text, out IPAddress IP);
      else
        return false;
    }

    private bool ValidatePort(string text)
    {
      if (text.Length > 3)
        return int.TryParse(text, out int result);
      else
        return false;
    }

    private void RockBtn_Click(object sender, EventArgs e)
    {
      SendMove(1);
    }

    private void PaperBtn_Click(object sender, EventArgs e)
    {
      SendMove(2);
    }

    private void ScissorsBtn_Click(object sender, EventArgs e)
    {
      SendMove(3);
    }

    private void HostBtn_Click(object sender, EventArgs e)
    {
      LogBox.Text += "\n Hosting Lobby....";
      HostLobby();
    }

    private void JoinBtn_Click(object sender, EventArgs e)
    {
      LogBox.Text += "\n Joining Lobby....";
      JoinLobby();
    }

    private void IPBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidateIP(IPBox.Text))
      {
        IpValidated = true;
      } else
      {
        IpValidated = false;
      }

      if(IpValidated && PortValidated)
      {
        HostBtn.Enabled = true;
        JoinBtn.Enabled = true;
      }
    }

    private void PortBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidatePort(PortBox.Text))
      {
        PortValidated = true;
      } else
      {
        PortValidated = false;
      }

      if (IpValidated && PortValidated)
      {
        HostBtn.Enabled = true;
        JoinBtn.Enabled = true;
      }
    }

    private void PlayerNameBox_TextChanged(object sender, EventArgs e)
    {
      IPBox_TextChanged(sender, e);
      PortBox_TextChanged(sender, e);
    }
  }
}

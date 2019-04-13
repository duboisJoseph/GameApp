﻿using System;
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
    private int playerCount;
    private string LobbyOwner;
    private string chosenMovement;
    private List<string> players = new List<string>();
    private static List<StateObject> ClientStates;
    private static StateObject HostState = new StateObject();


    public TicTacToePlayer()
    {
      InitializeComponent();
    }

    private void HostLobby()
    {
      playerCount = 2;
      Console.WriteLine("Started");
      AsynchronousSocketListener ASL = new AsynchronousSocketListener(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text), playerCount);

      Console.WriteLine("Moved Here 1");
      IAmHost = true;

      ClientStates = ASL.GetStateObjects();
      Console.WriteLine("Moved Here 2");
      /*
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
      */
      int i = 0;
      foreach(StateObject s in ClientStates)
      {
        s.id = i;
        i++;
      }

      HostTransmitString(PlayerNameBox.Text);

      HostReceiveString();

      foreach (StateObject s in ClientStates)
      {
        s.name = s.sb.ToString();
        LogBox.Text += "\n " + s.name + " connected...";
      }
    }

    private void JoinLobby()
    {
      IAmHost = false;

      bool endGame = false;

      SocketConnectedToLobby = AsynchronousClient.StartClient(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text));

      /*//Create Socket
      SocketConnectedToLobby = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      HostIPEndPoint = new IPEndPoint(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text));

      //Connect To Lobby
      StatusLbl.Text = "Connecting to game";
      SocketConnectedToLobby.Connect(HostIPEndPoint);
      */
      //Receive Greeting

      Console.WriteLine("Waiting For Host's Name");
      LobbyOwner = ReceiveString(SocketConnectedToLobby);

      LogBox.Text += "\n Joined " + LobbyOwner + "'s game.";

      StatusLbl.Text = "Connected";

      //Send player name
      TransmitString(SocketConnectedToLobby, PlayerNameBox.Text);

      //TransmitString(SocketConnectedToLobby, PlayerNameBox.Text.ToString() + " is now waiting.");
      LogBox.Text += "\n Sucessfully joined the game.";

      while (!endGame)
      {
        if (SocketConnectedToLobby.Poll(100, SelectMode.SelectRead))
        {
          LogBox.Text += "\nReceived: " + ReceiveString(SocketConnectedToLobby);
          //keepListening = false;
        }
        Application.DoEvents();
      }
    }

    private void TransmitString(Socket socket, string msg)
    {
      byte[] sendBuffer = Encoding.UTF8.GetBytes(msg);
      int bytesSent = socket.Send(sendBuffer);
    }

    private void HostTransmitString(string msg)
    {
      foreach (StateObject s in ClientStates)
      {
        TransmitString(s.workSocket, msg);
        //ReceiveString(s.workSocket, s);
      }
    }

    private void HostReceiveString()
    {
      foreach (StateObject s in ClientStates)
      {
        s.sb.Append(ReceiveString(s.workSocket));
      }
    }

    private string ReceiveString(Socket socket)
    {
      byte[] receiveBuffer = new byte[1024];
      int bytesReceived = socket.Receive(receiveBuffer);
      return Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
    }

    private void SendMove(int v)
    {

      string moveString = "You selected ";
      if (IAmHost)
      {
        //Store Move locally
        switch (v)
        {
          case 1:
            {
              moveString += "ROCK";
              chosenMovement = "1";
              break;
            }
          case 2:
            {
              moveString += "PAPER";
              chosenMovement = "2";
              break;
            }
          case 3:
            {
              moveString += "SCISSORS";
              chosenMovement = "3";
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
      }
      else
      {
        //Craft Move String
        switch (v)
        {
          case 1:
            {
              moveString += "ROCK";
              chosenMovement = "1";
              break;
            }
          case 2:
            {
              moveString += "PAPER";
              chosenMovement = "2";
              break;
            }
          case 3:
            {
              moveString += "SCISSORS";
              chosenMovement = "3";
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

        private void button1_Click(object sender, EventArgs e)
        {
            int bytesSent;
            //Send move to host:
            if (!IAmHost)
            {
                TransmitString(SocketConnectedToLobby, PlayerNameBox.Text.ToString() + " " + chosenMovement);
                LogBox.Text += "\n " + "Move sent.";
                button1.Enabled = false;

            }
            //Send move to players:
            else
            {
                //TransmitString(PeerSocket1, PlayerNameBox.Text.ToString() + " " + chosenMovement);
                LogBox.Text += "\n " + "Move sent to all players.";
            }

        }
    }
}

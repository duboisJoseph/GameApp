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
    private int playerCount, totalReceived;
    private string LobbyOwner;
    private string chosenMovement;
    private List<string> players = new List<string>();
    private static List<StateObject> ClientStates;
    private static StateObject HostState = new StateObject();
    private Timer MyTimer;
    Boolean roundOver = false;
    Boolean playerSet = false;      //set player names
    int x = 0;
    string[,] playerANDMovement = new String[3, 3];


    public TicTacToePlayer()
    {
      InitializeComponent();
      InitializeTimer();
    }


    private void InitializeTimer()
    {
      MyTimer = new Timer
      {
        // Call this procedure when the application starts.  
        // 1000 = 1 second.  
        Interval = 100
      };
      MyTimer.Tick += new EventHandler(MyTimer_tick);

      // Enable timer.  
      MyTimer.Enabled = false;
    }

    private void MyTimer_tick(object Sender, EventArgs e) //Event handler for Timer ticks.
    {
      //Here is where we read from the sockets and handle game logic
      if (IAmHost)
      {
        HostReceiveString();
        foreach (StateObject s in ClientStates)
        {
          if (s.sbChanged)
          {
            //LogBox.Text += "\n " + s.name + " says " + s.sb.ToString();
            if (!playerSet)
            {
              LogBox.Text = s.sb.ToString() + " : in-game." + "\n" + LogBox.Text;
              string[] tokens = s.sb.ToString().Split(' ');

              //Get movement
              if (playerTwoLabel.Text.ToString() == tokens[0] && tokens[1] == "movement")
              {
                playerANDMovement[1, 0] = playerTwoLabel.Text.ToString();
                playerANDMovement[1, 1] = tokens[3];
                PlyTwoScoreLbl.Text = playerANDMovement[1, 1];
                totalReceived++;
              }
              if (playerThreeLabel.Text.ToString() == tokens[0] && tokens[1] == "movement")
              {
                playerANDMovement[2, 0] = playerThreeLabel.Text.ToString();
                playerANDMovement[2, 1] = tokens[3];
                PlyThreeScoreLbl.Text = playerANDMovement[2, 1];
                totalReceived++;
              }

            }
            x++;
            s.sbChanged = false;
            if (x == 1 && !playerSet)
            {
              playerTwoLabel.Text = s.sb.ToString();
              HostTransmitString(s.sb.ToString() + " is player two.\n ");
            }
            else if (x == 2 && !playerSet)
            {
              playerThreeLabel.Text = s.sb.ToString();
              HostTransmitString(s.sb.ToString() + " is player three.\n ");
            }
          }

        }
        //IF ALL MOVEMENT HAS BEEN RECEIVED, SEND TO ALL PLAYERS:
        if (totalReceived == playerCount + 1)
        {
          HostTransmitString("ALL " + playerANDMovement[0, 0] + " " + playerANDMovement[0, 1] + " " + playerANDMovement[1, 0] + " " + playerANDMovement[1, 1] + " " + playerANDMovement[2, 0] + " " + playerANDMovement[2, 1]);
          totalReceived = 0;

          //ALEC ALLAIN DO YOUR DETERMINE VICTOR HERE FOR THE HOST OF THE GAME. THEN SEND RESULT OUT TO PLAYERS OR YOU CAN HAVE EACH INDIVIDUAL CLIENT DETERMINE WHO WON. UP TO YOU <-----------------------
          DetermineVictor(playerANDMovement);
        }
      }
      else
      {
        //NEW MESSAGES COMING IN FOR THE CLIENT:
        if (SocketConnectedToLobby.Poll(100, SelectMode.SelectRead))
        {
          String newlyReceived = "\n " + ReceiveString(SocketConnectedToLobby);
          LogBox.Text = newlyReceived + "\n" + LogBox.Text;
          string[] tokens = newlyReceived.Split(' ');
          //Handle read string here as the client!

          //Update Names of Players in box if we haven't updated it:
          if (!playerSet)
          {
            if (newlyReceived.Contains("two"))
            {
                playerTwoLabel.Text = tokens[1];
            }
            if (newlyReceived.Contains("three"))
            {
                playerThreeLabel.Text = tokens[5];
            }
          }

          //players now set
          playerSet = true;

          if (newlyReceived.Contains("ALL"))
          {
            //GET ALL THE PLAYER MOVEMENTS FOR THE CLIENT:
            //if (tokens[1] == "ALL")
            //{
            playerANDMovement[0, 0] = tokens[2];
            playerANDMovement[0, 1] = tokens[3];
            if (playerOneLabel.Text.ToString() == tokens[2])
            {
                PlyOneScoreLbl.Text = playerANDMovement[0, 1];
            }

            playerANDMovement[1, 0] = tokens[4];
            playerANDMovement[1, 1] = tokens[5];
            if (playerTwoLabel.Text.ToString() == tokens[4])
            {
                PlyTwoScoreLbl.Text = playerANDMovement[1, 1];
            }

            playerANDMovement[2, 0] = tokens[6];
            playerANDMovement[2, 1] = tokens[7];
            PlyThreeScoreLbl.Text = playerANDMovement[2, 1];

            //}
          }
          else if (newlyReceived.Contains("RESET"))
          {
            ResetGame();
          }

        }
      }
    }

    private void HostLobby()
    {
      playerCount = 2; //NOTE THIS Defines the number of users you need to start game;
      playerOneLabel.Text = PlayerNameBox.Text.ToString();


      Console.WriteLine("New Lobby Started");
      AsynchronousSocketListener ASL = new AsynchronousSocketListener(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text), playerCount);

      Console.WriteLine("Moved Here 1");
      IAmHost = true;

      ClientStates = ASL.GetStateObjects();
      Console.WriteLine("Moved Here 2");

      int i = 0;
      foreach (StateObject s in ClientStates)
      {
          s.id = i;
          i++;
      }

      HostTransmitString(PlayerNameBox.Text);

      HostReceiveString();

      foreach (StateObject s in ClientStates)
      {
          s.name = s.sb.ToString();
          LogBox.Text = s.name + " connected..." + "\n" + LogBox.Text;
      }

      MyTimer.Start();
    }

    private void JoinLobby()
    {
      IAmHost = false;

      SocketConnectedToLobby = AsynchronousClient.StartClient(IPAddress.Parse(IPBox.Text), int.Parse(PortBox.Text));

      Console.WriteLine("Waiting For Host's Name");
      LobbyOwner = ReceiveString(SocketConnectedToLobby);

      LogBox.Text = "Joined " + LobbyOwner + "'s game.\n" + LogBox.Text;

      StatusLbl.Text = "Connected";
      playerOneLabel.Text = LobbyOwner;

      //Send player name
      TransmitString(SocketConnectedToLobby, PlayerNameBox.Text);

      LogBox.Text = "Sucessfully joined the game.\n" + LogBox.Text;

      MyTimer.Start();
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
      }
    }

    private bool HostCheckForRead(Socket client)
    {
      if (client.Poll(100, SelectMode.SelectRead))
      {
          return true;
      }
      return false;
    }

    private void HostReceiveString()
    {
      foreach (StateObject s in ClientStates)
      {
        if (HostCheckForRead(s.workSocket))
        {
          s.sb.Append(ReceiveString(s.workSocket));
          s.sbChanged = true;
        }
        else
        {
          s.sbChanged = false;
        }

        //Handle read strings here.

      }
    }

    private string ReceiveString(Socket socket)
    {
      byte[] receiveBuffer = new byte[1024];
      int bytesReceived = 0;
      try
      {
          bytesReceived = socket.Receive(receiveBuffer);
      }
      catch (SocketException e)
      {
          Console.WriteLine("Exception: " + e);
          Environment.Exit(1);
      }
      return Encoding.UTF8.GetString(receiveBuffer, 0, bytesReceived);
    }

    private Dictionary<string, string> GetStringsFromClients(string name, string move)
    {
      Dictionary<string, string> playerMoves = new Dictionary<string, string>();

      return playerMoves;
    }

    private void DetermineVictor(string[,] players) {
      int score;
      int playerOne;
      int playerTwo;
      int playerThree;
            
      for (int i = 0; i < players.GetUpperBound(0); i++) {
        for (int j = i + 1; j < players.GetUpperBound(0); j++) {
          if (players[i,1] == "Rock" && players[j,1] == "Scissors") {
            Console.WriteLine("\n" + players[i,0] + " wins over " + players[j,0]);
            score = int.Parse(players[i,2]);
            score++;
            players[i,2] = score.ToString();
          } else if (players[j,1] == "Rock" && players[i,1] == "Scissors") {
            Console.WriteLine("\n" + players[j,0] + " wins over " + players[i,0]);
            score = int.Parse(players[j,2]);
            score++;
            players[j,2] = score.ToString();
          }

          if (players[i,1] == "Scissors" && players[j,1] == "Paper") {
            Console.WriteLine("\n" + players[i,0] + " wins over " + players[j,0]);
            score = int.Parse(players[i,2]);
            score++;
            players[i,2] = score.ToString();
          } else if (players[j,1] == "Rock" && players[i,1] == "Scissors") {
            Console.WriteLine("\n" + players[j,0] + " wins over " + players[i,0]);
            score = int.Parse(players[j,2]);
            score++;
            players[j,2] = score.ToString();
          }

          if (players[i,1] == "Paper" && players[j,1] == "Rock") {
            Console.WriteLine("\n" + players[i,0] + " wins over " + players[j,0]);
            score = int.Parse(players[i,2]);
            score++;
            players[i,2] = score.ToString();
          } else if (players[i,1] == "Paper" && players[j,1] == "Rock") {
            Console.WriteLine("\n" + players[j,0] + " wins over " + players[i,0]);
            score = int.Parse(players[j,2]);
            score++;
            players[j,2] = score.ToString();
          }
        }
      }

      playerOne = int.Parse(players[0,2]);
      playerTwo = int.Parse(players[1,2]);
      playerThree = int.Parse(players[2,2]);

      if (playerOne > playerTwo && playerOne > playerThree) {
        Console.WriteLine(players[0,0] + " wins this game!");
        LogBox.Text = players[0,0] + " wins this game!" + "\n" + LogBox.Text;
      } else if (playerTwo > playerOne && playerTwo > playerThree) {
        Console.WriteLine(players[1,0] + " wins this game!");
        LogBox.Text = players[1,0] + " wins this game!" + "\n" + LogBox.Text;
      } else if (playerThree > playerOne && playerThree > playerTwo) {
        Console.WriteLine(players[2,0] + " wins this game!");
        LogBox.Text = players[2,0] + " wins this game!" + "\n" + LogBox.Text;
      }
            
        //roundOver = true;
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
        LogBox.Text = moveString + "\n" + LogBox.Text;
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
        LogBox.Text = moveString + "\n" + LogBox.Text;

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
      LogBox.Text = "Hosting Lobby....\n" + LogBox.Text;
      HostLobby();
    }

    private void JoinBtn_Click(object sender, EventArgs e)
    {
      LogBox.Text = "Joining Lobby....\n" + LogBox.Text;
      JoinLobby();
    }

    private void IPBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidateIP(IPBox.Text))
      {
         IpValidated = true;
      }
      else
      {
         IpValidated = false;
      }

      if (IpValidated && PortValidated)
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
      }
      else
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

    private void Button1_Click(object sender, EventArgs e)
    {
      //Send move to host:
      if (!IAmHost)
      {
          TransmitString(SocketConnectedToLobby, " movement is " + chosenMovement);
          LogBox.Text = "Move sent.\n" + LogBox.Text;
          SendMoveBtn.Enabled = false;

      }
      //Send move to players:
      else
      {
        if (!roundOver)
        {
            HostTransmitString(playerOneLabel.Text + " movement is " + chosenMovement);
            playerANDMovement[0, 0] = playerOneLabel.Text.ToString();
            playerANDMovement[0, 1] = chosenMovement;
            PlyOneScoreLbl.Text = playerANDMovement[0, 1];                  //LABEL FOR TESTING
            totalReceived++;
            LogBox.Text = "Move sent to all players.\n" + LogBox.Text;
            SendMoveBtn.Text = "New Game";
            roundOver = true;
        }
        else
        {
            SendMoveBtn.Text = "Send Move";
            LogBox.Text += "reset\n" + LogBox.Text;
            ResetGame();
            HostTransmitString("RESET");
            roundOver = false;
        }
      }
    }

    /**
      * Method for reseting game once a victor has been determined.
      * */
    private void ResetGame()
    {
      PlyOneScoreLbl.Text = "...";
      PlyTwoScoreLbl.Text = "...";
      PlyThreeScoreLbl.Text = "...";
      chosenMovement = "";

      for (int i = 0; i < 3; i++)
      {
         playerANDMovement[i, 1] = "";
      }

      if (IAmHost)
      {
        foreach (StateObject s in ClientStates)
        {
           s.sb.Remove(1, s.sb.Length - 1);
        }
      }
      else
      {
        SendMoveBtn.Enabled = true;
      }
    }

    private void playerOneLabel_Click(object sender, EventArgs e)
    {

    }
  }
}

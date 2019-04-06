using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameApp
{
  public partial class TicTacToePlayer : Form
  {
    public bool IpValidated { get; private set; }
    public bool PortValidated { get; private set; }

    public TicTacToePlayer()
    {
      InitializeComponent();
    }
    private void SendMove(int v)
    {
      throw new NotImplementedException();
    }

    private void HostLobby()
    {
      throw new NotImplementedException();
    }

    private void JoinLobby()
    {
      throw new NotImplementedException();
    }

    private bool ValidateIP(string text)
    {
      throw new NotImplementedException();
    }

    private bool ValidatePort(string text)
    {
      throw new NotImplementedException();
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
      HostLobby();
    }

    private void JoinBtn_Click(object sender, EventArgs e)
    {
      JoinLobby();
    }

    private void IPBox_TextChanged(object sender, EventArgs e)
    {
      if (ValidateIP(IPBox.Text))
      {
        IpValidated = true;
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
      }

      if (IpValidated && PortValidated)
      {
        HostBtn.Enabled = true;
        JoinBtn.Enabled = true;
      }
    }
  }
}

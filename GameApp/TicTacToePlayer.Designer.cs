﻿namespace GameApp
{
  partial class TicTacToePlayer
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.IPBox = new System.Windows.Forms.TextBox();
      this.PortBox = new System.Windows.Forms.TextBox();
      this.HostBtn = new System.Windows.Forms.Button();
      this.JoinBtn = new System.Windows.Forms.Button();
      this.PortLbl = new System.Windows.Forms.Label();
      this.IPLbl = new System.Windows.Forms.Label();
      this.StatusLbl = new System.Windows.Forms.Label();
      this.RockBtn = new System.Windows.Forms.Button();
      this.PaperBtn = new System.Windows.Forms.Button();
      this.ScissorsBtn = new System.Windows.Forms.Button();
      this.OutcomeLbl = new System.Windows.Forms.Label();
      this.PlyOneLbl = new System.Windows.Forms.Label();
      this.PlyTwoLbl = new System.Windows.Forms.Label();
      this.PlyThreeLbl = new System.Windows.Forms.Label();
      this.PlyOneScoreLbl = new System.Windows.Forms.Label();
      this.PlyTwoScoreLbl = new System.Windows.Forms.Label();
      this.PlyThreeScoreLbl = new System.Windows.Forms.Label();
      this.PlayerNameBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.LogBox = new System.Windows.Forms.RichTextBox();
      this.SendMoveBtn = new System.Windows.Forms.Button();
      this.playerOneLabel = new System.Windows.Forms.Label();
      this.playerTwoLabel = new System.Windows.Forms.Label();
      this.playerThreeLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // IPBox
      // 
      this.IPBox.Location = new System.Drawing.Point(42, 14);
      this.IPBox.Name = "IPBox";
      this.IPBox.Size = new System.Drawing.Size(121, 20);
      this.IPBox.TabIndex = 0;
      this.IPBox.Text = "127.0.0.1";
      this.IPBox.TextChanged += new System.EventHandler(this.IPBox_TextChanged);
      // 
      // PortBox
      // 
      this.PortBox.Location = new System.Drawing.Point(213, 13);
      this.PortBox.Name = "PortBox";
      this.PortBox.Size = new System.Drawing.Size(100, 20);
      this.PortBox.TabIndex = 1;
      this.PortBox.Text = "3333";
      this.PortBox.TextChanged += new System.EventHandler(this.PortBox_TextChanged);
      // 
      // HostBtn
      // 
      this.HostBtn.Enabled = false;
      this.HostBtn.Location = new System.Drawing.Point(525, 11);
      this.HostBtn.Name = "HostBtn";
      this.HostBtn.Size = new System.Drawing.Size(75, 23);
      this.HostBtn.TabIndex = 2;
      this.HostBtn.Text = "Host";
      this.HostBtn.UseVisualStyleBackColor = true;
      this.HostBtn.Click += new System.EventHandler(this.HostBtn_Click);
      // 
      // JoinBtn
      // 
      this.JoinBtn.Enabled = false;
      this.JoinBtn.Location = new System.Drawing.Point(606, 12);
      this.JoinBtn.Name = "JoinBtn";
      this.JoinBtn.Size = new System.Drawing.Size(75, 23);
      this.JoinBtn.TabIndex = 3;
      this.JoinBtn.Text = "Join";
      this.JoinBtn.UseVisualStyleBackColor = true;
      this.JoinBtn.Click += new System.EventHandler(this.JoinBtn_Click);
      // 
      // PortLbl
      // 
      this.PortLbl.AutoSize = true;
      this.PortLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PortLbl.Location = new System.Drawing.Point(169, 14);
      this.PortLbl.Name = "PortLbl";
      this.PortLbl.Size = new System.Drawing.Size(38, 17);
      this.PortLbl.TabIndex = 4;
      this.PortLbl.Text = "Port:";
      // 
      // IPLbl
      // 
      this.IPLbl.AutoSize = true;
      this.IPLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.IPLbl.Location = new System.Drawing.Point(12, 14);
      this.IPLbl.Name = "IPLbl";
      this.IPLbl.Size = new System.Drawing.Size(24, 17);
      this.IPLbl.TabIndex = 5;
      this.IPLbl.Text = "IP:";
      // 
      // StatusLbl
      // 
      this.StatusLbl.AutoSize = true;
      this.StatusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StatusLbl.ForeColor = System.Drawing.Color.Red;
      this.StatusLbl.Location = new System.Drawing.Point(41, 61);
      this.StatusLbl.Name = "StatusLbl";
      this.StatusLbl.Size = new System.Drawing.Size(92, 17);
      this.StatusLbl.TabIndex = 6;
      this.StatusLbl.Text = "Unconnected";
      // 
      // RockBtn
      // 
      this.RockBtn.Location = new System.Drawing.Point(43, 94);
      this.RockBtn.Name = "RockBtn";
      this.RockBtn.Size = new System.Drawing.Size(120, 67);
      this.RockBtn.TabIndex = 7;
      this.RockBtn.Text = "ROCK";
      this.RockBtn.UseVisualStyleBackColor = true;
      this.RockBtn.Click += new System.EventHandler(this.RockBtn_Click);
      // 
      // PaperBtn
      // 
      this.PaperBtn.Location = new System.Drawing.Point(169, 94);
      this.PaperBtn.Name = "PaperBtn";
      this.PaperBtn.Size = new System.Drawing.Size(120, 67);
      this.PaperBtn.TabIndex = 8;
      this.PaperBtn.Text = "PAPER";
      this.PaperBtn.UseVisualStyleBackColor = true;
      this.PaperBtn.Click += new System.EventHandler(this.PaperBtn_Click);
      // 
      // ScissorsBtn
      // 
      this.ScissorsBtn.Location = new System.Drawing.Point(295, 94);
      this.ScissorsBtn.Name = "ScissorsBtn";
      this.ScissorsBtn.Size = new System.Drawing.Size(124, 67);
      this.ScissorsBtn.TabIndex = 9;
      this.ScissorsBtn.Text = "SCISSORS";
      this.ScissorsBtn.UseVisualStyleBackColor = true;
      this.ScissorsBtn.Click += new System.EventHandler(this.ScissorsBtn_Click);
      // 
      // OutcomeLbl
      // 
      this.OutcomeLbl.AutoSize = true;
      this.OutcomeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.OutcomeLbl.Location = new System.Drawing.Point(473, 48);
      this.OutcomeLbl.Name = "OutcomeLbl";
      this.OutcomeLbl.Size = new System.Drawing.Size(172, 31);
      this.OutcomeLbl.TabIndex = 10;
      this.OutcomeLbl.Text = "Game Status";
      // 
      // PlyOneLbl
      // 
      this.PlyOneLbl.AutoSize = true;
      this.PlyOneLbl.Location = new System.Drawing.Point(476, 94);
      this.PlyOneLbl.Name = "PlyOneLbl";
      this.PlyOneLbl.Size = new System.Drawing.Size(45, 13);
      this.PlyOneLbl.TabIndex = 11;
      this.PlyOneLbl.Text = "Player1:";
      // 
      // PlyTwoLbl
      // 
      this.PlyTwoLbl.AutoSize = true;
      this.PlyTwoLbl.Location = new System.Drawing.Point(476, 121);
      this.PlyTwoLbl.Name = "PlyTwoLbl";
      this.PlyTwoLbl.Size = new System.Drawing.Size(45, 13);
      this.PlyTwoLbl.TabIndex = 12;
      this.PlyTwoLbl.Text = "Player2:";
      // 
      // PlyThreeLbl
      // 
      this.PlyThreeLbl.AutoSize = true;
      this.PlyThreeLbl.Location = new System.Drawing.Point(476, 148);
      this.PlyThreeLbl.Name = "PlyThreeLbl";
      this.PlyThreeLbl.Size = new System.Drawing.Size(45, 13);
      this.PlyThreeLbl.TabIndex = 13;
      this.PlyThreeLbl.Text = "Player3:";
      // 
      // PlyOneScoreLbl
      // 
      this.PlyOneScoreLbl.AutoSize = true;
      this.PlyOneScoreLbl.Location = new System.Drawing.Point(664, 94);
      this.PlyOneScoreLbl.Name = "PlyOneScoreLbl";
      this.PlyOneScoreLbl.Size = new System.Drawing.Size(16, 13);
      this.PlyOneScoreLbl.TabIndex = 14;
      this.PlyOneScoreLbl.Text = "...";
      // 
      // PlyTwoScoreLbl
      // 
      this.PlyTwoScoreLbl.AutoSize = true;
      this.PlyTwoScoreLbl.Location = new System.Drawing.Point(665, 121);
      this.PlyTwoScoreLbl.Name = "PlyTwoScoreLbl";
      this.PlyTwoScoreLbl.Size = new System.Drawing.Size(16, 13);
      this.PlyTwoScoreLbl.TabIndex = 15;
      this.PlyTwoScoreLbl.Text = "...";
      // 
      // PlyThreeScoreLbl
      // 
      this.PlyThreeScoreLbl.AutoSize = true;
      this.PlyThreeScoreLbl.Location = new System.Drawing.Point(664, 148);
      this.PlyThreeScoreLbl.Name = "PlyThreeScoreLbl";
      this.PlyThreeScoreLbl.Size = new System.Drawing.Size(16, 13);
      this.PlyThreeScoreLbl.TabIndex = 16;
      this.PlyThreeScoreLbl.Text = "...";
      // 
      // PlayerNameBox
      // 
      this.PlayerNameBox.Location = new System.Drawing.Point(396, 13);
      this.PlayerNameBox.Name = "PlayerNameBox";
      this.PlayerNameBox.Size = new System.Drawing.Size(100, 20);
      this.PlayerNameBox.TabIndex = 17;
      this.PlayerNameBox.TextChanged += new System.EventHandler(this.PlayerNameBox_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(319, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(71, 17);
      this.label1.TabIndex = 18;
      this.label1.Text = "My Name:";
      // 
      // LogBox
      // 
      this.LogBox.Location = new System.Drawing.Point(44, 177);
      this.LogBox.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
      this.LogBox.Name = "LogBox";
      this.LogBox.ReadOnly = true;
      this.LogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
      this.LogBox.Size = new System.Drawing.Size(636, 78);
      this.LogBox.TabIndex = 19;
      this.LogBox.Text = "";
      // 
      // SendMoveBtn
      // 
      this.SendMoveBtn.Location = new System.Drawing.Point(344, 58);
      this.SendMoveBtn.Name = "SendMoveBtn";
      this.SendMoveBtn.Size = new System.Drawing.Size(75, 23);
      this.SendMoveBtn.TabIndex = 20;
      this.SendMoveBtn.Text = "Send Move";
      this.SendMoveBtn.UseVisualStyleBackColor = true;
      this.SendMoveBtn.Click += new System.EventHandler(this.Button1_Click);
      // 
      // playerOneLabel
      // 
      this.playerOneLabel.AutoSize = true;
      this.playerOneLabel.Location = new System.Drawing.Point(522, 94);
      this.playerOneLabel.Name = "playerOneLabel";
      this.playerOneLabel.Size = new System.Drawing.Size(100, 13);
      this.playerOneLabel.TabIndex = 21;
      this.playerOneLabel.Text = "NOT CONNECTED";
      this.playerOneLabel.Click += new System.EventHandler(this.playerOneLabel_Click);
      // 
      // playerTwoLabel
      // 
      this.playerTwoLabel.AutoSize = true;
      this.playerTwoLabel.Location = new System.Drawing.Point(522, 121);
      this.playerTwoLabel.Name = "playerTwoLabel";
      this.playerTwoLabel.Size = new System.Drawing.Size(100, 13);
      this.playerTwoLabel.TabIndex = 22;
      this.playerTwoLabel.Text = "NOT CONNECTED";
      // 
      // playerThreeLabel
      // 
      this.playerThreeLabel.AutoSize = true;
      this.playerThreeLabel.Location = new System.Drawing.Point(522, 148);
      this.playerThreeLabel.Name = "playerThreeLabel";
      this.playerThreeLabel.Size = new System.Drawing.Size(100, 13);
      this.playerThreeLabel.TabIndex = 23;
      this.playerThreeLabel.Text = "NOT CONNECTED";
      // 
      // TicTacToePlayer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(708, 275);
      this.Controls.Add(this.playerThreeLabel);
      this.Controls.Add(this.playerTwoLabel);
      this.Controls.Add(this.playerOneLabel);
      this.Controls.Add(this.SendMoveBtn);
      this.Controls.Add(this.LogBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.PlayerNameBox);
      this.Controls.Add(this.PlyThreeScoreLbl);
      this.Controls.Add(this.PlyTwoScoreLbl);
      this.Controls.Add(this.PlyOneScoreLbl);
      this.Controls.Add(this.PlyThreeLbl);
      this.Controls.Add(this.PlyTwoLbl);
      this.Controls.Add(this.PlyOneLbl);
      this.Controls.Add(this.OutcomeLbl);
      this.Controls.Add(this.ScissorsBtn);
      this.Controls.Add(this.PaperBtn);
      this.Controls.Add(this.RockBtn);
      this.Controls.Add(this.StatusLbl);
      this.Controls.Add(this.IPLbl);
      this.Controls.Add(this.PortLbl);
      this.Controls.Add(this.JoinBtn);
      this.Controls.Add(this.HostBtn);
      this.Controls.Add(this.PortBox);
      this.Controls.Add(this.IPBox);
      this.Name = "TicTacToePlayer";
      this.Text = "TicTacToe";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox IPBox;
    private System.Windows.Forms.TextBox PortBox;
    private System.Windows.Forms.Button HostBtn;
    private System.Windows.Forms.Button JoinBtn;
    private System.Windows.Forms.Label PortLbl;
    private System.Windows.Forms.Label IPLbl;
    private System.Windows.Forms.Label StatusLbl;
    private System.Windows.Forms.Button RockBtn;
    private System.Windows.Forms.Button PaperBtn;
    private System.Windows.Forms.Button ScissorsBtn;
    private System.Windows.Forms.Label OutcomeLbl;
    private System.Windows.Forms.Label PlyOneLbl;
    private System.Windows.Forms.Label PlyTwoLbl;
    private System.Windows.Forms.Label PlyThreeLbl;
    private System.Windows.Forms.Label PlyOneScoreLbl;
    private System.Windows.Forms.Label PlyTwoScoreLbl;
    private System.Windows.Forms.Label PlyThreeScoreLbl;
    private System.Windows.Forms.TextBox PlayerNameBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Button SendMoveBtn;
        private System.Windows.Forms.Label playerOneLabel;
        private System.Windows.Forms.Label playerTwoLabel;
        private System.Windows.Forms.Label playerThreeLabel;
    }
}


namespace NetSpeakServer
{
    partial class ServerScreen
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.messages = new System.Windows.Forms.ListBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.kickButton = new System.Windows.Forms.Button();
            this.users = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.banUser = new System.Windows.Forms.Button();
            this.pmUser = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.ItemHeight = 20;
            this.messages.Location = new System.Drawing.Point(12, 12);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(831, 544);
            this.messages.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(700, 562);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(143, 47);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // kickButton
            // 
            this.kickButton.Location = new System.Drawing.Point(1038, 76);
            this.kickButton.Name = "kickButton";
            this.kickButton.Size = new System.Drawing.Size(164, 47);
            this.kickButton.TabIndex = 2;
            this.kickButton.Text = "Kick user";
            this.kickButton.UseVisualStyleBackColor = true;
            // 
            // users
            // 
            this.users.FormattingEnabled = true;
            this.users.ItemHeight = 20;
            this.users.Location = new System.Drawing.Point(849, 12);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(183, 604);
            this.users.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 572);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(682, 26);
            this.textBox1.TabIndex = 4;
            // 
            // banUser
            // 
            this.banUser.Location = new System.Drawing.Point(1038, 129);
            this.banUser.Name = "banUser";
            this.banUser.Size = new System.Drawing.Size(164, 47);
            this.banUser.TabIndex = 5;
            this.banUser.Text = "Ban user";
            this.banUser.UseVisualStyleBackColor = true;
            // 
            // pmUser
            // 
            this.pmUser.Location = new System.Drawing.Point(1038, 12);
            this.pmUser.Name = "pmUser";
            this.pmUser.Size = new System.Drawing.Size(164, 58);
            this.pmUser.TabIndex = 6;
            this.pmUser.Text = "Send private message";
            this.pmUser.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1038, 182);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 55);
            this.button1.TabIndex = 7;
            this.button1.Text = "Change user permissions";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ServerScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 616);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pmUser);
            this.Controls.Add(this.banUser);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.users);
            this.Controls.Add(this.kickButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messages);
            this.Name = "ServerScreen";
            this.Text = "NetSpeak Server";
            this.Load += new System.EventHandler(this.ServerScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button kickButton;
        private System.Windows.Forms.ListBox users;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button banUser;
        private System.Windows.Forms.Button pmUser;
        private System.Windows.Forms.Button button1;
    }
}


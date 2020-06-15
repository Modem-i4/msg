namespace msg
{
    partial class Chat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.user = new System.Windows.Forms.Label();
            this.edit = new System.Windows.Forms.Button();
            this.leave = new System.Windows.Forms.Button();
            this.unreaded = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // user
            // 
            this.user.BackColor = System.Drawing.Color.PaleTurquoise;
            this.user.Enabled = false;
            this.user.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.user.Location = new System.Drawing.Point(0, 0);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(169, 53);
            this.user.TabIndex = 3;
            this.user.Text = "User";
            this.user.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edit
            // 
            this.edit.BackColor = System.Drawing.Color.White;
            this.edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edit.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.edit.Location = new System.Drawing.Point(129, 2);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(20, 20);
            this.edit.TabIndex = 4;
            this.edit.Text = "e";
            this.edit.UseVisualStyleBackColor = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // leave
            // 
            this.leave.BackColor = System.Drawing.Color.White;
            this.leave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leave.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.leave.Location = new System.Drawing.Point(150, 2);
            this.leave.Name = "leave";
            this.leave.Size = new System.Drawing.Size(20, 20);
            this.leave.TabIndex = 5;
            this.leave.Text = "x";
            this.leave.UseVisualStyleBackColor = false;
            this.leave.Click += new System.EventHandler(this.leave_Click);
            // 
            // unreaded
            // 
            this.unreaded.BackColor = System.Drawing.Color.PaleTurquoise;
            this.unreaded.Enabled = false;
            this.unreaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unreaded.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.unreaded.Location = new System.Drawing.Point(129, 23);
            this.unreaded.Name = "unreaded";
            this.unreaded.Size = new System.Drawing.Size(40, 28);
            this.unreaded.TabIndex = 6;
            this.unreaded.Text = "0";
            this.unreaded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.unreaded);
            this.Controls.Add(this.leave);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.user);
            this.Name = "Chat";
            this.Size = new System.Drawing.Size(171, 53);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button edit;
        private System.Windows.Forms.Button leave;
        public System.Windows.Forms.Label user;
        public System.Windows.Forms.Label unreaded;
    }
}

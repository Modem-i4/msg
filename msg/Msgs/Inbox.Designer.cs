namespace msg
{
    partial class Inbox
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
            this.container = new System.Windows.Forms.GroupBox();
            this.text = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.AutoSize = true;
            this.container.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.container.Controls.Add(this.text);
            this.container.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.container.Location = new System.Drawing.Point(182, 3);
            this.container.MaximumSize = new System.Drawing.Size(286, 0);
            this.container.Name = "container";
            this.container.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.container.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.container.Size = new System.Drawing.Size(286, 53);
            this.container.TabIndex = 0;
            this.container.TabStop = false;
            this.container.Text = "User";
            // 
            // text
            // 
            this.text.AutoSize = true;
            this.text.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text.Location = new System.Drawing.Point(9, 21);
            this.text.MaximumSize = new System.Drawing.Size(275, 0);
            this.text.MinimumSize = new System.Drawing.Size(275, 15);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(275, 16);
            this.text.TabIndex = 0;
            this.text.Text = "Content";
            // 
            // time
            // 
            this.time.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.time.Location = new System.Drawing.Point(109, 7);
            this.time.MinimumSize = new System.Drawing.Size(0, 30);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(59, 55);
            this.time.TabIndex = 1;
            this.time.Text = "00:00:00";
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Inbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.time);
            this.Controls.Add(this.container);
            this.MinimumSize = new System.Drawing.Size(475, 0);
            this.Name = "Inbox";
            this.Size = new System.Drawing.Size(475, 62);
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox container;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.Label time;
    }
}

namespace msg
{
    partial class Outbox
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
            this.edit = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.text = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.AutoSize = true;
            this.container.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.container.Controls.Add(this.edit);
            this.container.Controls.Add(this.delete);
            this.container.Controls.Add(this.text);
            this.container.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.container.Location = new System.Drawing.Point(3, -8);
            this.container.MaximumSize = new System.Drawing.Size(286, 0);
            this.container.Name = "container";
            this.container.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.container.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.container.Size = new System.Drawing.Size(286, 62);
            this.container.TabIndex = 0;
            this.container.TabStop = false;
            // 
            // edit
            // 
            this.edit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.edit.BackColor = System.Drawing.Color.FloralWhite;
            this.edit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.edit.FlatAppearance.BorderSize = 0;
            this.edit.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.edit.Location = new System.Drawing.Point(242, 8);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(23, 23);
            this.edit.TabIndex = 3;
            this.edit.Text = "🖊️";
            this.edit.UseVisualStyleBackColor = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.delete.BackColor = System.Drawing.Color.FloralWhite;
            this.delete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delete.FlatAppearance.BorderSize = 0;
            this.delete.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.delete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.delete.Location = new System.Drawing.Point(263, 8);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(23, 23);
            this.delete.TabIndex = 2;
            this.delete.Text = "🗑️";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // text
            // 
            this.text.AutoSize = true;
            this.text.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text.Location = new System.Drawing.Point(8, 30);
            this.text.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.text.MaximumSize = new System.Drawing.Size(275, 0);
            this.text.MinimumSize = new System.Drawing.Size(275, 15);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(275, 16);
            this.text.TabIndex = 0;
            this.text.Text = "Content";
            // 
            // time
            // 
            this.time.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.time.Location = new System.Drawing.Point(307, 0);
            this.time.MinimumSize = new System.Drawing.Size(0, 30);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(59, 60);
            this.time.TabIndex = 1;
            this.time.Text = "00:00:00";
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Outbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.time);
            this.Controls.Add(this.container);
            this.MinimumSize = new System.Drawing.Size(475, 0);
            this.Name = "Outbox";
            this.Size = new System.Drawing.Size(475, 60);
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox container;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.Label time;
        public System.Windows.Forms.Button delete;
        public System.Windows.Forms.Button edit;
    }
}

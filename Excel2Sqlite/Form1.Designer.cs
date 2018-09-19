namespace Excel2Sqlite
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.excel2SqliteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // excel2SqliteButton
            // 
            this.excel2SqliteButton.Location = new System.Drawing.Point(280, 176);
            this.excel2SqliteButton.Name = "excel2SqliteButton";
            this.excel2SqliteButton.Size = new System.Drawing.Size(75, 23);
            this.excel2SqliteButton.TabIndex = 0;
            this.excel2SqliteButton.Text = "Convert Excel to Sqlite";
            this.excel2SqliteButton.UseVisualStyleBackColor = true;
            this.excel2SqliteButton.Click += new System.EventHandler(this.excel2SqliteButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.excel2SqliteButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button excel2SqliteButton;
    }
}


namespace Ksu.Cis300.TextAnalyzer
{
    partial class UserInterface
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
            this.uxSelectText1 = new System.Windows.Forms.Button();
            this.uxSelectText2 = new System.Windows.Forms.Button();
            this.uxAnalyze = new System.Windows.Forms.Button();
            this.uxText1 = new System.Windows.Forms.TextBox();
            this.uxText2 = new System.Windows.Forms.TextBox();
            this.uxNumberOfWords = new System.Windows.Forms.NumericUpDown();
            this.uxNumberLabel = new System.Windows.Forms.Label();
            this.uxOpenDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.uxNumberOfWords)).BeginInit();
            this.SuspendLayout();
            // 
            // uxSelectText1
            // 
            this.uxSelectText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxSelectText1.Location = new System.Drawing.Point(12, 12);
            this.uxSelectText1.Name = "uxSelectText1";
            this.uxSelectText1.Size = new System.Drawing.Size(90, 35);
            this.uxSelectText1.TabIndex = 0;
            this.uxSelectText1.Text = "Text 1:";
            this.uxSelectText1.UseVisualStyleBackColor = true;
            this.uxSelectText1.Click += new System.EventHandler(this.uxSelectText1_Click);
            // 
            // uxSelectText2
            // 
            this.uxSelectText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxSelectText2.Location = new System.Drawing.Point(12, 53);
            this.uxSelectText2.Name = "uxSelectText2";
            this.uxSelectText2.Size = new System.Drawing.Size(90, 35);
            this.uxSelectText2.TabIndex = 1;
            this.uxSelectText2.Text = "Text 2:";
            this.uxSelectText2.UseVisualStyleBackColor = true;
            this.uxSelectText2.Click += new System.EventHandler(this.uxSelectText2_Click);
            // 
            // uxAnalyze
            // 
            this.uxAnalyze.Enabled = false;
            this.uxAnalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxAnalyze.Location = new System.Drawing.Point(300, 98);
            this.uxAnalyze.Name = "uxAnalyze";
            this.uxAnalyze.Size = new System.Drawing.Size(272, 35);
            this.uxAnalyze.TabIndex = 2;
            this.uxAnalyze.Text = "Analyze Texts";
            this.uxAnalyze.UseVisualStyleBackColor = true;
            this.uxAnalyze.Click += new System.EventHandler(this.uxAnalyze_Click);
            // 
            // uxText1
            // 
            this.uxText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxText1.Location = new System.Drawing.Point(108, 14);
            this.uxText1.Name = "uxText1";
            this.uxText1.ReadOnly = true;
            this.uxText1.Size = new System.Drawing.Size(464, 29);
            this.uxText1.TabIndex = 3;
            // 
            // uxText2
            // 
            this.uxText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxText2.Location = new System.Drawing.Point(108, 55);
            this.uxText2.Name = "uxText2";
            this.uxText2.ReadOnly = true;
            this.uxText2.Size = new System.Drawing.Size(464, 29);
            this.uxText2.TabIndex = 4;
            // 
            // uxNumberOfWords
            // 
            this.uxNumberOfWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxNumberOfWords.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.uxNumberOfWords.Location = new System.Drawing.Point(241, 101);
            this.uxNumberOfWords.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.uxNumberOfWords.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxNumberOfWords.Name = "uxNumberOfWords";
            this.uxNumberOfWords.Size = new System.Drawing.Size(53, 29);
            this.uxNumberOfWords.TabIndex = 5;
            this.uxNumberOfWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uxNumberOfWords.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // uxNumberLabel
            // 
            this.uxNumberLabel.AutoSize = true;
            this.uxNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uxNumberLabel.Location = new System.Drawing.Point(8, 103);
            this.uxNumberLabel.Name = "uxNumberLabel";
            this.uxNumberLabel.Size = new System.Drawing.Size(227, 24);
            this.uxNumberLabel.TabIndex = 6;
            this.uxNumberLabel.Text = "Number of Words to Use: ";
            // 
            // uxOpenDialog
            // 
            this.uxOpenDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 149);
            this.Controls.Add(this.uxNumberLabel);
            this.Controls.Add(this.uxNumberOfWords);
            this.Controls.Add(this.uxText2);
            this.Controls.Add(this.uxText1);
            this.Controls.Add(this.uxAnalyze);
            this.Controls.Add(this.uxSelectText2);
            this.Controls.Add(this.uxSelectText1);
            this.Name = "UserInterface";
            this.Text = "Text Analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.uxNumberOfWords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxSelectText1;
        private System.Windows.Forms.Button uxSelectText2;
        private System.Windows.Forms.Button uxAnalyze;
        private System.Windows.Forms.TextBox uxText1;
        private System.Windows.Forms.TextBox uxText2;
        private System.Windows.Forms.NumericUpDown uxNumberOfWords;
        private System.Windows.Forms.Label uxNumberLabel;
        private System.Windows.Forms.OpenFileDialog uxOpenDialog;
    }
}


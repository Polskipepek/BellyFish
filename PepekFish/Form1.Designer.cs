
namespace BellyFish {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.PiecesOnBoard = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PiecesOnBoard
            // 
            this.PiecesOnBoard.AutoSize = true;
            this.PiecesOnBoard.Location = new System.Drawing.Point(12, 30);
            this.PiecesOnBoard.Name = "PiecesOnBoard";
            this.PiecesOnBoard.Size = new System.Drawing.Size(41, 15);
            this.PiecesOnBoard.TabIndex = 0;
            this.PiecesOnBoard.Text = "Pawns";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PiecesOnBoard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label PiecesOnBoard;
    }
}


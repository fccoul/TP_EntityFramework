namespace TaskAsynchronous
{
    partial class Form1
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
            this.btnCall = new System.Windows.Forms.Button();
            this.txtLable = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCallAsync2 = new System.Windows.Forms.Button();
            this.txtAsync2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(50, 69);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(110, 28);
            this.btnCall.TabIndex = 0;
            this.btnCall.Text = "call Asynchron";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCallAsynchron_Click);
            // 
            // txtLable
            // 
            this.txtLable.AutoSize = true;
            this.txtLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLable.Location = new System.Drawing.Point(46, 109);
            this.txtLable.Name = "txtLable";
            this.txtLable.Size = new System.Drawing.Size(60, 24);
            this.txtLable.TabIndex = 1;
            this.txtLable.Text = "label1";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(60, 28);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(100, 20);
            this.txtInput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message";
            // 
            // btnCallAsync2
            // 
            this.btnCallAsync2.Location = new System.Drawing.Point(336, 28);
            this.btnCallAsync2.Name = "btnCallAsync2";
            this.btnCallAsync2.Size = new System.Drawing.Size(75, 23);
            this.btnCallAsync2.TabIndex = 4;
            this.btnCallAsync2.Text = "Call Async 2";
            this.btnCallAsync2.UseVisualStyleBackColor = true;
            this.btnCallAsync2.Click += new System.EventHandler(this.btnCallAsync2_Click);
            // 
            // txtAsync2
            // 
            this.txtAsync2.Location = new System.Drawing.Point(336, 76);
            this.txtAsync2.Multiline = true;
            this.txtAsync2.Name = "txtAsync2";
            this.txtAsync2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAsync2.Size = new System.Drawing.Size(272, 140);
            this.txtAsync2.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 320);
            this.Controls.Add(this.txtAsync2);
            this.Controls.Add(this.btnCallAsync2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.txtLable);
            this.Controls.Add(this.btnCall);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Label txtLable;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCallAsync2;
        private System.Windows.Forms.TextBox txtAsync2;
    }
}


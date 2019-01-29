namespace NewYorkShippingLTDForm
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
            this.header = new System.Windows.Forms.Label();
            this.inventoryTxtBx = new System.Windows.Forms.TextBox();
            this.importsTxtBx = new System.Windows.Forms.TextBox();
            this.exportsTxtBx = new System.Windows.Forms.TextBox();
            this.inventoryTxtBxLbl = new System.Windows.Forms.Label();
            this.importsTxtBxLbl = new System.Windows.Forms.Label();
            this.exportsTxtBxLbl = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.ForeColor = System.Drawing.Color.Maroon;
            this.header.Location = new System.Drawing.Point(224, 47);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(228, 29);
            this.header.TabIndex = 0;
            this.header.Text = "New York Shipping, LTD";
            // 
            // inventoryTxtBx
            // 
            this.inventoryTxtBx.Location = new System.Drawing.Point(44, 135);
            this.inventoryTxtBx.Name = "inventoryTxtBx";
            this.inventoryTxtBx.Size = new System.Drawing.Size(167, 20);
            this.inventoryTxtBx.TabIndex = 1;
            // 
            // importsTxtBx
            // 
            this.importsTxtBx.Location = new System.Drawing.Point(250, 135);
            this.importsTxtBx.Name = "importsTxtBx";
            this.importsTxtBx.Size = new System.Drawing.Size(177, 20);
            this.importsTxtBx.TabIndex = 2;
            // 
            // exportsTxtBx
            // 
            this.exportsTxtBx.Location = new System.Drawing.Point(468, 135);
            this.exportsTxtBx.Name = "exportsTxtBx";
            this.exportsTxtBx.Size = new System.Drawing.Size(169, 20);
            this.exportsTxtBx.TabIndex = 3;
            this.exportsTxtBx.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // inventoryTxtBxLbl
            // 
            this.inventoryTxtBxLbl.AutoSize = true;
            this.inventoryTxtBxLbl.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inventoryTxtBxLbl.Location = new System.Drawing.Point(80, 158);
            this.inventoryTxtBxLbl.Name = "inventoryTxtBxLbl";
            this.inventoryTxtBxLbl.Size = new System.Drawing.Size(98, 17);
            this.inventoryTxtBxLbl.TabIndex = 4;
            this.inventoryTxtBxLbl.Text = "Inventory File";
            // 
            // importsTxtBxLbl
            // 
            this.importsTxtBxLbl.AutoSize = true;
            this.importsTxtBxLbl.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importsTxtBxLbl.Location = new System.Drawing.Point(256, 158);
            this.importsTxtBxLbl.Name = "importsTxtBxLbl";
            this.importsTxtBxLbl.Size = new System.Drawing.Size(162, 17);
            this.importsTxtBxLbl.TabIndex = 5;
            this.importsTxtBxLbl.Text = "Inbound Shipments File";
            // 
            // exportsTxtBxLbl
            // 
            this.exportsTxtBxLbl.AutoSize = true;
            this.exportsTxtBxLbl.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportsTxtBxLbl.Location = new System.Drawing.Point(465, 158);
            this.exportsTxtBxLbl.Name = "exportsTxtBxLbl";
            this.exportsTxtBxLbl.Size = new System.Drawing.Size(172, 17);
            this.exportsTxtBxLbl.TabIndex = 6;
            this.exportsTxtBxLbl.Text = "Outbound Shipments File";
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(183, 286);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(135, 42);
            this.submitBtn.TabIndex = 7;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(363, 286);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(147, 42);
            this.exitBtn.TabIndex = 8;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 386);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.exportsTxtBxLbl);
            this.Controls.Add(this.importsTxtBxLbl);
            this.Controls.Add(this.inventoryTxtBxLbl);
            this.Controls.Add(this.exportsTxtBx);
            this.Controls.Add(this.importsTxtBx);
            this.Controls.Add(this.inventoryTxtBx);
            this.Controls.Add(this.header);
            this.Name = "Form1";
            this.Text = "New York Shipping";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header;
        private System.Windows.Forms.TextBox inventoryTxtBx;
        private System.Windows.Forms.TextBox importsTxtBx;
        private System.Windows.Forms.TextBox exportsTxtBx;
        private System.Windows.Forms.Label inventoryTxtBxLbl;
        private System.Windows.Forms.Label importsTxtBxLbl;
        private System.Windows.Forms.Label exportsTxtBxLbl;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}


namespace SXE
{
  partial class SXEMain
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
      this.sxe1 = new Quantifai.SXE();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // sxe1
      // 
      this.sxe1.Location = new System.Drawing.Point(12, 36);
      this.sxe1.Name = "sxe1";
      this.sxe1.Size = new System.Drawing.Size(711, 526);
      this.sxe1.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 7);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // SXEMain
      // 
      this.ClientSize = new System.Drawing.Size(874, 585);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.sxe1);
      this.Name = "SXEMain";
      this.ResumeLayout(false);

    }


    #endregion

    private Quantifai.SXE sxe1;
    private System.Windows.Forms.Button button1;
  }
}


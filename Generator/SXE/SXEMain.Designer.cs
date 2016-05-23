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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SXEMain));
      this.sxe1 = new Quantifai.SXE();
      this.SuspendLayout();
      // 
      // sxe1
      // 
      this.sxe1.AutoSize = true;
      this.sxe1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.sxe1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sxe1.BackgroundImage")));
      this.sxe1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sxe1.Location = new System.Drawing.Point(0, 0);
      this.sxe1.Name = "sxe1";
      this.sxe1.Size = new System.Drawing.Size(874, 585);
      this.sxe1.TabIndex = 0;
      this.sxe1.Load += new System.EventHandler(this.sxe1_Load);
      // 
      // SXEMain
      // 
      this.BackgroundImage = global::SXE.Properties.Resources.bg2;
      this.ClientSize = new System.Drawing.Size(874, 585);
      this.Controls.Add(this.sxe1);
      this.Name = "SXEMain";
      this.ResumeLayout(false);
      this.PerformLayout();

    }


    #endregion

    private Quantifai.SXE sxe1;
  }
}


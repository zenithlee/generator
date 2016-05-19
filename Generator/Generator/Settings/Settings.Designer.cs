namespace Generator.Options
{
  partial class Settings
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
      this.chk_AlwaysOnTop = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // AlwaysOnTop
      // 
      this.chk_AlwaysOnTop.AutoSize = true;
      this.chk_AlwaysOnTop.Location = new System.Drawing.Point(21, 18);
      this.chk_AlwaysOnTop.Name = "AlwaysOnTop";
      this.chk_AlwaysOnTop.Size = new System.Drawing.Size(98, 17);
      this.chk_AlwaysOnTop.TabIndex = 0;
      this.chk_AlwaysOnTop.Text = "Always On Top";
      this.chk_AlwaysOnTop.UseVisualStyleBackColor = true;
      this.chk_AlwaysOnTop.CheckedChanged += new System.EventHandler(this.AlwaysOnTop_CheckedChanged);
      // 
      // Settings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.chk_AlwaysOnTop);
      this.Name = "Settings";
      this.Size = new System.Drawing.Size(445, 371);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox chk_AlwaysOnTop;
  }
}

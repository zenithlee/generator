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
      this.AlwaysOnTop = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // AlwaysOnTop
      // 
      this.AlwaysOnTop.AutoSize = true;
      this.AlwaysOnTop.Location = new System.Drawing.Point(21, 18);
      this.AlwaysOnTop.Name = "AlwaysOnTop";
      this.AlwaysOnTop.Size = new System.Drawing.Size(98, 17);
      this.AlwaysOnTop.TabIndex = 0;
      this.AlwaysOnTop.Text = "Always On Top";
      this.AlwaysOnTop.UseVisualStyleBackColor = true;
      this.AlwaysOnTop.CheckedChanged += new System.EventHandler(this.AlwaysOnTop_CheckedChanged);
      // 
      // Settings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.AlwaysOnTop);
      this.Name = "Settings";
      this.Size = new System.Drawing.Size(445, 371);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox AlwaysOnTop;
  }
}

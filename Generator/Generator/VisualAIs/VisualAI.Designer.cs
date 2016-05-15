namespace Generator.VisualAIs
{
  partial class VisualAI
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
      this.VisualAIPicture = new System.Windows.Forms.PictureBox();
      this.VisualResultsList = new System.Windows.Forms.ListBox();
      this.VisualPath = new System.Windows.Forms.TextBox();
      this.VisualSimulateInteraction = new System.Windows.Forms.Button();
      this.VisualTest = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.VisualAIPicture)).BeginInit();
      this.SuspendLayout();
      // 
      // VisualAIPicture
      // 
      this.VisualAIPicture.ImageLocation = "https://images.stackcommerce.com/assets/productshot1-image/12547/c6ce3fcd945583e7" +
    "af8cf15d3e36e85249e4e426_main_hero_image.png";
      this.VisualAIPicture.Location = new System.Drawing.Point(13, 55);
      this.VisualAIPicture.Name = "VisualAIPicture";
      this.VisualAIPicture.Size = new System.Drawing.Size(457, 381);
      this.VisualAIPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.VisualAIPicture.TabIndex = 9;
      this.VisualAIPicture.TabStop = false;
      // 
      // VisualResultsList
      // 
      this.VisualResultsList.FormattingEnabled = true;
      this.VisualResultsList.Location = new System.Drawing.Point(476, 55);
      this.VisualResultsList.Name = "VisualResultsList";
      this.VisualResultsList.Size = new System.Drawing.Size(216, 381);
      this.VisualResultsList.TabIndex = 8;
      // 
      // VisualPath
      // 
      this.VisualPath.Location = new System.Drawing.Point(13, 29);
      this.VisualPath.Name = "VisualPath";
      this.VisualPath.Size = new System.Drawing.Size(679, 20);
      this.VisualPath.TabIndex = 7;
      this.VisualPath.Text = "https://images.stackcommerce.com/assets/productshot1-image/12547/c6ce3fcd945583e7" +
    "af8cf15d3e36e85249e4e426_main_hero_image.png";
      this.VisualPath.TextChanged += new System.EventHandler(this.VisualPath_TextChanged);
      // 
      // VisualSimulateInteraction
      // 
      this.VisualSimulateInteraction.Location = new System.Drawing.Point(698, 29);
      this.VisualSimulateInteraction.Name = "VisualSimulateInteraction";
      this.VisualSimulateInteraction.Size = new System.Drawing.Size(97, 23);
      this.VisualSimulateInteraction.TabIndex = 6;
      this.VisualSimulateInteraction.Text = "Analyse";
      this.VisualSimulateInteraction.UseVisualStyleBackColor = true;
      this.VisualSimulateInteraction.Click += new System.EventHandler(this.VisualSimulateInteraction_Click);
      // 
      // VisualTest
      // 
      this.VisualTest.Location = new System.Drawing.Point(698, 55);
      this.VisualTest.Name = "VisualTest";
      this.VisualTest.Size = new System.Drawing.Size(97, 23);
      this.VisualTest.TabIndex = 5;
      this.VisualTest.Text = "Analyse NSFW";
      this.VisualTest.UseVisualStyleBackColor = true;
      this.VisualTest.Click += new System.EventHandler(this.VisualTest_Click);
      // 
      // VisualAI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.VisualAIPicture);
      this.Controls.Add(this.VisualResultsList);
      this.Controls.Add(this.VisualPath);
      this.Controls.Add(this.VisualSimulateInteraction);
      this.Controls.Add(this.VisualTest);
      this.Name = "VisualAI";
      this.Size = new System.Drawing.Size(798, 552);
      ((System.ComponentModel.ISupportInitialize)(this.VisualAIPicture)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox VisualAIPicture;
    private System.Windows.Forms.ListBox VisualResultsList;
    private System.Windows.Forms.TextBox VisualPath;
    private System.Windows.Forms.Button VisualSimulateInteraction;
    private System.Windows.Forms.Button VisualTest;
  }
}

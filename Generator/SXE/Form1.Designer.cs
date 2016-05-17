namespace SXE
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
      System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Name", System.Windows.Forms.HorizontalAlignment.Left);
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "#BMW",
            "BMW",
            "5012",
            "22"}, -1);
      System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Thingy");
      this.listView1 = new System.Windows.Forms.ListView();
      this.SuspendLayout();
      // 
      // listView1
      // 
      listViewGroup1.Header = "Name";
      listViewGroup1.Name = "listViewGroup1";
      this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
      listViewItem1.Group = listViewGroup1;
      this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
      this.listView1.Location = new System.Drawing.Point(12, 57);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(224, 591);
      this.listView1.TabIndex = 0;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.List;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(868, 660);
      this.Controls.Add(this.listView1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView listView1;
  }
}


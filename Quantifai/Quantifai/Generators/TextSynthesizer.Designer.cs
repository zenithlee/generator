namespace Quantifai.Generators
{
  partial class TextSynthesizer
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
      this.txtInput = new System.Windows.Forms.TextBox();
      this.txtOutput = new System.Windows.Forms.TextBox();
      this.btnProcess = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.btnClear = new System.Windows.Forms.Button();
      this.btnCopyToModel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtInput
      // 
      this.txtInput.Location = new System.Drawing.Point(14, 42);
      this.txtInput.MaxLength = 632767;
      this.txtInput.Multiline = true;
      this.txtInput.Name = "txtInput";
      this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtInput.Size = new System.Drawing.Size(346, 351);
      this.txtInput.TabIndex = 0;
      // 
      // txtOutput
      // 
      this.txtOutput.Location = new System.Drawing.Point(366, 14);
      this.txtOutput.Multiline = true;
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtOutput.Size = new System.Drawing.Size(364, 379);
      this.txtOutput.TabIndex = 1;
      // 
      // btnProcess
      // 
      this.btnProcess.Location = new System.Drawing.Point(667, 399);
      this.btnProcess.Name = "btnProcess";
      this.btnProcess.Size = new System.Drawing.Size(75, 23);
      this.btnProcess.TabIndex = 2;
      this.btnProcess.Text = "Process";
      this.btnProcess.UseVisualStyleBackColor = true;
      this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Location = new System.Drawing.Point(14, 13);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 3;
      this.btnLoad.Text = "Load+";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // btnClear
      // 
      this.btnClear.Location = new System.Drawing.Point(285, 14);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(75, 23);
      this.btnClear.TabIndex = 4;
      this.btnClear.Text = "Clear Model";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // btnCopyToModel
      // 
      this.btnCopyToModel.Location = new System.Drawing.Point(95, 14);
      this.btnCopyToModel.Name = "btnCopyToModel";
      this.btnCopyToModel.Size = new System.Drawing.Size(98, 23);
      this.btnCopyToModel.TabIndex = 5;
      this.btnCopyToModel.Text = "CopyToModel";
      this.btnCopyToModel.UseVisualStyleBackColor = true;
      this.btnCopyToModel.Click += new System.EventHandler(this.btnCopyToModel_Click);
      // 
      // TextSynthesizer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnCopyToModel);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.btnProcess);
      this.Controls.Add(this.txtOutput);
      this.Controls.Add(this.txtInput);
      this.Name = "TextSynthesizer";
      this.Size = new System.Drawing.Size(745, 434);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtInput;
    private System.Windows.Forms.TextBox txtOutput;
    private System.Windows.Forms.Button btnProcess;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnCopyToModel;
  }
}

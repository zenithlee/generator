namespace Quantifai.Analyse
{
  partial class Analyser
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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 0.2D);
      System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
      this.btnAnalyseOpen = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.SentimentView = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.txtMean = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtAverage = new System.Windows.Forms.TextBox();
      this.listPopularity = new System.Windows.Forms.ListView();
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnLoadText = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      this.SuspendLayout();
      // 
      // btnAnalyseOpen
      // 
      this.btnAnalyseOpen.Location = new System.Drawing.Point(3, 3);
      this.btnAnalyseOpen.Name = "btnAnalyseOpen";
      this.btnAnalyseOpen.Size = new System.Drawing.Size(75, 23);
      this.btnAnalyseOpen.TabIndex = 1;
      this.btnAnalyseOpen.Text = "Load CSV...";
      this.btnAnalyseOpen.UseVisualStyleBackColor = true;
      this.btnAnalyseOpen.Click += new System.EventHandler(this.btnAnalyseOpen_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // SentimentView
      // 
      this.SentimentView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.SentimentView.Location = new System.Drawing.Point(4, 33);
      this.SentimentView.Name = "SentimentView";
      this.SentimentView.Size = new System.Drawing.Size(384, 479);
      this.SentimentView.TabIndex = 2;
      this.SentimentView.UseCompatibleStateImageBehavior = false;
      this.SentimentView.View = System.Windows.Forms.View.Details;
      this.SentimentView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SentimentView_ColumnClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Content";
      this.columnHeader1.Width = 260;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Sentiment";
      // 
      // chart1
      // 
      chartArea3.Area3DStyle.Enable3D = true;
      chartArea3.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
      chartArea3.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea3);
      legend3.Name = "Legend1";
      this.chart1.Legends.Add(legend3);
      this.chart1.Location = new System.Drawing.Point(394, 66);
      this.chart1.Name = "chart1";
      series3.ChartArea = "ChartArea1";
      series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
      series3.Legend = "Legend1";
      series3.Name = "Series1";
      dataPoint5.AxisLabel = "AxistLabel";
      dataPoint5.Label = "MyLabel";
      dataPoint5.LegendText = "LegendText";
      series3.Points.Add(dataPoint5);
      series3.Points.Add(dataPoint6);
      this.chart1.Series.Add(series3);
      this.chart1.Size = new System.Drawing.Size(464, 178);
      this.chart1.TabIndex = 3;
      this.chart1.Text = "chart1";
      // 
      // txtMean
      // 
      this.txtMean.Location = new System.Drawing.Point(435, 33);
      this.txtMean.Name = "txtMean";
      this.txtMean.Size = new System.Drawing.Size(78, 20);
      this.txtMean.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(395, 36);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Mean";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(529, 36);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Average";
      // 
      // txtAverage
      // 
      this.txtAverage.Location = new System.Drawing.Point(582, 33);
      this.txtAverage.Name = "txtAverage";
      this.txtAverage.Size = new System.Drawing.Size(80, 20);
      this.txtAverage.TabIndex = 6;
      // 
      // listPopularity
      // 
      this.listPopularity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
      this.listPopularity.Location = new System.Drawing.Point(395, 251);
      this.listPopularity.Name = "listPopularity";
      this.listPopularity.Size = new System.Drawing.Size(463, 261);
      this.listPopularity.TabIndex = 8;
      this.listPopularity.UseCompatibleStateImageBehavior = false;
      this.listPopularity.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Concept";
      this.columnHeader3.Width = 260;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Frequency";
      // 
      // btnLoadText
      // 
      this.btnLoadText.Location = new System.Drawing.Point(84, 3);
      this.btnLoadText.Name = "btnLoadText";
      this.btnLoadText.Size = new System.Drawing.Size(75, 23);
      this.btnLoadText.TabIndex = 9;
      this.btnLoadText.Text = "Load TXT...";
      this.btnLoadText.UseVisualStyleBackColor = true;
      this.btnLoadText.Click += new System.EventHandler(this.btnLoadText_Click);
      // 
      // Analyser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnLoadText);
      this.Controls.Add(this.listPopularity);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtAverage);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtMean);
      this.Controls.Add(this.chart1);
      this.Controls.Add(this.SentimentView);
      this.Controls.Add(this.btnAnalyseOpen);
      this.Name = "Analyser";
      this.Size = new System.Drawing.Size(861, 527);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnAnalyseOpen;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.ListView SentimentView;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    private System.Windows.Forms.TextBox txtMean;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtAverage;
    private System.Windows.Forms.ListView listPopularity;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.Button btnLoadText;
  }
}

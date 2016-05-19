namespace Quantifai
{
  partial class SXE
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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, "0,0");
      this.btnTest = new System.Windows.Forms.Button();
      this.StockList = new System.Windows.Forms.ListBox();
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      this.SuspendLayout();
      // 
      // btnTest
      // 
      this.btnTest.Location = new System.Drawing.Point(4, 4);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(75, 23);
      this.btnTest.TabIndex = 0;
      this.btnTest.Text = "Test";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // StockList
      // 
      this.StockList.FormattingEnabled = true;
      this.StockList.Items.AddRange(new object[] {
            "$AAPL",
            "$MSFT"});
      this.StockList.Location = new System.Drawing.Point(4, 33);
      this.StockList.Name = "StockList";
      this.StockList.Size = new System.Drawing.Size(131, 485);
      this.StockList.TabIndex = 1;
      // 
      // chart1
      // 
      chartArea2.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea2);
      legend2.Name = "Legend1";
      this.chart1.Legends.Add(legend2);
      this.chart1.Location = new System.Drawing.Point(142, 33);
      this.chart1.Name = "chart1";
      series2.ChartArea = "ChartArea1";
      series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
      series2.Legend = "Legend1";
      series2.Name = "Series1";
      series2.Points.Add(dataPoint2);
      series2.YValuesPerPoint = 2;
      this.chart1.Series.Add(series2);
      this.chart1.Size = new System.Drawing.Size(553, 300);
      this.chart1.TabIndex = 2;
      this.chart1.Text = "chart1";
      // 
      // SXE
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.chart1);
      this.Controls.Add(this.StockList);
      this.Controls.Add(this.btnTest);
      this.Name = "SXE";
      this.Size = new System.Drawing.Size(711, 526);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.ListBox StockList;
    private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    private System.Windows.Forms.Timer timer1;
  }
}

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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.btnTest = new System.Windows.Forms.Button();
      this.StockList = new System.Windows.Forms.ListBox();
      this.StockChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.StockText = new System.Windows.Forms.TextBox();
      this.StockLister = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.StockChart)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StockLister)).BeginInit();
      this.SuspendLayout();
      // 
      // btnTest
      // 
      this.btnTest.Location = new System.Drawing.Point(141, 6);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(75, 23);
      this.btnTest.TabIndex = 0;
      this.btnTest.Text = "Search";
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
      // StockChart
      // 
      chartArea5.Name = "ChartArea1";
      this.StockChart.ChartAreas.Add(chartArea5);
      legend5.Name = "Legend1";
      this.StockChart.Legends.Add(legend5);
      this.StockChart.Location = new System.Drawing.Point(142, 33);
      this.StockChart.Name = "StockChart";
      series5.ChartArea = "ChartArea1";
      series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
      series5.Legend = "Legend1";
      series5.Name = "Series1";
      series5.YValuesPerPoint = 2;
      this.StockChart.Series.Add(series5);
      this.StockChart.Size = new System.Drawing.Size(553, 300);
      this.StockChart.TabIndex = 2;
      this.StockChart.Text = "chart1";
      // 
      // StockText
      // 
      this.StockText.Location = new System.Drawing.Point(5, 7);
      this.StockText.Name = "StockText";
      this.StockText.Size = new System.Drawing.Size(131, 20);
      this.StockText.TabIndex = 3;
      this.StockText.Text = "AAPL";
      // 
      // StockLister
      // 
      this.StockLister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.StockLister.Location = new System.Drawing.Point(142, 340);
      this.StockLister.Name = "StockLister";
      this.StockLister.Size = new System.Drawing.Size(553, 178);
      this.StockLister.TabIndex = 4;
      // 
      // SXE
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = global::Quantifai.Properties.Resources.bg2;
      this.Controls.Add(this.StockLister);
      this.Controls.Add(this.StockText);
      this.Controls.Add(this.StockChart);
      this.Controls.Add(this.StockList);
      this.Controls.Add(this.btnTest);
      this.Name = "SXE";
      this.Size = new System.Drawing.Size(1012, 526);
      ((System.ComponentModel.ISupportInitialize)(this.StockChart)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StockLister)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.ListBox StockList;
    private System.Windows.Forms.DataVisualization.Charting.Chart StockChart;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TextBox StockText;
    private System.Windows.Forms.DataGridView StockLister;
  }
}

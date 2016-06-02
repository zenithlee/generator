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
      this.StockChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.StockText = new System.Windows.Forms.TextBox();
      this.StockLister = new System.Windows.Forms.DataGridView();
      this.btn_Test = new System.Windows.Forms.Button();
      this.btn_Clear = new System.Windows.Forms.Button();
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
      chartArea2.Name = "ChartArea1";
      this.StockChart.ChartAreas.Add(chartArea2);
      legend2.Name = "Legend1";
      this.StockChart.Legends.Add(legend2);
      this.StockChart.Location = new System.Drawing.Point(142, 33);
      this.StockChart.Name = "StockChart";
      series2.ChartArea = "ChartArea1";
      series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
      series2.Legend = "Legend1";
      series2.Name = "Series1";
      series2.Points.Add(dataPoint2);
      series2.YValuesPerPoint = 2;
      this.StockChart.Series.Add(series2);
      this.StockChart.Size = new System.Drawing.Size(553, 300);
      this.StockChart.TabIndex = 2;
      this.StockChart.Text = "chart1";
      this.StockChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.StockChart_AxisViewChanged);
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
      // btn_Test
      // 
      this.btn_Test.Location = new System.Drawing.Point(222, 6);
      this.btn_Test.Name = "btn_Test";
      this.btn_Test.Size = new System.Drawing.Size(75, 23);
      this.btn_Test.TabIndex = 5;
      this.btn_Test.Text = "Test";
      this.btn_Test.UseVisualStyleBackColor = true;
      this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
      // 
      // btn_Clear
      // 
      this.btn_Clear.Location = new System.Drawing.Point(303, 6);
      this.btn_Clear.Name = "btn_Clear";
      this.btn_Clear.Size = new System.Drawing.Size(75, 23);
      this.btn_Clear.TabIndex = 6;
      this.btn_Clear.Text = "Clear";
      this.btn_Clear.UseVisualStyleBackColor = true;
      this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
      // 
      // SXE
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = global::Quantifai.Properties.Resources.bg2;
      this.Controls.Add(this.btn_Clear);
      this.Controls.Add(this.btn_Test);
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
    private System.Windows.Forms.Button btn_Test;
    private System.Windows.Forms.Button btn_Clear;
  }
}

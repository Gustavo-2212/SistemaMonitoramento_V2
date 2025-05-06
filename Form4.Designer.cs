namespace SistemaMonitoramentoSE_v2
{
    partial class FaseC
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
            components = new System.ComponentModel.Container();
            corrente_chart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            tensao_chart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            update_plots = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // corrente_chart
            // 
            corrente_chart.BackColor = SystemColors.ButtonHighlight;
            corrente_chart.BorderStyle = BorderStyle.Fixed3D;
            corrente_chart.Location = new Point(12, 12);
            corrente_chart.MatchAxesScreenDataRatio = false;
            corrente_chart.Name = "corrente_chart";
            corrente_chart.Size = new Size(1356, 493);
            corrente_chart.TabIndex = 4;
            // 
            // tensao_chart
            // 
            tensao_chart.BackColor = SystemColors.ButtonHighlight;
            tensao_chart.BorderStyle = BorderStyle.Fixed3D;
            tensao_chart.Location = new Point(12, 530);
            tensao_chart.MatchAxesScreenDataRatio = false;
            tensao_chart.Name = "tensao_chart";
            tensao_chart.Size = new Size(1356, 493);
            tensao_chart.TabIndex = 5;
            // 
            // update_plots
            // 
            update_plots.Tick += update_plots_Tick;
            // 
            // FaseC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1370, 749);
            Controls.Add(tensao_chart);
            Controls.Add(corrente_chart);
            Name = "FaseC";
            Text = "Fase C";
            Load += FaseC_Load;
            ResumeLayout(false);
        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart corrente_chart;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart tensao_chart;
        private System.Windows.Forms.Timer update_plots;
    }
}
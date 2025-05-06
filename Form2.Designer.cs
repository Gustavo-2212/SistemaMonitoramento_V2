namespace SistemaMonitoramentoSE_v2
{
    partial class FaseA
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
            update_plots = new System.Windows.Forms.Timer(components);
            tensao_chart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            corrente_chart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            SuspendLayout();
            // 
            // update_plots
            // 
            update_plots.Enabled = true;
            update_plots.Tick += update_plots_Tick;
            // 
            // tensao_chart
            // 
            tensao_chart.BackColor = SystemColors.ButtonHighlight;
            tensao_chart.BorderStyle = BorderStyle.Fixed3D;
            tensao_chart.Location = new Point(12, 556);
            tensao_chart.MatchAxesScreenDataRatio = false;
            tensao_chart.Name = "tensao_chart";
            tensao_chart.Size = new Size(1356, 493);
            tensao_chart.TabIndex = 1;
            // 
            // corrente_chart
            // 
            corrente_chart.BackColor = SystemColors.ButtonHighlight;
            corrente_chart.BorderStyle = BorderStyle.Fixed3D;
            corrente_chart.Location = new Point(12, 26);
            corrente_chart.MatchAxesScreenDataRatio = false;
            corrente_chart.Name = "corrente_chart";
            corrente_chart.Size = new Size(1356, 493);
            corrente_chart.TabIndex = 2;
            // 
            // FaseA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1370, 749);
            Controls.Add(corrente_chart);
            Controls.Add(tensao_chart);
            Name = "FaseA";
            Text = "Fase A";
            Load += FaseA_Load;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer update_plots;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart tensao_chart;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart corrente_chart;
    }
}
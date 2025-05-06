namespace SistemaMonitoramentoSE_v2
{
    partial class Eventos
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            eventos_m2 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            eventos_m4 = new ListView();
            MU = new ColumnHeader();
            Parametro = new ColumnHeader();
            Regra = new ColumnHeader();
            Valor = new ColumnHeader();
            DataHora = new ColumnHeader();
            eventos_m5 = new ListView();
            Latitude = new ColumnHeader();
            Logitude = new ColumnHeader();
            Data_Hora = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            atualiza_listviews = new System.Windows.Forms.Timer(components);
            filtros_m4 = new ListView();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            label4 = new Label();
            timer_filtro = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // eventos_m2
            // 
            eventos_m2.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
            eventos_m2.Location = new Point(12, 36);
            eventos_m2.Name = "eventos_m2";
            eventos_m2.Size = new Size(466, 524);
            eventos_m2.TabIndex = 0;
            eventos_m2.UseCompatibleStateImageBehavior = false;
            eventos_m2.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "MU";
            columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Evento";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Data e Hora (Início)";
            columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Data e Hora (Fim)";
            columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Fase";
            // 
            // eventos_m4
            // 
            eventos_m4.Columns.AddRange(new ColumnHeader[] { MU, Parametro, Regra, Valor, DataHora });
            eventos_m4.Location = new Point(518, 36);
            eventos_m4.Name = "eventos_m4";
            eventos_m4.Size = new Size(504, 352);
            eventos_m4.TabIndex = 1;
            eventos_m4.UseCompatibleStateImageBehavior = false;
            eventos_m4.View = View.Details;
            // 
            // MU
            // 
            MU.Text = "MU";
            MU.Width = 40;
            // 
            // Parametro
            // 
            Parametro.Text = "Parâmetro";
            Parametro.Width = 80;
            // 
            // Regra
            // 
            Regra.Text = "Regra";
            Regra.Width = 120;
            // 
            // Valor
            // 
            Valor.Text = "Valor";
            // 
            // DataHora
            // 
            DataHora.Text = "Data e Hora";
            DataHora.Width = 120;
            // 
            // eventos_m5
            // 
            eventos_m5.Columns.AddRange(new ColumnHeader[] { Latitude, Logitude, Data_Hora, columnHeader6 });
            eventos_m5.Location = new Point(1062, 36);
            eventos_m5.Name = "eventos_m5";
            eventos_m5.Size = new Size(430, 524);
            eventos_m5.TabIndex = 2;
            eventos_m5.UseCompatibleStateImageBehavior = false;
            eventos_m5.View = View.Details;
            // 
            // Latitude
            // 
            Latitude.Text = "Latitude";
            // 
            // Logitude
            // 
            Logitude.Text = "Longitude";
            Logitude.Width = 80;
            // 
            // Data_Hora
            // 
            Data_Hora.Text = "Data e Hora";
            Data_Hora.Width = 120;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Erro";
            columnHeader6.Width = 160;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(180, 23);
            label1.TabIndex = 3;
            label1.Text = "EVENTOS MÓDULO 2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(518, 9);
            label2.Name = "label2";
            label2.Size = new Size(180, 23);
            label2.TabIndex = 4;
            label2.Text = "EVENTOS MÓDULO 4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(1062, 10);
            label3.Name = "label3";
            label3.Size = new Size(180, 23);
            label3.TabIndex = 5;
            label3.Text = "EVENTOS MÓDULO 5";
            // 
            // atualiza_listviews
            // 
            atualiza_listviews.Enabled = true;
            atualiza_listviews.Tick += atualiza_listviews_Tick;
            // 
            // filtros_m4
            // 
            filtros_m4.Columns.AddRange(new ColumnHeader[] { columnHeader7, columnHeader8, columnHeader9, columnHeader10, columnHeader11 });
            filtros_m4.Location = new Point(518, 428);
            filtros_m4.Name = "filtros_m4";
            filtros_m4.Size = new Size(504, 132);
            filtros_m4.TabIndex = 6;
            filtros_m4.UseCompatibleStateImageBehavior = false;
            filtros_m4.View = View.Details;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Regra";
            columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Ocorrência (Total)";
            columnHeader8.Width = 110;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "Ocorrência (LR)";
            columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "MU";
            columnHeader10.Width = 40;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Todas MU";
            columnHeader11.Width = 100;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(518, 402);
            label4.Name = "label4";
            label4.Size = new Size(76, 23);
            label4.TabIndex = 7;
            label4.Text = "FILTROS";
            // 
            // timer_filtro
            // 
            timer_filtro.Enabled = true;
            timer_filtro.Interval = 500;
            timer_filtro.Tick += timer_filtro_Tick;
            // 
            // Eventos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1370, 586);
            Controls.Add(label4);
            Controls.Add(filtros_m4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(eventos_m5);
            Controls.Add(eventos_m4);
            Controls.Add(eventos_m2);
            Name = "Eventos";
            Text = "Eventos";
            Load += Eventos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView eventos_m2;
        private ListView eventos_m4;
        private ListView eventos_m5;
        private Label label1;
        private Label label2;
        private Label label3;
        private ColumnHeader MU;
        private ColumnHeader Regra;
        private ColumnHeader Valor;
        private ColumnHeader DataHora;
        private ColumnHeader Latitude;
        private ColumnHeader Logitude;
        private ColumnHeader Data_Hora;
        private System.Windows.Forms.Timer atualiza_listviews;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader Parametro;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ListView filtros_m4;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private Label label4;
        private System.Windows.Forms.Timer timer_filtro;
    }
}

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMonitoramentoSE_v2
{
    public partial class FaseB : Form
    {
        private Dictionary<int, SKColor> ied_colors = new();
        private SKColor[] default_colors = {
            SKColors.Red, SKColors.Green, SKColors.Blue, SKColors.Magenta,
            SKColors.Silver, SKColors.Yellow, SKColors.Black, SKColors.Gray,
            SKColors.Aquamarine, SKColors.Brown
        };
        private int index_color = 0;

        private Dictionary<int, ViewModelCorrentePorIED> view_models_corrente = new();
        private Dictionary<int, ViewModelTensaoPorIED> view_models_tensao = new();

        private Axis x_axis_corrente;
        private Axis x_axis_tensao;

        public object Sync { get; } = new object();

        public FaseB()
        {
            InitializeComponent();

            x_axis_corrente = new DateTimeAxis(TimeSpan.FromMilliseconds(10), date => date.ToString("HH:mm:ss"))
            {
                MinLimit = null,
                MaxLimit = null,
                ForceStepToMin = false,
                LabelsRotation = 15,
                UnitWidth = TimeSpan.FromMilliseconds(10).Ticks,
                IsVisible = true
            };
            x_axis_tensao = new DateTimeAxis(TimeSpan.FromMilliseconds(10), date => date.ToString("HH:mm:ss"))
            {
                MinLimit = null,
                MaxLimit = null,
                ForceStepToMin = false,
                LabelsRotation = 15,
                UnitWidth = TimeSpan.FromMilliseconds(10).Ticks,
                IsVisible = true
            };

            corrente_chart.XAxes = new[] { x_axis_corrente };
            tensao_chart.XAxes = new[] { x_axis_tensao };

            corrente_chart.Series = new ObservableCollection<ISeries>();
            tensao_chart.Series = new ObservableCollection<ISeries>();

            update_plots.Interval = 10;
            update_plots.Enabled = true;
            update_plots.Start();
        }
        private void FaseB_Load(object sender, EventArgs e)
        {
            corrente_chart.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.X;
            tensao_chart.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.X;

            corrente_chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Corrente [A]",
                    LabelsPaint = new SolidColorPaint(SKColors.Black)
                }
            };

            tensao_chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Tensão [V]",
                    LabelsPaint = new SolidColorPaint(SKColors.Black)
                }
            };

            this.Resize += Form_Resize;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            int margem_lateral = 50;
            int margem_vertical = 30;
            int altura_grafico = (this.ClientSize.Height - (3 * margem_vertical)) / 2;

            int nova_largura = this.ClientSize.Width - (2 * margem_lateral);

            corrente_chart.Width = nova_largura;
            corrente_chart.Left = margem_lateral;
            corrente_chart.Top = margem_vertical;
            corrente_chart.Height = altura_grafico;

            tensao_chart.Width = nova_largura;
            tensao_chart.Left = margem_lateral;
            tensao_chart.Top = corrente_chart.Bottom + margem_vertical;
            tensao_chart.Height = altura_grafico;
        }

        private SKColor GetColorByid_MU(int id_MU)
        {
            if (!ied_colors.ContainsKey(id_MU))
            {
                SKColor color = default_colors[index_color % default_colors.Length];
                lock (ied_colors) ied_colors[id_MU] = color;
                index_color++;
            }

            return ied_colors[id_MU];
        }

        private void update_plots_Tick(object sender, EventArgs e)
        {
            Task.Run(() => update_corrente_plot());
            Task.Run(() => update_tensao_plot());
        }

        private void update_corrente_plot()
        {
            lock (M1DataHandler.lock_cb)
            {
                foreach (var (id_MU, buffer) in M1DataHandler.corrente_faseB)
                {
                    if (!view_models_corrente.ContainsKey(id_MU))
                    {
                        var cor = GetColorByid_MU(id_MU);
                        var vm = new ViewModelCorrentePorIED(id_MU, cor);
                        view_models_corrente[id_MU] = vm;

                        corrente_chart.Invoke(() => ((ObservableCollection<ISeries>)corrente_chart.Series).Add(vm.corrente_series.First()));
                    }

                    var model = view_models_corrente[id_MU];
                    lock (buffer)
                    {
                        if (buffer.Count > 0)
                        {
                            if (model.ultima_att < M1DataHandler.timestampB)
                            {
                                var ts = M1DataHandler.timestampB;
                                var i = buffer.Last();
                                lock (model.Sync) model.AddDados(ts, i);
                                model.ultima_att = ts;

                                corrente_chart.Invoke(() =>
                                {
                                    double segundos_janela = 5;

                                    x_axis_corrente.MinLimit = ts.AddSeconds(-segundos_janela).Ticks;
                                    x_axis_corrente.MaxLimit = ts.AddSeconds(segundos_janela - 2).Ticks;
                                });

                            }
                        }
                    }
                }
            }
        }

        private void update_tensao_plot()
        {
            lock (M1DataHandler.lock_tb)
            {
                foreach (var (id_MU, buffer) in M1DataHandler.tensao_faseB)
                {
                    if (!view_models_tensao.ContainsKey(id_MU))
                    {
                        var cor = GetColorByid_MU(id_MU);
                        var vm = new ViewModelTensaoPorIED(id_MU, cor);
                        view_models_tensao[id_MU] = vm;

                        tensao_chart.Invoke(() => ((ObservableCollection<ISeries>)tensao_chart.Series).Add(vm.tensao_series.First()));
                    }

                    var model = view_models_tensao[id_MU];
                    lock (buffer)
                    {
                        if (buffer.Count > 0)
                        {
                            if (model.ultima_att < M1DataHandler.timestampB)
                            {
                                var ts = M1DataHandler.timestampB;
                                var i = buffer.Last();
                                lock (model.Sync) model.AddDados(ts, i);
                                model.ultima_att = ts;

                                tensao_chart.Invoke(() =>
                                {
                                    double segundos_janela = 5;

                                    x_axis_tensao.MinLimit = ts.AddSeconds(-segundos_janela).Ticks;
                                    x_axis_tensao.MaxLimit = ts.AddSeconds(segundos_janela - 2).Ticks; ;
                                });

                            }
                        }
                    }
                }
            }
        }
    }
}

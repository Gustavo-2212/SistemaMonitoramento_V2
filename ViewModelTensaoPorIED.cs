using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using CommunityToolkit.Mvvm.ComponentModel;


namespace SistemaMonitoramentoSE_v2
{
    public partial class ViewModelTensaoPorIED : ObservableObject
    {
        private readonly object sync = new();
        private readonly ObservableCollection<DateTimePoint> _tensao = new();

        public ObservableCollection<ISeries> tensao_series { get; set; }

        public DateTime ultima_att { get; set; } = DateTime.MinValue;

        public ViewModelTensaoPorIED(int id_MU, SKColor cor)
        {
            tensao_series = new()
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _tensao,
                    Stroke = new SolidColorPaint(cor, 2),
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    Name = $"IED {id_MU}"
                }
            };
        }

        public void AddDados(DateTime timestamp, double tensao)
        {
            lock (sync)
            {
                _tensao.Add(new DateTimePoint(timestamp, tensao));

                if (_tensao.Count > 100) _tensao.RemoveAt(0);
            }
        }

        public object Sync => sync;
    }
}

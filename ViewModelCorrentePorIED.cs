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
    public partial class ViewModelCorrentePorIED : ObservableObject
    {
        private readonly object sync = new();
        private readonly ObservableCollection<DateTimePoint> _corrente = new();

        public ObservableCollection<ISeries> corrente_series { get; set; }

        public DateTime ultima_att { get; set; } = DateTime.MinValue;

        public ViewModelCorrentePorIED(int id_MU, SKColor cor)
        {
            corrente_series = new()
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _corrente,
                    Stroke = new SolidColorPaint(cor, 2),
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    Name = $"IED {id_MU}"
                }
            };
        }

        public void AddDados(DateTime timestamp, double corrente)
        {
            lock (sync)
            {
                _corrente.Add(new DateTimePoint(timestamp, corrente));

                if (_corrente.Count > 100) _corrente.RemoveAt(0);
            }
        }

        public object Sync => sync;
    }
}

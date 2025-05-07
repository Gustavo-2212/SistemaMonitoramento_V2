namespace SistemaMonitoramentoSE_v2
{
    public partial class Eventos : Form
    {
        private readonly FaseA faseA;
        private readonly FaseB faseB;
        private readonly FaseC faseC;

        public Eventos()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            //Task.Run(() =>
            //{
            var faseA = new FaseA();
            faseA.Show();
            //  Application.Run(faseA);
            //});

            //Task.Run(() =>
            //{
            var faseB = new FaseB();
            faseB.Show();
            //  Application.Run(faseB);
            //});

            //Task.Run(() =>
            //{
            var faseC = new FaseC();
            faseC.Show();
            //    Application.Run(faseC);
            //});

            SwitchConnection connection = new SwitchConnection(12345, faseA, faseB, faseC);
            connection.Start();
        }
        private void Eventos_Load(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        private void atualiza_listviews_Tick(object sender, EventArgs e)
        {
            Task.Run(() => handle_eventos_m2());
            Task.Run(() => handle_eventos_m4());
            Task.Run(() => handle_eventos_m5());
        }


        private void handle_eventos_m2()
        {
            var buffer = M2DataHandler.eventos_m2;
            while (buffer.Count > 0)
            {
                var evento = buffer.Dequeue();

                foreach (ListViewItem i in eventos_m2.Items)
                {
                    i.BackColor = Color.FromArgb(40, 40, 40);
                    i.ForeColor = Color.White;
                }

                var item = new ListViewItem([(evento.MU).ToString(), (evento.evento ?? "Tranquilo"), (evento.data_inicio).ToString("dd/MM HH:mm:ss"), (evento.data_fim).ToString("dd/MM HH:mm:ss"), evento.fase.ToString()]);

                item.BackColor = Color.LightGreen;
                item.ForeColor = Color.Black;
                
                eventos_m2.Items.Insert(0, item);

                if (eventos_m2.Items.Count > 35)
                {
                    eventos_m2.Items.RemoveAt(eventos_m2.Items.Count - 1);
                }
            }
        }

        private void handle_eventos_m4()
        {
            var buffer = M4DataHandler.eventos_m4;
            while (buffer.Count > 0)
            {
                var evento = buffer.Dequeue();

                foreach (ListViewItem i in eventos_m4.Items)
                {
                    var x = i;
                    i.BackColor = Color.FromArgb(40, 40, 40);
                    i.ForeColor = Color.White;
                }

                var item = new ListViewItem([
                    (evento.MU).ToString(),
                    evento.parametro,
                    evento.regra,
                    evento.valor.ToString(),
                    (evento.timestamp).ToString("dd/MM HH:mm:ss")
                ]);

                item.BackColor = Color.LightGreen;
                item.ForeColor = Color.Black;

                eventos_m4.Items.Insert(0, item);

                if (eventos_m4.Items.Count > 35)
                {
                    eventos_m4.Items.RemoveAt(eventos_m2.Items.Count - 1);
                }
            }
        }

        private void handle_eventos_m5()
        {
            var buffer = M5DataHandler.eventos_m5;
            while (buffer.Count > 0)
            {
                var evento = buffer.Dequeue();

                foreach (ListViewItem i in eventos_m5.Items)
                {
                    i.BackColor = Color.FromArgb(40, 40, 40);
                    i.ForeColor = Color.White;
                }

                var item = new ListViewItem([
                    evento.latitude,
                    evento.longitude,
                    evento.data_hora.ToString("dd/MM HH:mm:ss"),
                    evento.erro.ToString()
                ]);

                item.BackColor = Color.LightGreen;
                item.ForeColor = Color.Black;

                eventos_m5.Items.Insert(0, item);

                if (eventos_m5.Items.Count > 35)
                {
                    eventos_m5.Items.RemoveAt(eventos_m2.Items.Count - 1);
                }
            }
        }

        private void handle_filtros()
        {
            var filtros = M4DataHandler.filtros;
            filtros_m4.Items.Clear();
            foreach (var (regra, valor) in filtros)
            {
                var item = new ListViewItem([
                    regra,
                    valor.count_t.ToString(),
                    valor.count_lr.ToString(),
                    valor.MU.ToString(),
                    (valor.todas_MUs) ? "Sim" : "Não"
                ]);

                filtros_m4.Items.Add(item);
            }
        }

        private void timer_filtro_Tick(object sender, EventArgs e)
        {
            Task.Run(() => handle_filtros());
        }
    }
}

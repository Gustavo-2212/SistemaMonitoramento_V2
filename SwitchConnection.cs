using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramentoSE_v2
{
    public class SwitchConnection
    {
        private readonly UdpClient udp_client;
        private readonly CancellationTokenSource cts = new();
        private Task? read_task;

        private readonly FaseA formA;
        private readonly Form formB;    
        private readonly Form formC;

        private readonly Dictionary<string, IMessageHandler> handlers = new();

        public SwitchConnection(int port, FaseA formA, Form formB, Form formC)
        {
            udp_client = new UdpClient(new IPEndPoint(IPAddress.Any, port));
            this.formA = formA;
            this.formB = formB;
            this.formC = formC;

            handlers["22/1"] = new M1DataHandler();
            handlers["22/2"] = new M1DataHandler();
            handlers["22/3"] = new M1DataHandler();
            handlers["M2/DATA"] = new M2DataHandler();
            handlers["101/1"] = new M4DataHandler();
            handlers["101/2"] = new M4DataHandler();
            handlers["102"] = new M5DataHandler();
        }

        public void Start()
        {
            read_task = Task.Run(() => ListenAsync(cts.Token));
        }

        public void Stop()
        {
            cts.Cancel();
            read_task?.Wait();
            udp_client.Close();
        }

        private async Task ListenAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await udp_client.ReceiveAsync();
                    string raw = Encoding.UTF8.GetString(result.Buffer);

                    JObject json = JObject.Parse(raw);
                    string? uri = json["URI"]?.ToString();

                    if (uri != null && handlers.TryGetValue(uri, out var handler))
                        handler.Handle(json);
                    else
                        MessageBox.Show($"Mensagem com URI desconhecida: {uri}\n{json.ToString()}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao receber o pacote: {ex.Message}");
                }
            }
        }
    }

    interface IMessageHandler
    {
        void Handle(JObject json);
    }

    class M1DataHandler : IMessageHandler
    {
        // Controla a existência do módulo e cria seus buffers próprios
        // Variável estática para ser possível capturar nos formulários windows
        // Buffers por IED para a fase A
        public static Dictionary<int, Queue<double>> corrente_faseA = new();
        public static Dictionary<int, Queue<double>> tensao_faseA = new();

        // Buffers por IED para a fase B
        public static Dictionary<int, Queue<double>> corrente_faseB = new();
        public static Dictionary<int, Queue<double>> tensao_faseB = new();

        // Buffers por IED para a fase C
        public static Dictionary<int, Queue<double>> corrente_faseC = new();
        public static Dictionary<int, Queue<double>> tensao_faseC = new();
        public static DateTime timestampA { get; set; }
        public static DateTime timestampB { get; set; }
        public static DateTime timestampC { get; set; }


        // Locks para lidar com os acessos concorrentes (race conditions)
        public static readonly object lock_ca = new();
        public static readonly object lock_ta = new();

        public static readonly object lock_cb = new();
        public static readonly object lock_tb = new();

        public static readonly object lock_cc = new();
        public static readonly object lock_tc = new();

        public M1DataHandler()
        {}

        public void Handle(JObject json)
        {
            int id_MU = Convert.ToInt32(json["ied"]?.ToString());
            char fase = json.TryGetValue("fase", out JToken? faseToken) ? faseToken.ToObject<char>() : 'A';
            double corrente = Convert.ToDouble(json["current"]?.ToString());
            double tensao   = Convert.ToDouble(json["voltage"]?.ToString());
            var timestamp = json["timestamp"]?.ToString();

            switch (fase)
            {
                case 'A':
                    lock (lock_ca)
                    {
                        var buffer_correnteA = corrente_faseA.ContainsKey(id_MU) ? corrente_faseA[id_MU] : corrente_faseA[id_MU] = new();
                        if (buffer_correnteA.Count >= 1000) buffer_correnteA.Dequeue();
                        buffer_correnteA.Enqueue(corrente);
                    }

                    lock (lock_ta)
                    {
                        var buffer_tensaoA = tensao_faseA.ContainsKey(id_MU) ? tensao_faseA[id_MU] : tensao_faseA[id_MU] = new();
                        if (buffer_tensaoA.Count >= 1000) buffer_tensaoA.Dequeue();
                        buffer_tensaoA.Enqueue(tensao);
                    }

                    timestampA = DateTime.TryParse(timestamp, out var ta) ? ta : DateTime.UtcNow;

                    break;

                case 'B':
                    lock (lock_cb)
                    {
                        var buffer_correnteB = corrente_faseB.ContainsKey(id_MU) ? corrente_faseB[id_MU] : corrente_faseB[id_MU] = new();
                        if (buffer_correnteB.Count >= 1000) buffer_correnteB.Dequeue();
                        buffer_correnteB.Enqueue(corrente);
                    }

                    lock (lock_tb)
                    {
                        var buffer_tensaoB = tensao_faseB.ContainsKey(id_MU) ? tensao_faseB[id_MU] : tensao_faseB[id_MU] = new();
                        if (buffer_tensaoB.Count >= 1000) buffer_tensaoB.Dequeue();
                        buffer_tensaoB.Enqueue(tensao);
                    }

                    timestampB = DateTime.TryParse(timestamp, out var tb) ? tb : DateTime.UtcNow;

                    break;

                case 'C':
                    lock (lock_cc)
                    {
                        var buffer_correnteC = corrente_faseC.ContainsKey(id_MU) ? corrente_faseC[id_MU] : corrente_faseC[id_MU] = new();
                        if (buffer_correnteC.Count >= 1000) buffer_correnteC.Dequeue();
                        buffer_correnteC.Enqueue(corrente);
                    }

                    lock (lock_tc)
                    {
                        var buffer_tensaoC = tensao_faseC.ContainsKey(id_MU) ? tensao_faseC[id_MU] : tensao_faseC[id_MU] = new();
                        if (buffer_tensaoC.Count >= 1000) buffer_tensaoC.Dequeue();
                        buffer_tensaoC.Enqueue(tensao);
                    }

                    timestampC = DateTime.TryParse(timestamp, out var tc) ? tc : DateTime.UtcNow;

                    break;

                default:
                    MessageBox.Show("Informação de \"fase\" do pacote inválida");
                    break;
            }
        }
    }

    record EventoM2Obj(int MU, string? evento, DateTime data_inicio, DateTime data_fim, char fase);
    class M2DataHandler : IMessageHandler
    {
        public static Queue<EventoM2Obj> eventos_m2 = new();
        public void Handle(JObject json)
        {
            var MU = Convert.ToInt32(json["ied"]?.ToString());
            var evento = json["evento"]?.ToString();
            var data_inicio = DateTime.TryParse(json["data_inicio"]?.ToString(), out var ti) ? ti : DateTime.UtcNow;
            var data_fim = DateTime.TryParse(json["data_fim"]?.ToString(), out var tf) ? tf : DateTime.UtcNow;
            char fase = json.TryGetValue("fase", out JToken? faseToken) ? faseToken.ToObject<char>() : 'A';

            var evento_obj = new EventoM2Obj(MU, evento, data_inicio, data_fim, fase);

            if (eventos_m2.Count >= 1000) eventos_m2.Dequeue();
            eventos_m2.Enqueue(evento_obj);
        }
    }

    record EventoM4Obj(int MU, string regra, double valor, string parametro, DateTime timestamp);
    record FiltroObj(int MU, bool todas_MUs, int count_t, int count_lr);
    class M4DataHandler : IMessageHandler
    {
        public static Queue<EventoM4Obj> eventos_m4 = new();
        public static Dictionary<string, FiltroObj> filtros = new();

        public void Handle(JObject json)
        {
            var tipo_evento = json["event_type"]?.ToString();
            var timestamp = DateTime.TryParse(json["timeStamping"]?.ToString(), out var ts) ? ts : DateTime.Now;
            var regra = json["rule_applied"]?.ToString();
            var parametro = json["parameter"]?.ToString();

            if (regra == null)
            {
                MessageBox.Show("Regra inválida aplicada!");
                return;
            }

            if (tipo_evento == "rule_triggered") // Filtro acionado
            {
                var ied = Convert.ToInt32(json["ied_id"]?.ToString());
                var valor_medido = Convert.ToDouble(json["value"]?.ToString());

                var evento_obj = new EventoM4Obj(ied, regra, valor_medido, parametro, timestamp);

                if (eventos_m4.Count >= 1000) eventos_m4.Dequeue();
                eventos_m4.Enqueue(evento_obj);
            }
            else if (tipo_evento == "periodic_rule_report")
            {
                var count_total = Convert.ToInt32(json["hit_count_total"]?.ToString());
                var count_ultimo_report = Convert.ToInt32(json["hit_count_since_last_report"]?.ToString());
                var todas_ieds = (json["todas_IEDs"]?.ToString() == "True") ? true : false;
                var ied_selecionada = (json["IED_selecionada"]?.ToString() == "None") ? -1 : Convert.ToInt32(json["IED_selecionada"]?.ToString());

                filtros[regra] = new FiltroObj(ied_selecionada, todas_ieds, count_total, count_ultimo_report);
            }
            else MessageBox.Show($"Tipo de evento inesperado: {tipo_evento} - {timestamp.ToString("dd/MM HH:mm:ss")}");
        }
    }

    record EventoM5Obj(string latitude, string longitude, DateTime data_hora, string erro);
    class M5DataHandler : IMessageHandler
    {
        public static Queue<EventoM5Obj> eventos_m5 = new();
        public void Handle(JObject json)
        {
            var latitude = json["lat"]?.ToString();
            var longitude = json["lon"]?.ToString();
            var data_hora = DateTime.TryParse(json["timestamp"]?.ToString(), out var ts) ? ts : DateTime.UtcNow;
            var cod_erro = Convert.ToInt16(json["error_code"]?.ToString());
            var erro = string.Empty;

            switch (cod_erro)
            {
                case 1:
                    erro = "Queda de energia total";
                    break;

                case 2:
                    erro = "Falha de medidor/sensor";
                    break;

                case 3:
                    erro = "Horário inconsistente";
                    break;

                case 4:
                    erro = "Queda parcial/flutuação";
                    break;

                case 5:
                    erro = "Não identificado";
                    break;

                default:
                    MessageBox.Show("Código de erro - módulo 5 - não previsto!");
                    break;

            }

            var evento = new EventoM5Obj(latitude, longitude, data_hora, erro);
            if (eventos_m5.Count >= 1000) eventos_m5.Dequeue();
            eventos_m5.Enqueue(evento);
        }
    }
}

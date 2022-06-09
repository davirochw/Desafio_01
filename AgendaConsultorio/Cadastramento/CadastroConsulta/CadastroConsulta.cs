using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaConsultorio.CadastroPaciente;

namespace AgendaConsultorio.CadastroConsulta
{
    public class CadastroConsulta : Cadastramento.Cadastramento
    {
        public CadastroConsulta()
        {

        }

        public void CadastraConsulta()
        {
            while (true)
            {
                Console.Clear();
                AdicionaConsulta();
            }
        }

        private void AdicionaConsulta()
        {
            Console.Clear();
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            if (pacientes.Count > 0)
            {
                pacientes.ForEach(p => {
                    if (p.Cpf != cpf)
                    {
                        Console.WriteLine("Erro: paciente não cadastrado");
                        Console.ReadKey();
                    }
                });
            }
            else
            {
                Console.WriteLine("Erro: paciente não cadastrado");
                Console.ReadKey();
            }

            Console.Write("Data da Consulta: ");
            string dataConsulta = Console.ReadLine();

            Console.Write("Hora inicial: ");
            string horaInicial = Console.ReadLine();

            Console.Write("Hora final: ");
            string horaFinal = Console.ReadLine();

            while (ValidaDataConsulta(dataConsulta, horaInicial, horaFinal).Equals(false))
            {
                Console.WriteLine();
                Console.Write("Data da consulta: ");
                dataConsulta = Console.ReadLine();

                Console.Write("Hora inicial: ");
                horaInicial = Console.ReadLine();

                Console.Write("Hora final: ");
                horaFinal = Console.ReadLine();
            }

            consulta = new Consulta(cpf, dataConsulta, horaInicial, horaFinal);

            if (VerificaConsulta(dataConsulta, horaInicial, horaFinal))
            {
                consultas.Add(consulta);
            }
            else
            {
                Console.ReadKey();
            }
        }

        private bool VerificaConsulta(string dataConsulta, string horaInicial, string horaFinal)
        {
            try
            {
                foreach (var c in consultas)
                {
                    var dataConsultada = DateTime.Parse(c.DataConsulta);
                    var dataAtual = DateTime.Now;

                    if (dataConsultada >= dataAtual)
                    {
                        return false;
                    }

                    var verificaData = c.DataConsulta.Equals(dataConsulta);
                    var verificaHoraInicial = c.HoraInicial.Equals(horaInicial);
                    var verificaHoraFinal = c.HoraFinal.Equals(horaFinal);

                    if (verificaData && verificaHoraInicial && verificaHoraFinal)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Erro: já existe uma consulta agendada nesta data/hora");
                        Console.WriteLine();
                        return false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Agendamento realizado com sucesso!");
                        Console.WriteLine();
                        return true;
                    }
                }
                return true;
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Agendamento realizado com sucesso!");
                Console.WriteLine();
                return true;
            }
        }

        public void CancelaAgendamento()
        {
            Console.Clear();
            Console.Write("CPF: ");
            var cpf = Console.ReadLine();

            if (pacientes.Count > 0)
            {
                pacientes.ForEach(p =>
                {
                    if (p.Cpf != cpf)
                    {
                        Console.WriteLine("Erro: paciente não cadastrado");
                        Console.ReadKey();
                    }
                });
            }
            else
            {
                Console.WriteLine("Erro: paciente não cadastrado");
                Console.ReadKey();
            }

            Console.Write("Data da Consulta: ");
            var dataConsulta = Console.ReadLine();

            Console.Write("Hora inicial: ");
            var horaInicial = Console.ReadLine();

            consultas.ForEach(c =>
            {
                if (c.Cpf.Equals(cpf) && c.DataConsulta.Equals(dataConsulta) && c.HoraInicial.Equals(horaInicial))
                {
                    var diaAtual = DateTime.Now.Date;
                    var dataDaConsulta = DateTime.Parse(dataConsulta);
                    var horaAtual = DateTime.Now.ToLocalTime();
                    var horaDaConsulta = Convert.ToDateTime(horaInicial);

                    if (dataDaConsulta > diaAtual || dataDaConsulta == diaAtual && horaDaConsulta > horaAtual)
                    {
                        consultas.Remove(c);
                        Console.WriteLine();
                        Console.WriteLine("Agendamento cancelado com sucesso!");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Erro: agendamento não encontrado");
                }
            });
        }

        public void ListaAgenda()
        {
            Console.Clear();
            Console.WriteLine("Apresentar a agenda T-Toda ou P-Periodo: P");
            char aprensentaAgenda = char.Parse(Console.ReadLine());

            if (aprensentaAgenda.Equals('T') || aprensentaAgenda.Equals('t'))
            {
                var menorData = consultas.Min(d => d.DataConsulta);
                var maiorData = consultas.Max(d => d.DataConsulta);

                ImprimeListaAgenda(menorData, maiorData);
            }
            else if (aprensentaAgenda.Equals('P') || aprensentaAgenda.Equals('p'))
            {
                Console.Write("Data inicial: ");
                string dataInicialConsulta = Console.ReadLine();

                Console.Write("Data final: ");
                string dataFinalConsulta = Console.ReadLine();

                ImprimeListaAgenda(dataInicialConsulta, dataFinalConsulta);
            }
            else
            {
                Console.WriteLine("Opção incorreta, digite novamente");
                Console.ReadKey();
            }
        }

        public void ImprimeListaAgenda(string dataInicialConsulta, string dataFinalConsulta)
        {
            var datasOrdenadas = consultas.OrderBy(d => d.DataConsulta).ToList();

            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Data | H.Ini | H.Fim | Tempo | Nome | Dt.Nasc.");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

            datasOrdenadas.ForEach(d => {
                var dataInicio = DateTime.Parse(dataInicialConsulta);
                var dataFim = DateTime.Parse(dataFinalConsulta);
                var dataConsulta = DateTime.Parse(d.DataConsulta);

                if (dataConsulta >= dataInicio && dataConsulta <= dataFim)
                {
                    var horaInicio = Convert.ToDateTime(d.HoraInicial);
                    var horaFim = Convert.ToDateTime(d.HoraFinal);
                    var tempo = horaFim.Subtract(horaInicio);

                    pacientes.ForEach(p => {
                        if (p.Cpf.Equals(d.Cpf))
                        {
                            Console.WriteLine($"{d.DataConsulta} | {d.HoraInicial} | {d.HoraFinal} | {tempo} | {p.Nome} | {p.DataNascimento}");
                        }
                    });
                }
            });
        }
    }
}

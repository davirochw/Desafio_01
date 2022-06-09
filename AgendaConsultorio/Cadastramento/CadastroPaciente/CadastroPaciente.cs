using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaConsultorio.Validacao;

namespace AgendaConsultorio.CadastroPaciente
{
    public class CadastroPaciente : Cadastramento.Cadastramento
    {
        public CadastroPaciente()
        {

        }

        public void CadastraPaciente()
        {
            while (true)
            {
                Console.Clear();
                AdicionaPaciente();
            }
        }

        /// <summary>
        /// Esse método privado adiciona um paciente 
        /// </summary>
        private void AdicionaPaciente()
        {
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            while (!ValidaCPF(cpf))
            {
                Console.Write("CPF: ");
                cpf = Console.ReadLine();
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            while (!ValidaNome(nome))
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine();
            }

            Console.Write("Data de Nascimento: ");
            string dataNascimento = Console.ReadLine();

            while (!ValidaDataNascimento(dataNascimento))
            {
                Console.Write("Data de Nascimento: ");
                dataNascimento = Console.ReadLine();
            }

            paciente = new Paciente(cpf, nome, dataNascimento);

            if (VerificaPaciente(cpf))
            {
                Console.WriteLine();
                Console.WriteLine("Cadastro realizado com sucesso!");
                pacientes.Add(paciente);
            }
            else
            {
                Console.ReadKey();
                return;
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Esse método verifica se o cpf já existe no cadastro no sistema
        /// </summary>
        private bool VerificaPaciente(string cpf)
        {
            try
            {
                foreach (var p in pacientes)
                {
                    if (p.Cpf.Equals(cpf))
                    {
                        Console.WriteLine("Paciente já está cadastrado!");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Esse método remove o paciente se o cpf for encontrado e caso ele não tenha uma consulta futura
        /// </summary>
        public void RemovePaciente()
        {
            Console.Clear();
            Console.Write("CPF: ");
            var cpf = Console.ReadLine();

            if (pacientes.Count > 0)
            {
                pacientes.ForEach(p => {
                    consultas.ForEach(c => {
                        var dataConsultada = DateTime.Parse(c.DataConsulta);
                        var dataAtual = DateTime.Now;

                        if (p.Cpf.Equals(cpf) && c.Cpf.Equals(cpf))
                        {
                            if (dataConsultada >= dataAtual)
                            {
                                Console.WriteLine($"Erro: paciente está agendado para {c.DataConsulta} as {c.HoraInicial}h.");
                            }
                            else
                            {
                                pacientes.Remove(p);
                                consultas.Remove(c);
                                Console.WriteLine();
                                Console.WriteLine("Paciente removido com sucesso!");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Erro: paciente não cadastrado");
                            Console.ReadKey();
                        }
                    });
                });
            }
        }

        /// <summary>
        /// Esse método lista os pacientes cadastrados por cpf, e se o paciente estiver alguma consulta futura,
        /// ele não será exibida na lista.
        /// </summary>
        public void ListaPacientesPorCPF()
        {
            Console.Clear();

            var cpfsOrdenados = pacientes.OrderBy(p => p.Cpf).ToList();

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("| CPF | Nome | Data de Nascimento |");

            cpfsOrdenados.ForEach(cOrd => {
                consultas.ForEach(c => {
                    var idade = IdadeDoPaciente(cOrd.DataNascimento);
                    Console.WriteLine($"| {cOrd.Cpf} | {cOrd.Nome} | {cOrd.DataNascimento} | {idade} |");

                    if (cOrd.Cpf.Equals(c.Cpf))
                    {
                        Console.WriteLine($"Agendado para: | {c.DataConsulta}\n | {c.HoraInicial} |");
                    }
                });
            });
            Console.WriteLine("------------------------------------------------------");
        }

        /// <summary>
        /// Esse método lista os pacientes cadastrados por nome, e se o paciente estiver alguma consulta futura,
        /// ele não será exibida na lista.
        /// </summary>
        public void ListaPacientesPorNome()
        {
            Console.Clear();

            var nomesOrdenados = pacientes.OrderBy(p => p.Nome).ToList();

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("| CPF | Nome | Data de Nascimento |");

            nomesOrdenados.ForEach(nOrd => {
                consultas.ForEach(c => {
                    var idade = IdadeDoPaciente(nOrd.DataNascimento);
                    Console.WriteLine($"| {nOrd.Cpf} | {nOrd.Nome} | {nOrd.DataNascimento} | {idade} |");

                    if (nOrd.Cpf.Equals(c.Cpf))
                    {
                        Console.WriteLine($"Agendado para: | {c.DataConsulta}\n | {c.HoraInicial} |");
                    }
                });
            });
            Console.WriteLine("------------------------------------------------------");
        }

        /// <summary>
        /// Esse método calcula a idade do paciente, e retorna para exibir na lista
        /// </summary>
        private int IdadeDoPaciente(string data)
        {
            var anoAtual = DateTime.Now.Year;
            var anoNascimento = DateTime.Parse(data).Year;
            var idade = anoAtual - anoNascimento;

            return idade;
        }

    }
}
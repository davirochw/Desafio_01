using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsultorio.Validacao
{
    public class Validacao
    {

        /// <summary>
        /// Esse método converte a hora da classe <class>Consulta</class> para o formato de hora do sistema 
        /// Caso ela não seja convertida, o programa retorno "-1" ou conversaoDeHoraInvalida;
        /// Caso seja convertida, mas não atende os requisitos do intervalo de horas do funcionamento do consultorio,
        /// e nem o intervalo de 15min, o programa retorna "0" ou intervaloDeHoraInvalida;
        /// </summary>
        private string ConverteHora(string horaInicial, string horaFinal)
        {
            try
            {
                var horaInicio = Convert.ToDateTime(horaInicial);
                var horaFim = Convert.ToDateTime(horaFinal);
                var horaAbreConsultorio = Convert.ToDateTime(0900);
                var horaFechaConsultorio = Convert.ToDateTime(1900);

                var inicio = (horaInicio >= horaAbreConsultorio && horaInicio < horaFim);
                var fim = (horaFim > horaInicio && horaFim <= horaFechaConsultorio);

                if ((inicio && fim) && (horaInicio.Minute % 15 == 0 && horaFim.Minute % 15 == 0))
                {
                    var horaCalculada = horaFim - horaInicio;
                    return horaCalculada.ToString();
                }
                var horaIncorreta = "0";
                return horaIncorreta;
            }
            catch
            {
                var conversaoDeHoraInvalida = "-1";
                return conversaoDeHoraInvalida;
            }
        }

        /// <summary>
        /// Esse método valida a data da consulta da classe <class>Consulta</class>,
        /// e a data de nascimento da classe <class>Paciente</class>
        /// caso seja valida, o programa retorna true, caso contrario, o programa retorna false;
        /// </summary>
        public bool ValidaData(string dataCompleta)
        {
            try
            {
                var data = Convert.ToDateTime(dataCompleta);
                return true;
            }
            catch
            {
                Console.WriteLine("Data inválida");
                return false;
            }
        }

        /// <summary>
        /// Esse método verifica primeiro se a data é valida, depois converte a hora inicial e final
        /// para o formato de hora do sistema, e depois verifica se a data é uma data futura para poder agendar, 
        /// e se a hora inicial é menor que a hora final.
        /// Caso seja valida, o programa retorna true, caso contrario, o programa retorna false;
        /// </summary>
        public bool ValidaDataConsulta(string data, string horaInicial, string horaFinal)
        {
            if (ValidaData(data))
            {
                var diaAtual = DateTime.Now.Day;
                var diaConsulta = Convert.ToDateTime(data).Day;
                var qtdDias = diaAtual - diaConsulta;

                var hora = int.Parse(ConverteHora(horaInicial, horaFinal));

                if (qtdDias > 0 || qtdDias == 0 && hora > 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Hora incorreta");
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Esse método verifica se a data de nascimento é valida, e depois verifica se a idade do paciente
        /// é menor que 13 anos.
        /// </summary>
        public bool ValidaDataNascimento(string dataNascimento)
        {
            if (ValidaData(dataNascimento))
            {
                var anoAtual = DateTime.Now.Year;
                var anoNascimento = Convert.ToDateTime(dataNascimento).Year;
                var idadePaciente = anoAtual - anoNascimento;

                if (idadePaciente < 13)
                {
                    Console.WriteLine($"Erro: paciente só tem {idadePaciente} ano(s).");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Esse método verifica se o nome do paciente tem mais de 5 caracteres
        /// </summary>
        public bool ValidaNome(string nome)
        {
            if (nome.Length >= 5)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Nome inválido");
                return false;
            }
        }


        /// <summary>
        /// Esse método verifica se o cpf é válido
        /// </summary>
        public bool ValidaCPF(string cpf)
        {
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                Console.WriteLine("CPF precisa ter exatamente 11 números");
                return false;
            }
            try
            {
                var cpfLong = long.Parse(cpf);
                if (cpfLong.Equals(00000000000) || cpfLong.Equals(11111111111) || cpfLong.Equals(22222222222)
                    || cpfLong.Equals(33333333333) || cpfLong.Equals(44444444444) || cpfLong.Equals(55555555555)
                    || cpfLong.Equals(66666666666) || cpfLong.Equals(77777777777) || cpfLong.Equals(88888888888)
                    || cpfLong.Equals(99999999999))
                {
                    Console.WriteLine("CPF inválido");
                    return false;
                }
                else
                {
                    int[] v1 = new int[9];
                    int[] v2 = new int[10];
                    int soma = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        v1[i] = int.Parse(cpf[i].ToString());
                        soma += v1[i] * (10 - i);
                    }

                    int resto = soma % 11;
                    int j = 0;

                    if (resto < 2)
                    {
                        j = 0;
                    }
                    else
                    {
                        j = 11 - resto;
                    }

                    soma = 0;

                    for (int i = 0; i < 10; i++)
                    {
                        v2[i] = int.Parse(cpf[i].ToString());
                        soma += v2[i] * (11 - i);
                    }

                    resto = soma % 11;
                    int k = 0;

                    if (resto < 2)
                    {
                        k = 0;
                    }
                    else
                    {
                        k = 11 - resto;
                    }

                    if (j == int.Parse(cpf[9].ToString()) && k == int.Parse(cpf[10].ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("CPF inválido");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

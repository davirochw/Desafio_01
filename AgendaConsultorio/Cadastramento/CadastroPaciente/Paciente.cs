using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaConsultorio.Validacao;

namespace AgendaConsultorio.CadastroPaciente
{
    public class Paciente : Validacao.Validacao
    {
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public string DataNascimento { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="nome"></param>
        /// <param name="dataNascimento"></param>
        public Paciente(string cpf, string nome, string dataNascimento)
        {
            Cpf = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
        }
    }
}

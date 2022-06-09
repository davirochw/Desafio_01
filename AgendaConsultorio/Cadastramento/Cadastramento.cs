using AgendaConsultorio.CadastroConsulta;
using AgendaConsultorio.CadastroPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsultorio.Cadastramento
{
    public class Cadastramento : Validacao.Validacao
    {
        protected List<Consulta> consultas = new List<Consulta>();
        protected Consulta consulta;

        protected List<Paciente> pacientes = new List<Paciente>();
        protected Paciente paciente;

    }
}
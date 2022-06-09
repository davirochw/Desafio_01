using AgendaConsultorio.CadastroPaciente;
using AgendaConsultorio.CadastroConsulta;

public class Program
{

    static void Main(string[] args)
    {
        while (true)
        {
            MenuPrincipal();
        }
    }

    /// <summary>
    /// Menu Principal
    /// </summary>
    public static void MenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("Menu Principal\n" +
            "1 - Cadastro de Paciente\n" +
            "2 - Agenda\n" +
            "3 - Fim\n");

        int opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                MenuCadastroPaciente();
                break;
            case 2:
                MenuAgenda();
                break;
            case 3:
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
    }

    /// <summary>
    /// Menu Cadastro de Paciente
    /// </summary>
    public static void MenuCadastroPaciente()
    {
        Console.Clear();
        Console.WriteLine("Menu Cadastro de Pacientes\n" +
            "1 - Cadastrar novo paciente\n" +
            "2 - Excluir paciente\n" +
            "3 - Listar pacientes(ordenado por CPF)\n" +
            "4 - Listar pacientes(ordenado por nome)\n" +
            "5 - Voltar p / menu principal\n");

        int opcao = int.Parse(Console.ReadLine());
        var cadastroPaciente = new CadastroPaciente();

        switch (opcao)
        {
            case 1:
                cadastroPaciente.CadastraPaciente();
                break;
            case 2:
                cadastroPaciente.RemovePaciente();
                break;
            case 3:
                cadastroPaciente.ListaPacientesPorCPF();
                break;
            case 4:
                cadastroPaciente.ListaPacientesPorNome();
                break;
            case 5:
                MenuPrincipal();
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
    }

    /// <summary>
    /// Menu Agenda
    /// </summary>
    public static void MenuAgenda()
    {
        Console.Clear();
        Console.WriteLine("Agenda\n" +
            "1 - Agendar consulta\n" +
            "2 - Cancelar agendamento\n" +
            "3 - Listar agenda\n" +
            "4 - Voltar p/ menu principal\n");

        int opcao = int.Parse(Console.ReadLine());
        var cadastroConsulta = new CadastroConsulta();

        switch (opcao)
        {
            case 1:
                cadastroConsulta.CadastraConsulta();
                break;
            case 2:
                cadastroConsulta.CancelaAgendamento();
                break;
            case 3:
                cadastroConsulta.ListaAgenda();
                break;
            case 4:
                MenuPrincipal();
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }
    }
}
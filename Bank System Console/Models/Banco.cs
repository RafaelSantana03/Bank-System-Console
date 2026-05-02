using Bank_System_Console.Enums;
using System.Text.Json;

namespace Bank_System_Console.Models;

public class Banco
{
    private List<Conta> _contas = new(); // lista de contas do banco, inicializada como vazia
    private readonly string _caminhoArquivo = "contas.json"; // caminho do arquivo para persistência dos dados
    public Banco() // construtor para receber a lista de contas pré definidas
    {
        CarregarDados(); // carrega os dados do arquivo JSON ao iniciar o banco
    }

    public void CriarConta(Conta conta)
    {
        if (_contas.Any(c => c.NumeroDaConta == conta.NumeroDaConta)) // verifica se já existe uma conta com o mesmo número
        {
            throw new ArgumentException("Essa conta já existe!");
        }
        _contas.Add(conta);
        SalvarContas();
    }
    public Conta BuscarConta(int numeroDaConta) // método para buscar uma conta pelo número da conta, usado para validar se a conta existe antes de realizar operações
    {
        var conta = _contas.FirstOrDefault(c => c.NumeroDaConta == numeroDaConta);
        if (conta == null)
            throw new ArgumentException("Conta não encontrada!");
        return conta;
    }

    public void Depositar(decimal valor, int numeroDaConta) // método para realizar um depósito em uma conta, recebe o valor e o número da conta
    {
       Conta conta = BuscarConta(numeroDaConta);
       conta.Depositar(valor);
       SalvarContas();
    }

    public void Sacar(decimal valor, int numeroDaConta) // método para realizar um saque em uma conta, recebe o valor e o número da conta
    {
        Conta conta = BuscarConta(numeroDaConta);
        conta.Sacar(valor);
        SalvarContas();
    }

    public decimal ObterSaldo(int numeroDaConta) // método para obter o saldo de uma conta, recebe o número da conta
    {
        Conta conta = BuscarConta(numeroDaConta);
        return conta.Saldo;
    }

    public void Transferir(decimal valor, int numeroDaContaOrigem, int numeroDaContaDestino) // método para realizar uma transferência entre contas, recebe o valor, o número da conta de origem e o número da conta de destino
    {
        Conta contaOrigem = BuscarConta(numeroDaContaOrigem);
        Conta contaDestino = BuscarConta(numeroDaContaDestino);
        if(numeroDaContaOrigem == numeroDaContaDestino) // validação para garantir que a conta de origem e destino sejam diferentes
        {
            throw new ArgumentException("Não é possível transferir para a mesma conta.");
        }
        contaOrigem.Sacar(valor); // realiza o saque na conta de origem
        contaDestino.Depositar(valor); // realiza o depósito na conta de destino
        SalvarContas();
    }   

    private void SalvarContas() // método para salvar as contas no arquivo JSON, chamado após cada operação que altera o estado das contas
    {
        string json = JsonSerializer.Serialize(_contas);
        File.WriteAllText(_caminhoArquivo, json);
    }
    private void CarregarDados() // método para carregar as contas do arquivo JSON, chamado no construtor para garantir que os dados sejam carregados ao iniciar o programa
    {
        if (!File.Exists(_caminhoArquivo))
            return;
       
        var json = File.ReadAllText(_caminhoArquivo);
        _contas = JsonSerializer.Deserialize<List<Conta>>(json) ?? new List<Conta>(); // desserializa os dados do arquivo para a lista de contas, se o arquivo estiver vazio ou com dados inválidos, inicializa como uma lista vazia

    }

}

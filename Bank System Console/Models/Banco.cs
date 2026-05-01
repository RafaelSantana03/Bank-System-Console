using Bank_System_Console.Enums;

namespace Bank_System_Console.Models;

public class Banco
{
    private readonly List<Conta> _contas = new(); // lista de contas do banco, inicializada como vazia
    public Banco(List<Conta> contas) // construtor para receber a lista de contas pré definidas
    {
        _contas = contas;
    }
    public void CriarConta(Conta conta)
    {
        if (_contas.Any(c => c.NumeroDaConta == conta.NumeroDaConta)) // verifica se já existe uma conta com o mesmo número
        {
            throw new ArgumentException("Essa conta já existe!");
        }
        _contas.Add(conta);
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
    }

    public void Sacar(decimal valor, int numeroDaConta) // método para realizar um saque em uma conta, recebe o valor e o número da conta
    {
        Conta conta = BuscarConta(numeroDaConta);
        conta.Sacar(valor);
    }

    public void ObterSaldo(int numeroDaConta) // método para obter o saldo de uma conta, recebe o número da conta
    {
        Conta conta = BuscarConta(numeroDaConta);
        Console.WriteLine($"Saldo atual: {conta.Saldo:C} da conta: {conta.NumeroDaConta}");
    }

}

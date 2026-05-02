using Bank_System_Console.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Bank_System_Console.Models;

public class Conta
{

    public Conta(int idCliente, int numeroDaConta, TipoDeConta tipoDeConta)
    {
        IdCliente = idCliente;
        NumeroDaConta = numeroDaConta;
        Tipo = tipoDeConta;
    }
    public Conta()  // necessário para o JsonSerializer conseguir desserializar
    { }
    public int IdCliente { get; set; }
    public decimal Saldo { get; private set; } // temporariamente público para serialização
    public int NumeroDaConta { get; set; }

    public List<Transacao> Extrato { get; private set; } = new();
    public TipoDeConta Tipo { get; set; }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor de depósito deve ser positivo.");

        Saldo += valor;
        Extrato.Add(new Transacao /* detalhes da transação */
        {
            Data = DateTime.Now,
            Tipo = "Depósito",
            Valor = valor
        });
    }

    public void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor de saque deve ser positivo");
        if (valor > Saldo)
            throw new InvalidOperationException("Saldo insuficiente");

        Saldo -= valor; // Atualiza o saldo
        Extrato.Add(new Transacao /* detalhes da transação */
        {
            Data = DateTime.Now,
            Tipo = "Saque",
            Valor = valor
        });
    }

    // criar um override do método ToString para exibir as informações da conta
    public override string ToString()
    {
        return $"Número da Conta: {NumeroDaConta}, Tipo: {Tipo}, Saldo: {Saldo:C}";
    }
}


using Bank_System_Console.Enums;

namespace Bank_System_Console.Models;

public class Conta
{
    public int IdCliente { get; set; }
    public decimal Saldo { get; private set; }
    public int NumeroDaConta { get; set; }

    public List<Transacao>? Extrato { get; set; } = new List<Transacao>();
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
        if (valor > Saldo )
            throw new InvalidOperationException("Saldo insuficiente");

        Saldo -= valor; // Atualiza o saldo
        Extrato.Add(new Transacao /* detalhes da transação */
        {
            Data = DateTime.Now,
            Tipo = "Saque",
            Valor = valor
        });
    }
}

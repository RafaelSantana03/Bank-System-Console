using Bank_System_Console.Enums;
using Bank_System_Console.Models;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Bank_System_Console;

public class Program
{
    public static int LerInt(string mensagem)
    {
        int numero;
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out numero))
                return numero;
            Console.WriteLine("Número inválido. Por favor, digite um número inteiro.");
        }
    }
    public static decimal LerDecimal(string mensagem)
    {
        decimal valorDecimal;
        while (true)
        {
            Console.Write(mensagem);
            if (decimal.TryParse(Console.ReadLine(), out valorDecimal))
                return valorDecimal;
            Console.WriteLine("Valor inválido. Por favor, digite um número decimal.");
        }
    }
    static void Main(string[] args)
    {
        // contas pré definida para teste
        List<Conta> contas = new()
        {
         new Conta(1, 12345, TipoDeConta.Corrente),
         new Conta(2, 54321, TipoDeConta.Corrente),
         new Conta(3, 67890, TipoDeConta.Poupanca)
        };

        Banco banco = new Banco(contas); // instanciando o banco com as contas pré definidas

        contas.First(c => c.NumeroDaConta == 12345).Depositar(1000); // depositando um valor para teste

        while (true)
        {
            // Menu de opções para o usuário com switch case para cada opção
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n## Escolha uma opção: ##");
            Console.WriteLine("\n--- 1. Criar conta ---");
            Console.WriteLine("\n--- 2. Depositar   ---");
            Console.WriteLine("\n--- 3. Sacar       ---");
            Console.WriteLine("\n--- 4. Extrato     ---");
            Console.WriteLine("\n--- 5. Ver Saldo   ---");
            Console.WriteLine("\n--- 6. Transferir  ---");
            Console.WriteLine("\n--- 7. Sair        ---");
            Console.ResetColor();

            // Lógica para ler a opção do usuário e chamar os métodos correspondentes
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.WriteLine("\n--- Criar conta ---");
                    Console.Write("Digite o ID do cliente: ");
                    var idCliente = int.Parse(Console.ReadLine()!);
                    try
                    {
                        var numeroDaConta = LerInt("Digite o número da conta: (O número deve ter no máximo 5 dígitos) ");
                        if (numeroDaConta > 99999)
                            throw new ArgumentException("Número da conta deve ter no máximo 5 dígitos.");

                        Console.WriteLine("Digite o tipo de conta (1 - Corrente, 2 - Poupança): ");
                        var tipoDeConta = (TipoDeConta)int.Parse(Console.ReadLine()!);

                        Conta NovaConta = new Conta(idCliente, numeroDaConta, tipoDeConta);
                        banco.CriarConta(NovaConta); // Criando a conta e adicionando ao banco
                        Console.WriteLine($"Conta criada: {NovaConta.ToString()}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "2":
                    Console.WriteLine("\n--- Depositar ---");

                    try
                    {                       
                        var numeroContaDeposito = LerInt("Digite o número da conta: ");
                        banco.BuscarConta(numeroContaDeposito);

                        var valorDeposito = LerDecimal("Digite o valor para depositar:");
                        banco.Depositar(valorDeposito, numeroContaDeposito);

                        Console.WriteLine($"Depósito de {valorDeposito:C} realizado com sucesso na conta: {numeroContaDeposito}.");
                        banco.ObterSaldo(numeroContaDeposito);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "3":
                    Console.WriteLine("\n--- Sacar ---");
                    try
                    {
                        var numeroContaSaque = LerInt("Digite o número da conta: ");
                        banco.BuscarConta(numeroContaSaque);

                        var valorSaque = LerDecimal("Digite o valor para sacar: ");
                        banco.Sacar(valorSaque, numeroContaSaque);

                        Console.WriteLine($"Saque de {valorSaque:C} realizado com sucesso na conta: {numeroContaSaque}");
                        banco.ObterSaldo(numeroContaSaque);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    break;

                case "4":
                    Console.WriteLine("\n--- Extrato ---");
                    try
                    {
                        var numeroContaExtrato = LerInt("Digite o número da conta: ");
                        Conta contaExtrato = banco.BuscarConta(numeroContaExtrato);

                        Console.WriteLine($"Extrato da conta {contaExtrato.NumeroDaConta}:");
                        if (contaExtrato.Extrato.Count == 0)
                        {
                            Console.WriteLine("Nenhuma transação encontrada.");
                            break;
                        }
                        foreach (var transacao in contaExtrato.Extrato)
                        {
                            Console.WriteLine($"{transacao.Data}: {transacao.Tipo} de {transacao.Valor:C}");
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "5":
                    Console.WriteLine("\n--- Saldo ---");
                    try
                    {
                        var numeroContaSaldo = LerInt("Digite o número da conta: ");
                        decimal saldo = banco.ObterSaldo(numeroContaSaldo);

                        Console.WriteLine($"Saldo atual: {saldo:C} da conta: {numeroContaSaldo}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "6":
                    Console.WriteLine("\n--- Transferir ---");
                    try
                    {
                        var numeroContaOrigem = LerInt("Digite o número da conta Origem: ");

                        var numeroContaDestino = LerInt("Digite o número da conta de destino: ");

                        var valorTransferencia = LerDecimal("Informe o valor da transferência: ");
                        banco.Transferir(valorTransferencia, numeroContaOrigem, numeroContaDestino);

                        Console.WriteLine($"Transferência de {valorTransferencia:C} realizada com sucesso da conta: {numeroContaOrigem} para a conta: {numeroContaDestino}");

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }   
                    break;

                case "7":
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default:
                    Console.WriteLine("Escolha uma opção válida");
                    break;
            }
        }
    }
}

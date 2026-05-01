using Bank_System_Console.Enums;
using Bank_System_Console.Models;

namespace Bank_System_Console;

public class Program
{
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
                    Console.WriteLine("Digite o ID do cliente: ");
                    var idCliente = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine("Digite o número da conta: (O número deve ter no máximo 5 dígitos)");
                        var numeroDaConta = int.Parse(Console.ReadLine()!);
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
                        Console.WriteLine("Digite o número da conta:");
                        var numeroContaDeposito = int.Parse(Console.ReadLine()!);
                        banco.BuscarConta(numeroContaDeposito);

                        Console.WriteLine("Digite o valor para depositar:");
                        var valorDeposito = decimal.Parse(Console.ReadLine()!);
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
                        Console.WriteLine("Digite o número da conta:");
                        var numeroContaSaque = int.Parse(Console.ReadLine()!);
                        banco.BuscarConta(numeroContaSaque);

                        Console.WriteLine("Digite o valor para sacar:");
                        var valorSaque = decimal.Parse(Console.ReadLine()!);
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
                        Console.WriteLine("Digite o número da conta:");
                        var numeroContaExtrato = int.Parse(Console.ReadLine()!);
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
                        Console.WriteLine("Digite o número da conta:");
                        var numeroContaSaldo = int.Parse(Console.ReadLine()!);
                        Conta contaSaldo = banco.BuscarConta(numeroContaSaldo);

                        banco.ObterSaldo(numeroContaSaldo);
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
                        Console.WriteLine("Digite o número da conta de origem:");
                        var numeroContaOrigem = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Digite o número da conta de destino:");
                        var numeroContaDestino = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Informe o valor da transferência:");
                        var valorTransferencia = decimal.Parse(Console.ReadLine()!);
                        banco.Transferir(valorTransferencia, numeroContaOrigem, numeroContaDestino);

                    }
                    catch(ArgumentException ex)
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

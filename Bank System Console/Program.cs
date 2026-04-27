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
        // depositando um valor inicial para teste
        contas.First(c => c.NumeroDaConta == 12345).Depositar(1000);

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
            Console.WriteLine("\n--- 6. Sair        ---");
            Console.ResetColor();

            // Lógica para ler a opção do usuário e chamar os métodos correspondentes
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.WriteLine("\n--- Criar conta ---");
                    Console.WriteLine("Digite o ID do cliente: ");
                    var idCliente = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Digite o número da conta: (O número deve ter no máximo 5 dígitos)");
                    var numeroDaConta = int.Parse(Console.ReadLine()!);
                    if (numeroDaConta > 99999)
                    {
                        Console.WriteLine("Número da conta inválido. O número deve ter no máximo 5 dígitos.");
                        return;
                    }
                    Console.WriteLine("Digite o tipo de conta (1 - Corrente, 2 - Poupança): ");
                    var tipoDeConta = (TipoDeConta)int.Parse(Console.ReadLine()!);
                    Conta NovaConta = new Conta(idCliente, numeroDaConta, tipoDeConta);
                    contas.Add(NovaConta);
                    Console.WriteLine($"Conta criada: {NovaConta.ToString()}");
                    break;

                case "2":
                    Console.WriteLine("\n--- Depositar ---");

                    Console.WriteLine("Digite o número da conta:");
                    var numeroContaDeposito = int.Parse(Console.ReadLine()!);

                    Conta contaDeposito = contas.First(c => c.NumeroDaConta == numeroContaDeposito);
                    if (contaDeposito == null)
                    {
                        Console.WriteLine("Conta não encontrada.");
                        break;
                    }

                    Console.WriteLine("Digite o valor para depositar:");
                    var valorDeposito = decimal.Parse(Console.ReadLine()!);
                    if (valorDeposito <= 0)
                    {
                        Console.WriteLine("Valor de depósito inválido. O valor deve ser positivo.");
                        break;
                    }
                    contaDeposito.Depositar(valorDeposito);
                    Console.WriteLine($"Depósito de {valorDeposito:C} realizado com sucesso na conta: {contaDeposito.NumeroDaConta}.");
                    break;

                case "3":
                    Console.WriteLine("\n--- Sacar ---");

                    Console.WriteLine("Digite o número da conta:");
                    var numeroContaSaque = int.Parse(Console.ReadLine()!);
                    Conta contaParaSaque = contas.First(x => x.NumeroDaConta == numeroContaSaque);
                    if (contaParaSaque is null)
                    {
                        Console.WriteLine("Conta não encontrada.");
                        break;
                    }
                    Console.WriteLine("Digite o valor para sacar:");
                    var valorSaque = decimal.Parse(Console.ReadLine()!);
                    if (valorSaque <= 0)
                    {
                        Console.WriteLine("Valor de saque inválido. O valor deve ser positivo.");
                        break;
                    }
                    if (contaParaSaque.Saldo < valorSaque)
                    {
                        Console.WriteLine("Saldo insuficiente para realizar o saque.");
                        break;
                    }
                    contaParaSaque.Sacar(valorSaque);
                    Console.WriteLine($"Saque de {valorSaque:C} realizado com sucesso na conta: {contaParaSaque.NumeroDaConta}.");
                    break;

                case "4":
                    Console.WriteLine("\n--- Extrato ---");

                    Console.WriteLine("Digite o número da conta:");
                    var numeroContaExtrato = int.Parse(Console.ReadLine()!);
                    Conta contaExtrato = contas.First(x => x.NumeroDaConta == numeroContaExtrato);
                    if (contaExtrato is null)
                    {
                        Console.WriteLine("Conta não encontrada.");
                        break;
                    }
                    Console.WriteLine($"Extrato da conta {contaExtrato.NumeroDaConta}:");
                    if (contaExtrato.Extrato is null || contaExtrato.Extrato.Count == 0)
                    {
                        Console.WriteLine("Nenhuma transação encontrada.");
                        break;
                    }
                    foreach (var transacao in contaExtrato.Extrato)
                    {
                        Console.WriteLine($"{transacao.Data}: {transacao.Tipo} de {transacao.Valor:C}");
                    }
                    break;

                case "5":
                    Console.WriteLine("\n--- Saldo ---");
                    Console.WriteLine("Digite o número da conta:");
                    var numeroContaSaldo = int.Parse(Console.ReadLine()!);

                    Conta contaSaldo = contas.First(x => x.NumeroDaConta == numeroContaSaldo);
                    if (contaSaldo is null)
                    {
                        Console.WriteLine("Conta não encontrada.");
                        break;
                    }
                    Console.WriteLine($"Saldo da conta {contaSaldo.NumeroDaConta}: {contaSaldo.Saldo:C}");
                    break;

                case "6":
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default:
                    Console.WriteLine("Escolha uma opção válida");
                    break;
            }
        }
    }
}

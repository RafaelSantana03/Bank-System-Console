using Bank_System_Console.Enums;
using Bank_System_Console.Models;

namespace Bank_System_Console;

public class Program
{
    static void Main(string[] args)
    {
        // Menu de opções para o usuário com switch case para cada opção
        Console.WriteLine("## Escolha uma opção: ##");
        Console.WriteLine("\n--- 1. Criar conta ---");
        Console.WriteLine("\n--- 2. Depositar   ---");
        Console.WriteLine("\n--- 3. Sacar       ---");
        Console.WriteLine("\n--- 4. Extrato     ---");
        Console.WriteLine("\n--- 5. Ver Saldo   ---");

        // Lógica para ler a opção do usuário e chamar os métodos correspondentes
        var opcao = Console.ReadLine();



        switch (opcao)
        {
            case "1":
                Console.WriteLine("\n--- Criar conta ---");
                Console.WriteLine("Digite o ID do cliente: ");
                var idCliente = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o número da conta: (O número deve ter no máximo 5 dígitos)");
                var numeroDaConta = int.Parse(Console.ReadLine());
                if(numeroDaConta > 99999)
                {
                    Console.WriteLine("Número da conta inválido. O número deve ter no máximo 5 dígitos.");
                    return;
                }
                Console.WriteLine("Digite o tipo de conta (1 - Corrente, 2 - Poupança): ");
                var tipoDeConta = (TipoDeConta)int.Parse(Console.ReadLine());
                Conta conta = new Conta(idCliente, numeroDaConta, tipoDeConta);
                Console.WriteLine($"Conta criada: {conta.ToString()}");
                break;

            //case "2":
            //    Console.WriteLine("Digite o valor para depositar:");
            //    var valorDeposito = decimal.Parse(Console.ReadLine());
            //    if(valorDeposito <= 0)
            //    {
            //        Console.WriteLine("Valor de depósito inválido. O valor deve ser positivo.");
            //        return;
            //    }
            //    conta?.Depositar(valorDeposito);
            //    break;

            default:
                Console.WriteLine("Escolha uma opção válida");
                break;
        }
    }
}

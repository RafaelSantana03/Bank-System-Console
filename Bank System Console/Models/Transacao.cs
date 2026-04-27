
using System.Reflection.Metadata.Ecma335;

namespace Bank_System_Console.Models;

public class Transacao
{
    public DateTime Data { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }

    public override string ToString()
    {
        return $"{Data}: {Tipo} de {Valor:C}";
    }
}

using System;
namespace AulaPOO.Models
{
// Classe representa um Livro
public class Livro
{
// Atributos (propriedades)
public string Titulo { get; set; }
public string Autor { get; set; }
public string ISBN { get; set; }
public int AnoPublicacao { get; set; }
public int Paginas { get; set; }
public bool Disponivel { get; private set; }
// Construtor
public Livro(string titulo, string autor, string isbn, int ano, int paginas)
{
Titulo = titulo;
Autor = autor;
ISBN = isbn;
AnoPublicacao = ano;
Paginas = paginas;
Disponivel = true; // Novo livro sempre disponível
}
// Métodos
public void Emprestar(){
    
if (Disponivel)
{
Disponivel = false;
Console.WriteLine($"Livro '{Titulo}' emprestado com sucesso!");
}
else
{
Console.WriteLine($"Livro '{Titulo}' não está disponível para empréstimo.");
}
}
public void Devolver()
{
if (!Disponivel)
{
Disponivel = true;
Console.WriteLine($"Livro '{Titulo}' devolvido com sucesso!");
}
else
{
Console.WriteLine($"Livro '{Titulo}' já está disponível.");
}
}
public void ExibirInformacoes()
{
Console.WriteLine($"\n--- Informações do Livro ---");
Console.WriteLine($"Título: {Titulo}");
Console.WriteLine($"Autor: {Autor}");
Console.WriteLine($"ISBN: {ISBN}");
Console.WriteLine($"Ano: {AnoPublicacao}");
Console.WriteLine($"Páginas: {Paginas}");
Console.WriteLine($"Status: {(Disponivel ? "Disponível" : "Emprestado")}");
}
}
}
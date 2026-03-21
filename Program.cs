using System;
using System.Collections.Generic;
     // ← Livro, Usuario, UsuarioPremium
using AulaPOO.Models;    // ← Emprestimo, DVD
using AulaPOO.Services;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║  SISTEMA DE BIBLIOTECA - EXERCÍCIOS  ║");
Console.WriteLine("╚══════════════════════════════════════╝\n");

// ── Dados base ──────────────────────────────────────────
var livro1 = new Livro("Dom Casmurro", "Machado de Assis", "978-1", 1899, 336);
var livro2 = new Livro("Quincas Borba", "Machado de Assis", "978-2", 1891, 280);
var livro3 = new Livro("O Cortiço", "Aluísio Azevedo", "978-3", 1890, 256);
var livro4 = new Livro("Iracema", "José de Alencar", "978-4", 1865, 100);

var usuario1 = new Usuario("Ana Silva", 28, "ana@email.com");
var usuario2 = new Usuario("Bruno Costa", 35, "bruno@email.com");
var usuarioPremium = new UsuarioPremium("Carla Dias", 42, "carla@email.com", 0.20m);

// ════════════════════════════════════════════════════════
Console.WriteLine("══ EXERCÍCIO 1: Classe DVD ══");
// ════════════════════════════════════════════════════════

var dvd1 = new DVD("Matrix", "Wachowski", 1999, 136);
var dvd2 = new DVD("Inception", "Christopher Nolan", 2010, 148);

dvd1.ExibirInformacoes();
dvd1.Emprestar(usuario1);
dvd1.Emprestar(usuario2);       // Deve mostrar que está indisponível
dvd1.Devolver(usuario1);
dvd1.ExibirInformacoes();

Console.WriteLine($"\nDVD '{dvd2.Titulo}' disponível? {dvd2.VerificarDisponibilidade()}");

// ════════════════════════════════════════════════════════
Console.WriteLine("\n══ EXERCÍCIO 2: Validação de Data de Devolução ══");
// ════════════════════════════════════════════════════════

// Empréstimo com prazo padrão (14 dias)
var emprestimo1 = new Emprestimo(livro1, usuario1);
emprestimo1.ExibirStatus();

// Empréstimo com data customizada (7 dias)
var emprestimo2 = new Emprestimo(livro2, usuario2, DateTime.Now.AddDays(7));
emprestimo2.ExibirStatus();

// Testando devolução
emprestimo1.RegistrarDevolucao();

// Testando validação de data inválida
try
{
    var emprestimoInvalido = new Emprestimo(livro3, usuario1, DateTime.Now.AddDays(-1));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\n✗ Erro esperado: {ex.Message}");
}

try
{
    var emprestimoLongo = new Emprestimo(livro3, usuario1, DateTime.Now.AddDays(60));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"✗ Erro esperado: {ex.Message}");
}

// ════════════════════════════════════════════════════════
Console.WriteLine("\n══ EXERCÍCIO 3: Busca de Livros ══");
// ════════════════════════════════════════════════════════

var livros = new List<Livro> { livro1, livro2, livro3, livro4 };
var busca = new BibliotecaBusca(livros);

busca.BuscarPorAutor("Machado de Assis");
busca.BuscarPorAutorParcial("Alencar");
busca.BuscarPorTitulo("o");       // Deve retornar Dom Casmurro, O Cortiço
busca.BuscarPorAno(1899);
busca.BuscarDisponiveis();

// ════════════════════════════════════════════════════════
Console.WriteLine("\n══ EXERCÍCIO 4: Sistema de Multas ══");
// ════════════════════════════════════════════════════════

// Calculando multas por tipo de item
int diasAtraso = 5;
decimal multaLivro = SistemaMultas.CalcularMultaLivro(diasAtraso);
SistemaMultas.ExibirDetalheMulta("Livro", diasAtraso, multaLivro);

decimal multaRevista = SistemaMultas.CalcularMultaRevista(diasAtraso);
SistemaMultas.ExibirDetalheMulta("Revista", diasAtraso, multaRevista);

decimal multaDvd = SistemaMultas.CalcularMultaDVD(diasAtraso);
SistemaMultas.ExibirDetalheMulta("DVD", diasAtraso, multaDvd);

// Multa com limite máximo
Console.WriteLine("\n--- Testando multa máxima (50 dias de atraso) ---");
decimal multaMaxima = SistemaMultas.CalcularMultaLivro(50);
SistemaMultas.ExibirDetalheMulta("Livro", 50, multaMaxima);

// Desconto para usuário premium
Console.WriteLine("\n--- Desconto premium (20%) sobre a multa ---");
SistemaMultas.AplicarDescontoPremium(multaLivro, usuarioPremium.Desconto);

// ════════════════════════════════════════════════════════
Console.WriteLine("\n══ EXERCÍCIO 5: Relatório Estatístico ══");
// ════════════════════════════════════════════════════════

// Simula empréstimos para gerar estatísticas
livro1.Emprestar(); // Empresa livro 1
livro2.Emprestar(); // Empresta livro 2
usuario1.AdicionarLivro(livro1);
usuario2.AdicionarLivro(livro2);

var usuarios = new List<Usuario> { usuario1, usuario2, usuarioPremium };
var emprestimos = new List<Emprestimo> { emprestimo1, emprestimo2 };

var relatorio = new BibliotecaRelatorio(livros, usuarios, emprestimos);
relatorio.GerarRelatorioCompleto();

Console.WriteLine("\nExercícios concluídos com sucesso! ✓");
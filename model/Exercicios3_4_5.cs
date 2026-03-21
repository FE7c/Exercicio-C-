using System;
using System.Collections.Generic;
using System.Linq;
using AulaPOO.Models;    // ← único using necessário agora
using AulaPOO.Services;  // onde aplicável      // ← Usuario, Livro 

    /// <summary>
    /// Exercício 3: Métodos de busca de livros por autor (e outros critérios)
    /// </summary>
public class BibliotecaBusca
{
    private List<Livro> _livros;

    public BibliotecaBusca(List<Livro> livros)
    {
        _livros = livros ?? throw new ArgumentNullException(nameof(livros));
    }

    // Busca exata por autor
    public List<Livro> BuscarPorAutor(string autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("Nome do autor não pode ser vazio.");

        var resultados = _livros
            .Where(l => l.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase))
            .ToList();

        ExibirResultados($"Busca por autor: '{autor}'", resultados);
        return resultados;
    }

    // Busca parcial por autor (contém o termo)
    public List<Livro> BuscarPorAutorParcial(string termoAutor)
    {
        var resultados = _livros
            .Where(l => l.Autor.Contains(termoAutor, StringComparison.OrdinalIgnoreCase))
            .ToList();

        ExibirResultados($"Busca parcial por autor: '{termoAutor}'", resultados);
        return resultados;
    }

    // Busca por título
    public List<Livro> BuscarPorTitulo(string titulo)
    {
        var resultados = _livros
            .Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase))
            .ToList();

        ExibirResultados($"Busca por título: '{titulo}'", resultados);
        return resultados;
    }

    // Busca por disponibilidade
    public List<Livro> BuscarDisponiveis()
    {
        var resultados = _livros.Where(l => l.Disponivel).ToList();
        ExibirResultados("Livros disponíveis", resultados);
        return resultados;
    }

    // Busca por ano de publicação
    public List<Livro> BuscarPorAno(int ano)
    {
        var resultados = _livros.Where(l => l.AnoPublicacao == ano).ToList();
        ExibirResultados($"Livros do ano {ano}", resultados);
        return resultados;
    }

    private void ExibirResultados(string cabecalho, List<Livro> livros)
    {
        Console.WriteLine($"\n=== {cabecalho} ===");
        if (!livros.Any())
        {
            Console.WriteLine("Nenhum resultado encontrado.");
            return;
        }
        Console.WriteLine($"{livros.Count} resultado(s) encontrado(s):");
        foreach (var livro in livros)
        {
            Console.WriteLine($"  - '{livro.Titulo}' por {livro.Autor} " +
                                $"({livro.AnoPublicacao}) | " +
                                $"{(livro.Disponivel ? "Disponível" : "Emprestado")}");
        }
    }
}

// =========================================================================

/// <summary>
/// Exercício 4: Sistema de multas para devoluções atrasadas
/// </summary>
public static class SistemaMultas
{
    // Multa padrão por dia de atraso
    private const decimal MULTA_DIARIA_LIVRO = 0.50m;
    private const decimal MULTA_DIARIA_REVISTA = 0.25m;
    private const decimal MULTA_DIARIA_DVD = 1.00m;
    private const decimal MULTA_MAXIMA = 20.00m;

    public static decimal CalcularMultaLivro(int diasAtraso)
    {
        if (diasAtraso <= 0) return 0;

        decimal multa = diasAtraso * MULTA_DIARIA_LIVRO;
        return Math.Min(multa, MULTA_MAXIMA);
    }

    public static decimal CalcularMultaRevista(int diasAtraso)
    {
        if (diasAtraso <= 0) return 0;

        decimal multa = diasAtraso * MULTA_DIARIA_REVISTA;
        return Math.Min(multa, MULTA_MAXIMA);
    }

    public static decimal CalcularMultaDVD(int diasAtraso)
    {
        if (diasAtraso <= 0) return 0;

        decimal multa = diasAtraso * MULTA_DIARIA_DVD;
        return Math.Min(multa, MULTA_MAXIMA);
    }

    public static void ExibirDetalheMulta(string tipoItem, int diasAtraso, decimal valorMulta)
    {
        Console.WriteLine($"\n=== MULTA POR ATRASO ===");
        Console.WriteLine($"Item: {tipoItem}");
        Console.WriteLine($"Dias de atraso: {diasAtraso}");
        Console.WriteLine($"Valor da multa: R$ {valorMulta:F2}");

        if (valorMulta >= MULTA_MAXIMA)
            Console.WriteLine($"⚠ Multa limitada ao valor máximo de R$ {MULTA_MAXIMA:F2}");
    }

    public static decimal AplicarDescontoPremium(decimal multa, decimal percentualDesconto)
    {
        if (percentualDesconto < 0 || percentualDesconto > 1)
            throw new ArgumentException("Desconto deve ser entre 0 e 1 (ex: 0.1 = 10%)");

        decimal desconto = multa * percentualDesconto;
        decimal multaFinal = multa - desconto;
        Console.WriteLine($"Desconto premium aplicado: -R$ {desconto:F2}");
        Console.WriteLine($"Valor final da multa: R$ {multaFinal:F2}");
        return multaFinal;
    }
}

// =========================================================================

/// <summary>
/// Exercício 5: Classe BibliotecaRelatorio com métodos estatísticos
/// </summary>
public class BibliotecaRelatorio
{
    private List<Livro> _livros;
    private List<Usuario> _usuarios;
    private List<Emprestimo> _emprestimos;

    public BibliotecaRelatorio(
        List<Livro> livros,
        List<Usuario> usuarios,
        List<Emprestimo> emprestimos)
    {
        _livros = livros ?? new List<Livro>();
        _usuarios = usuarios ?? new List<Usuario>();
        _emprestimos = emprestimos ?? new List<Emprestimo>();
    }

    // Total de livros no acervo
    public int TotalLivros() => _livros.Count;

    // Total de livros disponíveis
    public int TotalLivrosDisponiveis() => _livros.Count(l => l.Disponivel);

    // Total de livros emprestados
    public int TotalLivrosEmprestados() => _livros.Count(l => !l.Disponivel);

    // Taxa de ocupação do acervo (percentual emprestado)
    public double TaxaOcupacao()
    {
        if (!_livros.Any()) return 0;
        return (double)TotalLivrosEmprestados() / TotalLivros() * 100;
    }

    // Autor com mais livros no acervo
    public string AutorMaisPresente()
    {
        if (!_livros.Any()) return "Sem livros cadastrados";
        return _livros
            .GroupBy(l => l.Autor)
            .OrderByDescending(g => g.Count())
            .First().Key;
    }

    // Usuário com mais livros emprestados
    public string UsuarioMaisAtivo()
    {
        if (!_usuarios.Any()) return "Sem usuários cadastrados";
        return _usuarios
            .OrderByDescending(u => u.QuantidadeLivrosEmprestados)
            .First().Nome;
    }

    // Empréstimos em atraso
    public List<Emprestimo> EmprestimosAtrasados()
    {
        return _emprestimos.Where(e => e.EstaAtrasado()).ToList();
    }

    // Multa total a receber
    public decimal MultaTotalPendente()
    {
        return EmprestimosAtrasados()
            .Sum(e => SistemaMultas.CalcularMultaLivro(e.DiasAtraso()));
    }

    // Exibe relatório completo
    public void GerarRelatorioCompleto()
    {
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("       RELATÓRIO DA BIBLIOTECA");
        Console.WriteLine(new string('=', 40));

        Console.WriteLine("\n📚 ACERVO");
        Console.WriteLine($"  Total de livros:       {TotalLivros()}");
        Console.WriteLine($"  Livros disponíveis:    {TotalLivrosDisponiveis()}");
        Console.WriteLine($"  Livros emprestados:    {TotalLivrosEmprestados()}");
        Console.WriteLine($"  Taxa de ocupação:      {TaxaOcupacao():F1}%");
        Console.WriteLine($"  Autor mais presente:   {AutorMaisPresente()}");

        Console.WriteLine("\n👥 USUÁRIOS");
        Console.WriteLine($"  Total de usuários:     {_usuarios.Count}");
        Console.WriteLine($"  Usuário mais ativo:    {UsuarioMaisAtivo()}");

        Console.WriteLine("\n⚠ INADIMPLÊNCIA");
        var atrasados = EmprestimosAtrasados();
        Console.WriteLine($"  Empréstimos atrasados: {atrasados.Count}");
        Console.WriteLine($"  Multas pendentes:      R$ {MultaTotalPendente():F2}");

        if (atrasados.Any())
        {
            Console.WriteLine("\n  Detalhes dos atrasos:");
            foreach (var e in atrasados)
                Console.WriteLine($"    - '{e.Livro.Titulo}' com {e.DiasAtraso()} dia(s) de atraso");
        }

        Console.WriteLine("\n" + new string('=', 40));
    }
}


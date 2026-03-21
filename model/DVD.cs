using System;
using AulaPOO.Interfaces;

namespace AulaPOO.Models
{
    public class DVD : IEmprestavel
    {
        public string Titulo { get; set; } = string.Empty;
        public string Diretor { get; set; } = string.Empty;
        public int AnoLancamento { get; set; }
        public int DuracaoMinutos { get; set; }
        public bool Disponivel { get; private set; }

        private Usuario? _usuarioAtual;

        public DVD(string titulo, string diretor, int anoLancamento, int duracaoMinutos)
        {
            Titulo = titulo;
            Diretor = diretor;
            AnoLancamento = anoLancamento;
            DuracaoMinutos = duracaoMinutos;
            Disponivel = true;
        }

        public void Emprestar(Usuario usuario)
        {
            if (Disponivel)
            {
                Disponivel = false;
                _usuarioAtual = usuario;
                Console.WriteLine($"DVD '{Titulo}' emprestado para {usuario.Nome}");
            }
            else
            {
                Console.WriteLine($"DVD '{Titulo}' não está disponível. " +
                                  $"Emprestado para: {_usuarioAtual?.Nome}");
            }
        }

        public void Devolver(Usuario usuario)
        {
            if (!Disponivel && _usuarioAtual == usuario)
            {
                Disponivel = true;
                _usuarioAtual = null;
                Console.WriteLine($"DVD '{Titulo}' devolvido por {usuario.Nome}"); // ← tudo na mesma linha
            }
            else if (Disponivel)
            {
                Console.WriteLine($"DVD '{Titulo}' já está disponível.");
            }
            else
            {
                Console.WriteLine($"Erro: DVD '{Titulo}' está com outro usuário.");
            }
        }

        public bool VerificarDisponibilidade() => Disponivel;

        public void ExibirInformacoes()
        {
            Console.WriteLine("\n--- DVD ---");
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Diretor: {Diretor}");
            Console.WriteLine($"Ano: {AnoLancamento}");
            Console.WriteLine($"Duração: {DuracaoMinutos} minutos");
            Console.WriteLine($"Status: {(Disponivel ? "Disponível" : $"Emprestado para {_usuarioAtual?.Nome}")}");
        }
    }
}
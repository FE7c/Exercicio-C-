using System;
using AulaPOO.Interfaces;

namespace AulaPOO.Models
{
    public class Revista : IEmprestavel
    {
        public string Titulo { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public int Ano { get; set; }
        public bool Disponivel { get; private set; }

        private Usuario? _usuarioAtual; // ← ? indica que null é válido aqui

        public Revista(string titulo, int edicao, int ano)
        {
            Titulo = titulo;
            Edicao = edicao;
            Ano = ano;
            Disponivel = true;
            // _usuarioAtual não precisa ser inicializado — null é o estado correto
        }

        public void Emprestar(Usuario usuario)
        {
            if (Disponivel)
            {
                Disponivel = false;
                _usuarioAtual = usuario;
                Console.WriteLine($"Revista '{Titulo}' emprestada para {usuario.Nome}");
            }
            else
            {
                Console.WriteLine($"Revista '{Titulo}' não está disponível");
            }
        }

        public void Devolver(Usuario usuario)
        {
            if (!Disponivel && _usuarioAtual == usuario)
            {
                Disponivel = true;
                _usuarioAtual = null; // ← sem warning agora, null é permitido
                Console.WriteLine($"Revista '{Titulo}' devolvida por {usuario.Nome}");
            }
        }

        public bool VerificarDisponibilidade() => Disponivel;

        public void ExibirInformacoes()
        {
            Console.WriteLine("\n--- Revista ---");
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Edição: {Edicao}");
            Console.WriteLine($"Ano: {Ano}");
            string status = Disponivel ? "Disponível" : $"Emprestada para {_usuarioAtual?.Nome}";
            Console.WriteLine($"Status: {status}");
        }
    }
}
using System;
using System.Collections.Generic;
using AulaPOO.Models;    // ← único using necessário agora
using AulaPOO.Services;  // onde aplicável    // namespace de Livro e Usuario

namespace AulaPOO.Services
{
    public class BibliotecaService
    {
        private List<Livro> _livros;
        private List<Usuario> _usuarios;

        public BibliotecaService()
        {
            _livros = new List<Livro>();
            _usuarios = new List<Usuario>();
        }

        public void AdicionarLivro(Livro livro)
        {
            _livros.Add(livro);
            Console.WriteLine($"Livro '{livro.Titulo}' adicionado à biblioteca");
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
            Console.WriteLine($"Usuário '{usuario.Nome}' registrado na biblioteca");
        }

        public void RealizarEmprestimo(string isbn, string usuarioNome)
        {
            var livro = _livros.Find(l => l.ISBN == isbn);
            var usuario = _usuarios.Find(u => u.Nome == usuarioNome);

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado!");
                return;
            }
            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado!");
                return;
            }

            livro.Emprestar();

            if (!livro.Disponivel)
                usuario.AdicionarLivro(livro);
        }

        public void ListarTodosLivros()
        {
            Console.WriteLine("\n=== ACERVO DA BIBLIOTECA ===");
            foreach (var livro in _livros)
                livro.ExibirInformacoes();
        }

        public void ListarUsuarios()
        {
            Console.WriteLine("\n=== USUÁRIOS CADASTRADOS ===");
            foreach (var usuario in _usuarios)
            {
                Console.WriteLine($"- {usuario.Nome} ({usuario.Email})");
                if (usuario is UsuarioPremium premium)
                    Console.WriteLine($"  [Premium] Desconto: {premium.Desconto:P}");
            }
        }
    }
}
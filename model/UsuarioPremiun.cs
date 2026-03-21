using System;
using AulaPOO.Models;    // ← único using necessário agora
using AulaPOO.Services;  // onde aplicável

namespace AulaPOO.Models
{
    public class UsuarioPremium : Usuario
    {
        public DateTime DataExpiracao { get; private set; }
        public decimal Desconto { get; private set; }

        public UsuarioPremium(string nome, int idade, string email, decimal desconto)
            : base(nome, idade, email)
        {
            DataExpiracao = DateTime.Now.AddYears(1);
            Desconto = desconto;
        }

        public void RenovarAssinatura()
        {
            DataExpiracao = DateTime.Now.AddYears(1);
            Console.WriteLine($"Assinatura renovada para {Nome} até {DataExpiracao:dd/MM/yyyy}");
        }

        public new void AdicionarLivro(Livro livro)
        {
            if (QuantidadeLivrosEmprestados < 5)
                base.AdicionarLivro(livro);
            else
                Console.WriteLine($"Usuário premium {Nome} atingiu o limite de 5 livros!");
        }
    }
}
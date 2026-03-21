using System;
using AulaPOO.Models;

namespace AulaPOO.Models
{
    /// <summary>
    /// Exercício 2: Classe Emprestimo com validação de data de devolução
    /// </summary>
    public class Emprestimo
    {
        public Livro Livro { get; private set; }
        public Usuario Usuario { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public bool Devolvido => DataDevolucaoReal.HasValue;

        private const int PRAZO_PADRAO_DIAS = 14;

        public Emprestimo(Livro livro, Usuario usuario, DateTime? dataDevolucao = null)
        {
            Livro = livro ?? throw new ArgumentNullException(nameof(livro));
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            DataEmprestimo = DateTime.Now;

            // Validação da data de devolução prevista
            if (dataDevolucao.HasValue)
            {
                ValidarDataDevolucao(dataDevolucao.Value);
                DataDevolucaoPrevista = dataDevolucao.Value;
            }
            else
            {
                DataDevolucaoPrevista = DataEmprestimo.AddDays(PRAZO_PADRAO_DIAS);
            }
        }

        private void ValidarDataDevolucao(DateTime dataDevolucao)
        {
            if (dataDevolucao <= DateTime.Now)
                throw new ArgumentException("A data de devolução deve ser futura.");

            if (dataDevolucao > DateTime.Now.AddDays(30))
                throw new ArgumentException("O prazo máximo de empréstimo é 30 dias.");
        }

        public void RegistrarDevolucao(DateTime? dataRetorno = null)
        {
            if (Devolvido)
            {
                Console.WriteLine("Este empréstimo já foi devolvido.");
                return;
            }

            DataDevolucaoReal = dataRetorno ?? DateTime.Now;

            if (DataDevolucaoReal > DataDevolucaoPrevista)
            {
                int diasAtraso = (DataDevolucaoReal.Value - DataDevolucaoPrevista).Days;
                Console.WriteLine($"⚠ Devolução com {diasAtraso} dia(s) de atraso!");
            }
            else
            {
                Console.WriteLine($"Livro '{Livro.Titulo}' devolvido dentro do prazo.");
            }
        }

        public bool EstaAtrasado()
        {
            if (Devolvido) return false;
            return DateTime.Now > DataDevolucaoPrevista;
        }

        public int DiasAtraso()
        {
            if (!EstaAtrasado()) return 0;
            return (DateTime.Now - DataDevolucaoPrevista).Days;
        }

        public void ExibirStatus()
        {
            Console.WriteLine($"\n--- Empréstimo ---");
            Console.WriteLine($"Livro: {Livro.Titulo}");
            Console.WriteLine($"Usuário: {Usuario.Nome}");
            Console.WriteLine($"Data do Empréstimo: {DataEmprestimo:dd/MM/yyyy}");
            Console.WriteLine($"Previsão de Devolução: {DataDevolucaoPrevista:dd/MM/yyyy}");
            if (Devolvido)
                Console.WriteLine($"Devolvido em: {DataDevolucaoReal:dd/MM/yyyy}");
            else
                Console.WriteLine($"Status: {(EstaAtrasado() ? $"ATRASADO ({DiasAtraso()} dias)" : "Em dia")}");
        }
    }
}

using AulaPOO.Models;    // ← único using necessário agora
using AulaPOO.Services;  // onde aplicável // ← traz o tipo Usuario

namespace AulaPOO.Interfaces
{
    public interface IEmprestavel
    {
        void Emprestar(Usuario usuario);
        void Devolver(Usuario usuario);
        bool VerificarDisponibilidade();
    }
}
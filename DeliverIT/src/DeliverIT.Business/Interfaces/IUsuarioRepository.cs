using DeliverIT.Business.Models;

namespace DeliverIT.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterUsuarioPorLoginESenha(string username, string senha);
        Usuario GravarNovoUsuario(Usuario usuario);
    }
}

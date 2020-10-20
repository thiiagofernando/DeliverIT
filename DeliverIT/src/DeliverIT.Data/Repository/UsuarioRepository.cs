using DeliverIT.Business.Interfaces;
using DeliverIT.Business.Models;
using DeliverIT.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeliverIT.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DeliverDbContext context) : base(context) { }

        public Usuario GravarNovoUsuario(Usuario usuario)
        {
            Db.usuario.Add(usuario);
            var user = Db.usuario.Find(usuario.Id);
            Db.SaveChanges();
            return user;
        }

        public Usuario ObterUsuarioPorLoginESenha(string username, string senha)
        {
            return  Db.usuario
               .AsNoTracking()
               .FirstOrDefault(p => p.Username == username && p.Password == senha);
        }

        
    }
}

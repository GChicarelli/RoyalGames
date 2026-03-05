using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly RoyalGamesContext _context;

        public UsuarioRepository(RoyalGamesContext contex)
        {
            _context = contex;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario? ObterPorId(int id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObtePorId(int id)
        {
            return _context.Usuario.Find(id);
        }
        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.First(u => u.Email == email);
        }
        public bool EmailExistente(string email)
        {
            return _context.Usuario.Any(u => u.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);
            if (usuarioBanco == null)
            {
                return;
            }
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Usuario? usuario = _context.Usuario.FirstOrDefault(u => u.UsuarioID == id);

            if (usuario == null)
            {
                return;
            }
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
        }
    }
}

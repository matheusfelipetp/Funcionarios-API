using Microsoft.EntityFrameworkCore;
using WebApi_CRUD.Models;

namespace WebApi_CRUD.DataContext
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }
    }
}

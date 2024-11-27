using Microsoft.EntityFrameworkCore;
using WebApi_CRUD.DataContext;
using WebApi_CRUD.Models;

namespace WebApi_CRUD.Services.Funcionario
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                serviceResponse.Dados = await _context.Funcionarios.AsNoTracking().ToListAsync();

                if(serviceResponse.Dados.Count == 0)
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                else
                    serviceResponse.Mensagem = "Funcionários listados com sucesso!";
            } catch(Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            var serviceResponse = new ServiceResponse<FuncionarioModel>();

            try
            {
                var funcionario = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

                if(funcionario is null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                } else
                {
                    serviceResponse.Dados = funcionario;
                    serviceResponse.Mensagem = "Funcionário encontrado com sucesso!";
                }   
            } catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                if(novoFuncionario is null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Dados inválidos!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Funcionarios.Add(novoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário cadastrado com sucesso!";
            } catch(Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel atualizarFuncionario)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionarioAtualizado = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == atualizarFuncionario.Id);

                if (funcionarioAtualizado is null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                funcionarioAtualizado.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(atualizarFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário atualizado com sucesso!";
            } catch(Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionarioDeletado = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionarioDeletado is null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }
               
                _context.Funcionarios.Remove(funcionarioDeletado);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário deletado com sucesso!";
            } catch(Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            var serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionarioInativado = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionarioInativado is null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                }
                else
                {
                    funcionarioInativado.Ativo = false;
                    funcionarioInativado.DataDeAlteracao = DateTime.Now.ToLocalTime();

                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                    serviceResponse.Mensagem = "Funcionário inativado com sucesso!";
                }
            } catch(Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }
    }
}

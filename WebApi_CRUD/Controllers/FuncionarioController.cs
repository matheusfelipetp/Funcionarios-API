using Microsoft.AspNetCore.Mvc;
using WebApi_CRUD.Models;
using WebApi_CRUD.Services.Funcionario;

namespace WebApi_CRUD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioInterface _funcionarioInterface;

        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
            _funcionarioInterface = funcionarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> Get()
        {
            var funcionarios = await _funcionarioInterface.GetFuncionarios();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> GetById(int id)
        {
            var funcionario = await _funcionarioInterface.GetFuncionarioById(id);
            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> Create(FuncionarioModel novoFuncionario)
        {
            var novosFuncionarios = await _funcionarioInterface.CreateFuncionario(novoFuncionario);
            return Ok(novosFuncionarios);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> Update(FuncionarioModel funcionarioAtualizado)
        {
            var funcionario = await _funcionarioInterface.UpdateFuncionario(funcionarioAtualizado);
            return Ok(funcionario);
        }

        [HttpPut("inativar/{id}")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> Inativar(int id)
        {
            var funcionariosAtualizados = await _funcionarioInterface.InativaFuncionario(id);
            return Ok(funcionariosAtualizados);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> Delete(int id)
        {
            var funcionarios = await _funcionarioInterface.DeleteFuncionario(id);
            return Ok(funcionarios);
        }
    }
}

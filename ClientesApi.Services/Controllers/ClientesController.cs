using ClientesApi.Infra.Data.Entities;
using ClientesApi.Infra.Data.Interfaces;
using ClientesApi.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApi.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPost]
        public IActionResult Post(ClientePostRequest request)
        {
            try
            {
                if (_clienteRepository.BuscarPorEmail(request.Email) != null)
                    return StatusCode(422, new { message = "O email informado já está cadastrado." });

                if (_clienteRepository.BuscarPorCpf(request.Cpf) != null)
                    return StatusCode(422, new { message = "O CPF informado já está cadastrado." });

                
                //Verificar a idade do cliente
                DateTime dataAtual = DateTime.Now;
                int idade = dataAtual.Year - request.DataNascimento.Year;
                if (dataAtual.Month < request.DataNascimento.Month || (dataAtual.Month == request.DataNascimento.Month && dataAtual.Day < request.DataNascimento.Day))
                {
                    idade--;
                }

                if (idade < 18)
                {
                    return StatusCode(422, new { message = "Cliente menor de idade!" });
                }

                var cliente = new Cliente()
                {
                    IdCliente = Guid.NewGuid(),
                    Nome = request.Nome,
                    Email = request.Email,
                    Cpf = request.Cpf,
                    DataNascimento = request.DataNascimento
                };

                _clienteRepository.Inserir(cliente);

                return StatusCode(201, new { message = "Cliente cadastrado com sucesso!", cliente });
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [HttpPut]
        public IActionResult Put(ClientePutRequest request)
        {
            try
            {
                var cliente = _clienteRepository.BuscarPorId(request.IdCliente);
                if (cliente == null)
                    return StatusCode(422, new { message = "Cliente não encontrado." });

                cliente.Cpf = request.Cpf;
                cliente.Nome = request.Nome;
                cliente.DataNascimento = request.DataNascimento;
                cliente.Email = request.Email;

                _clienteRepository.Alterar(cliente);

                return StatusCode(200, new { message = "Cliente atualizado com sucesso", cliente });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpDelete("{idCliente}")]
        public IActionResult Delete(Guid idCliente)
        {
            try
            {
                var cliente = _clienteRepository.BuscarPorId(idCliente);
                if (cliente == null)
                    return StatusCode(422, new { message = "Cliente não encontrado." });

                _clienteRepository.Deletar(cliente);

                return StatusCode(200, new { message = "Cliente excluído com sucesso", cliente });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var clientes = _clienteRepository.BuscarTodos();

                return StatusCode(200, clientes);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet("{idCliente}")]
        public IActionResult GetById(Guid idCliente)
        {
            try
            {
                var clientes = _clienteRepository.BuscarPorId(idCliente);

                return StatusCode(200, clientes);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}

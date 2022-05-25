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

                var cliente = new Cliente()
                {
                    IdCliente = Guid.NewGuid(),
                    Nome = request.Nome,
                    Email = request.Email,
                    Cpf = request.Cpf,
                    DataNascimento = request.DataNascimento
                };

                _clienteRepository.Inserir(cliente);

                return StatusCode(201, new { message = "Cliente cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete("{idCliente}")]
        public IActionResult Delete(Guid idCliente)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("{idCliente}")]
        public IActionResult GetById(Guid idCliente)
        {
            return Ok();
        }
    }
}

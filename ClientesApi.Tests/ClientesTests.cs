using ClientesApi.Services.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Text;
using ClientesApi.Tests.Config;
using ClientesApi.Infra.Data.Entities;
using Bogus;
using Bogus.Extensions.Brazil;

namespace ClientesApi.Tests
{
    public class ClientesTests
    {
        private readonly string _endpoint;

        public ClientesTests()
        {
            _endpoint = ApiConfig.GetEndpoint() + "/Clientes";
        }

        [Fact]
        public async Task<ClienteResult> Test_Post_Returns_Ok()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(_endpoint, CreateDadosDeCliente());

            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<ClienteResult>
               (response.Content.ReadAsStringAsync().Result);

            return result;
        }

        [Fact]
        public async Task Test_Put_Returns_OK()
        {
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            var faker = new Faker("pt_BR");

            var request = new ClientePutRequest
            {
                IdCliente = result.cliente.IdCliente,
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                DataNascimento = new System.DateTime(1999, 6, 3, 22, 15, 0),
                Email = faker.Person.Email.ToLower()
            };

            var content = new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(_endpoint, content);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_Delete_Returns_OK()
        {
            var result = await Test_Post_Returns_Ok();
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(_endpoint + "/" + result.cliente.IdCliente);

            response
               .StatusCode
               .Should()
               .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_GetAll_Returns_OK()
        {
            await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(_endpoint);

            var result = JsonConvert.DeserializeObject<List<Cliente>>
               (response.Content.ReadAsStringAsync().Result);

            result.
                Should()
                .NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Test_GetById_Returns_OK()
        {
            var result = await Test_Post_Returns_Ok();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(_endpoint + "/" + result.cliente.IdCliente);

            var resposta = JsonConvert.DeserializeObject<Cliente>
               (response.Content.ReadAsStringAsync().Result);

            resposta
                .Should()
                .NotBeNull();

        }

        private StringContent CreateDadosDeCliente()
        {
            var faker = new Faker("pt_BR");

            var request = new ClientePostRequest()
            {
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                DataNascimento = new System.DateTime(1996, 6, 3, 22, 15, 0),
                Email = faker.Person.Email.ToLower()
            };

            return new StringContent
                (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }

    }
    public class ClienteResult
    {
        public string message { get; set; }
        public Cliente cliente { get; set; }
    }

}
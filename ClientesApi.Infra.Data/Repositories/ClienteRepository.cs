using ClientesApi.Infra.Data.Contexts;
using ClientesApi.Infra.Data.Entities;
using ClientesApi.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApi.Infra.Data.Repositories
{
    public class ClienteRepository: IClienteRepository
    {
        private readonly SqlServerContext _sqlServerContext;

        public ClienteRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Inserir(Cliente entity)
        {
            _sqlServerContext.Cliente.Add(entity);
            _sqlServerContext.SaveChanges();
        }

        public void Alterar(Cliente entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();    
        }

        public void Deletar(Cliente entity)
        {
            _sqlServerContext.Cliente.Remove(entity);
            _sqlServerContext.SaveChanges();
        }

        public List<Cliente> BuscarTodos()
        {
            return _sqlServerContext.Cliente.OrderBy(c => c.Nome).ToList();
        }

        public Cliente BuscarPorId(Guid id)
        {
            return _sqlServerContext.Cliente.Find(id);
        }

        public Cliente BuscarPorEmail(string email)
        {
            return _sqlServerContext.Cliente
                .FirstOrDefault(c => c.Email.Equals(email));
        }
        public Cliente BuscarPorCpf(string cpf)
        {
            return _sqlServerContext.Cliente
                .FirstOrDefault(c => c.Cpf.Equals(cpf));
        }
    }
}

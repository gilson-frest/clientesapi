using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApi.Infra.Data.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity entity);
        void Alterar(TEntity entity);
        void Deletar(TEntity entity);

        List<TEntity> BuscarTodos();
        TEntity BuscarPorId(Guid id);
    }
}

using FCamara.ProjetoTeste.Domain.Contracts.Repository;
using FCamara.ProjetoTeste.Domain.Contracts.Service;
using FCamara.ProjetoTeste.Domain.Models;
using FCamara.ProjetoTeste.Infra.Data;
using FCamara.ProjetoTeste.Infra.Repository;
using FCamara.ProjetoTeste.Service.Services;
using Microsoft.Practices.Unity;

namespace FCamara.ProjetoTeste.Ioc
{
    public class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<AppDataContext, AppDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdutoRepository, ProdutoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProdutoService, ProdutoService>(new HierarchicalLifetimeManager());

            container.RegisterType<Produto, Produto>(new HierarchicalLifetimeManager());
        }
    }
}

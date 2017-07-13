using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommandAndQuery.Commands;
using CommandAndQuery.Queries;
using CommonStuff;
using CommonStuff.Setup.Castle;
using QuerySide.shouldbenuget;
using Storage.Read.Setup.Castle;

namespace QuerySide.Setup.Castle
{
    public class QueryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new CommonInstaller());
            container.Install(new StorageReadInstaller());

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(IDomainEventHandler<>))
                    .WithService.FirstInterface());

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(IQueryHandler<>))
                    .LifestyleTransient()
                    .WithService.AllInterfaces());

            var domainEventProcessor = new DomainEventProcessor(container);
            var serviceLocator = new WindsorServiceLocator(container);
            container.Register(
                Component.For<IDomainEventProcessor>().Instance(domainEventProcessor),
                Component.For<IServiceLocator>().Instance(serviceLocator)
            );
            ServiceLocator.SetServiceLocator(serviceLocator);
        }
    }
}

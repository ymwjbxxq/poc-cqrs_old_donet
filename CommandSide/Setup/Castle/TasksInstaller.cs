using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommandAndQuery.Commands;
using CommonStuff;
using CommonStuff.Setup.Castle;
using Storage.Write.Setup.Castle;

namespace CommandSide.Setup.Castle
{
    public class TasksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new CommonInstaller());
            container.Install(new StorageWriteInstaller());
            
            var serviceLocator = new WindsorServiceLocator(container);
            container.Register(Component.For<IServiceLocator>().Instance(serviceLocator));
            ServiceLocator.SetServiceLocator(serviceLocator);

            container.Register(
              Classes.FromThisAssembly()
                   .BasedOn(typeof(ICommandHandler<>))
                   .WithService.FirstInterface());

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(ICommandHandler<,>))
                    .WithService.FirstInterface());

            container.Register(
                Component.For(typeof(ICommandProcessor))
                    .ImplementedBy(typeof(CommandProcessor))
                    .Named("CommandProcessor"));
        }
    }
}

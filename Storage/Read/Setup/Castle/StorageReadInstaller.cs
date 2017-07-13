using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Storage.Read.Repos;

namespace Storage.Read.Setup.Castle
{
    public class StorageReadInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPocketRepo>().ImplementedBy<PocketRepo>());
        }
    }
}

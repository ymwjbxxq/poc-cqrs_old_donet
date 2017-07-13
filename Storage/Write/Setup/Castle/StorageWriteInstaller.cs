using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Storage.Write.Repos;

namespace Storage.Write.Setup.Castle
{
    public class StorageWriteInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPocketRepo>().ImplementedBy<PocketRepo>());
        }
    }
}

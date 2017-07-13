using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonStuff.Queue;

namespace CommonStuff.Setup.Castle
{
    public class CommonInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMsMqService>().ImplementedBy<MsMqService>());
        }
    }
}

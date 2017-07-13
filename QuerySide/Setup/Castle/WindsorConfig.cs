using Castle.Windsor;
using Castle.Windsor.Installer;

namespace QuerySide.Setup.Castle
{
    public class WindsorConfig
    {
        public static IWindsorContainer Container { get; private set; }

        public static void Boot()
        {
            Container = new WindsorContainer().Install(FromAssembly.This());
        }
    }
}

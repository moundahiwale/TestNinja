using Autofac;
using TestNinja.Services;

namespace TestNinja.Config
{
    public class AutofacRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
        }
    }
}
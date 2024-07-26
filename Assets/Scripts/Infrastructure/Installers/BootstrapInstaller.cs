using FreedLOW.Painting.Infrastructure.Factories;
using Zenject;

namespace FreedLOW.Painting.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<IPrimitiveFactory>()
                .To<PrimitiveFactory>()
                .AsSingle();
        }
    }
}
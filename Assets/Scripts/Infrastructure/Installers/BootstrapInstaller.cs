using FreedLOW.Painting.Infrastructure.Factories;
using FreedLOW.Painting.Infrastructure.Services.Input;
using Zenject;

namespace FreedLOW.Painting.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();

            BindInputService();
        }

        private void BindFactories()
        {
            Container.Bind<IPrimitiveFactory>()
                .To<PrimitiveFactory>()
                .AsSingle();
        }

        private void BindInputService()
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            Container.Bind<IInputService>()
                .To<StandaloneInputService>()
                .AsSingle();
#elif UNITY_IOS || UNITY_ANDROID
            Container.Bind<IInputService>()
                .To<MobileInputService>()
                .AsSingle();
#endif
        }
    }
}
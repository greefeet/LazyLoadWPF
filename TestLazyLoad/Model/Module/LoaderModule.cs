using Ninject.Modules;

namespace TestLazyLoad.Model.Module
{
    public class LoaderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DataStorage>().ToConstant(new DataStorage());
        }
    }
}
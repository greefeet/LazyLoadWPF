using Ninject;
using TestLazyLoad.Model.Module;

namespace TestLazyLoad.Model
{
    public static class IoC
    {
        public static void Setup()
        {
            Kernel.Load<LoaderModule>();
        }
        private static IKernel Kernel { get; set; } = new StandardKernel();
        public static T Get<T>() => Kernel.Get<T>();
    }
}
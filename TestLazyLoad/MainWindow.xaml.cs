using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using TestLazyLoad.Model;
using System.Linq;
using Ninject;
using Ninject.Parameters;
using System;

namespace TestLazyLoad
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = IoC.Get<MainViewModel>();
            InitializeComponent();
        }
    }

    public class MainViewModel : BindableBase
    {
        [Inject]
        public DataStorage Loader { private get;  set; }

        public MainViewModel()
        {
            _Datas = new Lazy<ObservableCollection<DataLazyViewModel>>(
                () => new ObservableCollection<DataLazyViewModel>(from d in Loader.GetData() select IoC.Get<DataLazyViewModel>(new ConstructorArgument("Id", d)))
            , true);
        }
        public ObservableCollection<DataLazyViewModel> Datas => _Datas.Value;
        private readonly Lazy<ObservableCollection<DataLazyViewModel>> _Datas;
    }
    
    public class DataLazyViewModel : BindableBase
    {
        [Inject]
        public DataStorage Loader { private get; set; }


        public string State { get => _State; set => SetProperty(ref _State, value, nameof(State)); }
        string _State = "Loading";


        public int Id { get; private set; }
        private readonly Lazy<ArabeModel> _Model = null;
        public ArabeModel Model => _Model.Value;


        public DataLazyViewModel(int Id)
        {
            this.Id = Id;
            _Model = new Lazy<ArabeModel>(() => 
            {
                var Result = Loader.GetDataAsync(Id).Result;
                State = "Display";
                return Result;
            },true);
        }
    }

    public class ArabeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}

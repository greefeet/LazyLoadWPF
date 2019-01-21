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
            MainViewModel ViewModel = IoC.Get<MainViewModel>();
            DataContext = ViewModel;
            InitializeComponent();

            Loaded += (s, e) => 
            {
                ViewModel.Init();
            };
        }
    }

    public class MainViewModel : BindableBase
    {
        [Inject]
        public DataLoader Loader { private get;  set; }

        public async void Init()
        {
            Loader = new DataLoader();
            var dd = await Loader.GetDataAsync();
            Datas = new ObservableCollection<DataLazyViewModel>(from d in dd select IoC.Get<DataLazyViewModel>(new ConstructorArgument("Id", d)));
        }
        public ObservableCollection<DataLazyViewModel> Datas { get => _Datas; set => SetProperty(ref _Datas,value,nameof(Datas)); }
        ObservableCollection<DataLazyViewModel> _Datas;
    }

    

    

    public class DataLazyViewModel : BindableBase
    {
        [Inject]
        public DataLoader Loader { private get; set; }

        public int Id { get; set; }
        public string Name { get => _Name; set => SetProperty(ref _Name,value,nameof(Name)); }
        string _Name;

        public string State { get => _State; set => SetProperty(ref _State, value, nameof(State)); }
        string _State = "Loading";


        private Lazy<ArabeModel> Model = null;

        public DataLazyViewModel(int Id)
        {
            this.Id = Id;
            State = "Loading";
            Name = "";
            Model = new Lazy<ArabeModel>(() => GetData());
        }

        public ArabeModel GetData()
        {
            // https://www.codeproject.com/Articles/652556/Can-you-explain-Lazy-Loading
            var Result = Loader.GetDataAsync(Id).Result;
            Name = Result.Name;
            return Result;
        }
    }
    public class ArabeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using TestLazyLoad.Model;
using System.Linq;

namespace TestLazyLoad
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainViewModel ViewModel = new MainViewModel();
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
        private DataLoader Loader { get; set; }
        public async void Init()
        {
            Loader = new DataLoader();
            var dd = await Loader.GetDataAsync();

            Datas = new ObservableCollection<DataLazyViewModel>(from d in dd select new DataLazyViewModel(d));
        }
        public ObservableCollection<DataLazyViewModel> Datas { get => _Datas; set => SetProperty(ref _Datas,value,nameof(Datas)); }
        ObservableCollection<DataLazyViewModel> _Datas;
    }

    

    

    public class DataLazyViewModel : BindableBase
    {
        public int Id { get; set; }

        public string State { get => _State; set => SetProperty(ref _State, value, nameof(State)); }
        string _State = "Loading";

        public DataLazyViewModel(int Id)
        {
            this.Id = Id;
            State = "Loading";
            MockLoad();
        }

        public async void MockLoad()
        {
            await Task.Delay(1000);
            State = "Display";
        }
    }
    public class ArabeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}

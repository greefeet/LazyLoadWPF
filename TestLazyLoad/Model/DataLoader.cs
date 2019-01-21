using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLazyLoad.Model
{
    public class DataLoader
    {
        private Dictionary<int, ArabeModel> DataStore { get; set; }
        public DataLoader()
        {
            DataStore = new Dictionary<int, ArabeModel>()
            {
                [1] = new ArabeModel()
                {
                    Id = 1,
                    Name = "Test01",
                    Value = 100
                },
                [2] = new ArabeModel()
                {
                    Id = 2,
                    Name = "Arabe",
                    Value = 90
                },
                [3] = new ArabeModel()
                {
                    Id = 3,
                    Name = "Fedfe",
                    Value = 80
                }
            };
        }
        public async Task<IEnumerable<int>> GetDataAsync()
        {
            await Task.Delay(100);
            return from d in DataStore select d.Value.Id;
        }

        public async Task<ArabeModel> GetDataAsync(int Id)
        {
            await Task.Delay(100);
            return DataStore[Id];
        }
    }
}
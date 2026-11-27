using Products;
using System.Text.Json;
public class ProductRepository : IProductRepository
{

    private readonly string _filePath;

    public ProductRepository(string filePath)
    {
        _filePath = filePath;
    }
    public List<Produto> loadJson()

    {
        if (File.Exists(_filePath))
        {
            string Data = File.ReadAllText(_filePath);
            try
            {
                if (Data == "" || string.IsNullOrEmpty(Data))
                {
                    Console.WriteLine("Data not found! Creating a new List ....");
                    return new List<Produto>();
                }
                Console.WriteLine("Data found! Deserializing.......");
                List<Produto> produtos = JsonSerializer.Deserialize<List<Produto>>(Data) ?? new List<Produto>();
                return produtos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        else
        {
            Console.WriteLine("Data not found!");
            return new List<Produto>();
        }
    }

    public void saveJson(List<Produto> produtos)
    {
        string jsonString = JsonSerializer.Serialize<List<Produto>>(produtos);
        File.WriteAllText("savedProducts.json", jsonString);
    }
}
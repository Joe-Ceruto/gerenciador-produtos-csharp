using Products;
using System.Text.Json;
public class Program
{
    private static bool _isRunning = true;
    static void Main()
    {
        IProductRepository repository = new ProductRepository("savedProducts.json");
        var service = new ProductService(repository);

        while (_isRunning)
        {

            Console.WriteLine("\nWelcome to the management software!");
            Console.WriteLine("");
            var input = service.validationInt("\n 1- Create a product. \n 2- Consult the products. \n 3- Edit a product. \n 4- Delete a product. \n 5- Exit.");
            switch (input)
            {
                case 1:
                    service.Create();
                    break;
                case 2:
                    service.Read();
                    break;
                case 3:
                    int codeUpdate = service.validationInt("Type the product code you want edit :");
                    if (codeUpdate > 0)
                    {
                        Console.WriteLine("Type the new name:");
                        string? inputName = Console.ReadLine();

                        Console.WriteLine("Type the new amount:");
                        string? inputAmount = Console.ReadLine();

                        Console.WriteLine("Type the new price:");
                        string? inputPrice = Console.ReadLine();

                        service.Update(codeUpdate, inputName, inputAmount, inputPrice);
                        break;
                    }
                    Console.WriteLine("This code dont exist:");
                    break;

                case 4:
                    int codeDelete = service.validateId();
                    if (codeDelete > 0)
                    {
                        service.Delete(codeDelete);
                        break;
                    }
                    Console.WriteLine("This code dont exist:");
                    break;

                case 5:
                    Console.WriteLine("Closing aplication ....");
                    _isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;

            }
        }
    }
}
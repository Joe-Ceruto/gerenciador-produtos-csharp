using Products;
using System.Text.Json;
public class Program
{
    public static bool isRunning = true;
    public static int idCounterGlobal = 1;

    static List<Produto> produtos = loadJson();

    static void Main()
    {
        while (isRunning)
        {
            Console.WriteLine("\nWelcome to the management software!");
            Console.WriteLine("\n 1- Create a product. \n 2- Consult the products. \n 3- Edit a product. \n 4- Delete a product. \n 5- Exit.");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                switch (result)
                {
                    case 1:
                        Create();
                        break;
                    case 2:
                        Read();
                        break;
                    case 3:
                        Console.WriteLine("Type the product code you want edit :");
                        string? inputCode = Console.ReadLine();
                        int codeUpdate = validateId(inputCode);
                        if (codeUpdate > 0)
                        {
                            Console.WriteLine("Type the new name:");
                            string? inputName = Console.ReadLine();

                            Console.WriteLine("Type the new amount:");
                            string? inputAmount = Console.ReadLine();

                            Console.WriteLine("Type the new price:");
                            string? inputPrice = Console.ReadLine();

                            Update(codeUpdate, inputName, inputAmount, inputPrice);
                            break;
                        }
                        Console.WriteLine("This code dont exist:");
                        break;

                    case 4:
                        Console.WriteLine("Type the code you want delete :");
                        string? inputCodeDelete = Console.ReadLine();
                        int codeDelete = validateId(inputCodeDelete);
                        if (codeDelete > 0)
                        {
                            Delete(codeDelete);
                            break;
                        }
                        Console.WriteLine("This code dont exist:");
                        break;

                    case 5:
                        Console.WriteLine("Closing aplication ....");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;

                }
            }
            else
            {
                Console.WriteLine("\n ❌ Error! Type a number, not a text ❌");
            }
        }
    }


    public static string dotToComma(string? inputPrice)
    {
        if (!string.IsNullOrWhiteSpace(inputPrice))
        {
            string price = inputPrice.Replace('.', ',');
            return price;
        }
        string msg = "Error! Cannot possible convert this price!";
        return msg;
    }
    public static int searchHighestIdCounter()
    {
        foreach (Produto produto in produtos)
        {
            while (idCounterGlobal <= produto.code)
            {
                idCounterGlobal++;
            }
        }
        return idCounterGlobal;
    }
    public static int validateId(string? inputCode)
    {
        if (!int.TryParse(inputCode, out int validatedCode))
        {
            Console.WriteLine("\n ❌ Type a valid code! ❌ ");
            return 0;
        }
        var product = produtos.Find(x => x.code == validatedCode);
        if (product == null)
        {
            Console.WriteLine("\n ❌ This code dont exsist! ❌ ");
            return 0;
        }

        return validatedCode;
    }

    public static bool validateString(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine(" ❌ Don't let the camp blank!  ");
            return false;
        }
        return true;
    }

    static List<Produto> loadJson()
    {
        string DataFile = "savedProducts.json";
        if (File.Exists(DataFile))
        {
            string Data = File.ReadAllText(DataFile);
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

    static void saveJson(List<Produto> produtos)
    {
        string jsonString = JsonSerializer.Serialize<List<Produto>>(produtos);
        File.WriteAllText("savedProducts.json", jsonString);
    }


    static void Create()
    {
        //Getters
        Console.WriteLine("Type the product name!");
        string? inputName = Console.ReadLine();

        Console.WriteLine("Type the amount of the product!");
        string? inputAmount = Console.ReadLine();

        Console.WriteLine("Type the product price!");
        string? inputPrice = Console.ReadLine();

        if (validateString(inputName) && validateString(inputAmount) && validateString(inputPrice))
        {
            inputPrice = dotToComma(inputPrice);
            if (int.TryParse(inputAmount, out int amount) && int.Parse(inputAmount) > 0 && double.TryParse(inputPrice, out double price) && double.Parse(inputPrice) > 0)
            {
                Produto novoProduto = new Produto // Cria o objeto
                {
                    code = searchHighestIdCounter(),
                    name = inputName,
                    amount = amount,
                    price = price

                };

                produtos.Add(novoProduto); // Guarda dentro da lista

                Console.WriteLine($"\nProduto: {novoProduto.name}, \nQuantidade: {novoProduto.amount}, \nPreço: {novoProduto.price:C2}"); // Puxa os dados pelo objeto
                Console.WriteLine("\n ✅Product created with sucess! ✅");

                saveJson(produtos);
            }
            else
            {
                Console.WriteLine("\n❌ Error! Type the product amount ou price in the valid way");
            }

        }
    }

    static void Read()
    {
        if (produtos.Count() == 0)
        {
            Console.WriteLine("\n Nothing was founded!");
            return;
        }

        string linha = new string('-', 62);

        Console.WriteLine();
        Console.WriteLine(linha);
        Console.WriteLine("{0,-8}{1,-26}{2,12}{3,16}", "Código", "Produto", "Quantidade", "Preço");
        Console.WriteLine(linha);

        foreach (Produto produto in produtos)
        {
            string nome = produto.name.Length > 24
            ? produto.name.Substring(0, 21) + "..."
            : produto.name;

            Console.WriteLine($"{produto.code,-8}{nome,-26}{produto.amount,12}{produto.price,16:C2}");
        }

        Console.WriteLine(linha);
        Console.WriteLine($"Total de produtos: {produtos.Count}");
    }

    static void Update(int id, string? inputName, string? inputAmount, string? inputPrice)
    {
        var produtoUpdate = produtos.Find(x => x.code == id);
        inputPrice = dotToComma(inputPrice);
        if (produtoUpdate != null)
        {
            if (!string.IsNullOrWhiteSpace(inputName))
            {
                produtoUpdate.name = inputName.Trim();
                saveJson(produtos);
                Console.WriteLine("\n ✅ Name was sucessfully updated! ✅");
            }
            else
            {
                Console.WriteLine("\n✅ The name wasn't updated! ✅");
            }
            if (int.TryParse(inputAmount, out int amount))
            {
                if (amount >= 0)
                {
                    produtoUpdate.amount = amount;
                    saveJson(produtos);
                    Console.WriteLine("\n ✅ Amount was sucessfully updated! ✅");
                }
                else
                {
                    Console.WriteLine("\n❌ Error! Type a amount that isn't negative ");
                }
            }
            else
            {
                Console.WriteLine("\n✅ The amount wasn't updated! ✅");
            }
            if (double.TryParse(inputPrice, out double price))
            {
                if (price >= 0)
                {
                    produtoUpdate.price = price;
                    saveJson(produtos);
                    Console.WriteLine("\n ✅ Price was sucessfully updated! ✅");
                }
                else
                {
                    Console.WriteLine("\n❌ Error! Type a price that isn't negative! ❌");
                }
            }
            else
            {
                Console.WriteLine("\n✅ The price wasn't updated! ✅");
            }

        }
        else
        {
            Console.WriteLine("\n❌ Error. This code dont exist ❌ ");
        }
    }

    static void Delete(int id)
    {
        Console.WriteLine($"Do you really want to remove the product with {id} code?(Y/N) ");
        string? input = Console.ReadLine();

        if (input == "Y" || input == "y")
        {
            var removed = produtos.RemoveAll(x => x.code == id);
            if (removed != 0)
            {
                saveJson(produtos);
                Console.WriteLine("\nProduct removed!");
            }
            else
            {
                Console.WriteLine("Error! Nothing been removed!");
            }
        }
        else if (input == "N" || input == "n")
        {
            Console.WriteLine("\n Process canceled!");
            return;
        }
        else
        {
            Console.WriteLine("\n ❌ Invalid choice! Type Y or N. ❌");
            return;
        }
    }


}
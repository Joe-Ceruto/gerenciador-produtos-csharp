using System.Security.Cryptography;
using Products;
public class ProductService
{

    public static int idCounterGlobal = 1;
    private IProductRepository _repository;
    private List<Produto> _produtos;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
        _produtos = _repository.loadJson();
    }

    static string validationString(string? message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string? text = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            Console.WriteLine("\n ❌ Campo obrigatório! Tente novamente! ❌ ");
        }
    }

    public int validationInt(string? message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string? text = Console.ReadLine();
            if (int.TryParse(text, out int number))
            {
                if (number >= 0)
                {
                    return number;
                }
            }
            Console.WriteLine("\n ❌ Campo inválido! Tente novamente ❌ ");
        }
    }

    static decimal validationDecimal(string? message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string? text = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(text))
            {
                string textPrice = text.Replace('.', ',');

                if (decimal.TryParse(textPrice, out decimal number))
                {
                    if (number >= 0)
                    {
                        return number;
                    }
                    Console.WriteLine("\n ❌ Type a non negative number! ❌ ");
                }
            }
            Console.WriteLine("\n ❌ Campo obrigatório! Tente novamente ❌ ");
        }
    }

    public int validateId()
    {
        int validatedCode = validationInt("Type the code you want delete :");
        var product = _produtos.Find(x => x.Code == validatedCode);
        if (product == null)
        {
            Console.WriteLine("\n ❌ This code dont exsist! ❌ ");
            return 0;
        }

        return validatedCode;
    }

    public int searchHighestIdCounter()
    {
        foreach (Produto produto in _produtos)
        {
            while (idCounterGlobal <= produto.Code)
            {
                idCounterGlobal++;
            }
        }
        return idCounterGlobal;
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

    public void Create()
    {

        //Getters
        string name = validationString("Type the product name");

        int amount = validationInt("Type the amount of the product!");

        decimal price = validationDecimal("Type the product price!");

        if (price <= 0)
        {
            Console.WriteLine("Digite um número válido!");
        }

        Produto novoProduto = new Produto // Cria o objeto
        {
            Code = searchHighestIdCounter(),
            Name = name,
            Amount = amount,
            Price = price

        };

        _produtos.Add(novoProduto); // Guarda dentro da lista

        Console.WriteLine($"\nProduto: {novoProduto.Name}, \nQuantidade: {novoProduto.Amount}, \nPreço: {novoProduto.Price:C2}"); // Puxa os dados pelo objeto
        Console.WriteLine("\n ✅Product created with sucess! ✅");

        _repository.saveJson(_produtos);
    }

    public void Read()
    {
        if (_produtos.Count() == 0)
        {
            Console.WriteLine("\n Nothing was founded!");
            return;
        }

        string linha = new string('-', 62);

        Console.WriteLine();
        Console.WriteLine(linha);
        Console.WriteLine("{0,-8}{1,-26}{2,12}{3,16}", "Código", "Produto", "Quantidade", "Preço");
        Console.WriteLine(linha);

        foreach (Produto produto in _produtos)
        {
            string nome = produto.Name.Length > 24
            ? produto.Name.Substring(0, 21) + "..."
            : produto.Name;

            Console.WriteLine($"{produto.Code,-8}{nome,-26}{produto.Amount,12}{produto.Price,16:C2}");
        }

        Console.WriteLine(linha);
        Console.WriteLine($"Total de produtos: {_produtos.Count}");
    }

    public void Update(int id, string? inputName, string? inputAmount, string? inputPrice)
    {
        var produtoUpdate = _produtos.Find(x => x.Code == id);
        inputPrice = dotToComma(inputPrice);
        if (produtoUpdate != null)
        {
            if (!string.IsNullOrWhiteSpace(inputName))
            {
                produtoUpdate.Name = inputName.Trim();
                _repository.saveJson(_produtos);
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
                    produtoUpdate.Amount = amount;
                    _repository.saveJson(_produtos);
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
            if (decimal.TryParse(inputPrice, out decimal price))
            {
                if (price >= 0)
                {
                    produtoUpdate.Price = price;
                    _repository.saveJson(_produtos);
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

    public void Delete(int id)
    {
        Console.WriteLine($"Do you really want to remove the product with {id} code?(Y/N) ");
        string? input = Console.ReadLine();

        if (input == "Y" || input == "y")
        {
            var removed = _produtos.RemoveAll(x => x.Code == id);
            if (removed != 0)
            {
                _repository.saveJson(_produtos);
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

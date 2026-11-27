using System.ComponentModel;
using System.Security.Permissions;

namespace Products
{
    public class Produto
    {
        private int _code;
        public int Code
        {
            get => _code;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("The code can't be negative");
                _code = value;
            }
        }
        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The name can't be blank");
                _name = value.Trim();
            }
        }


        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("The amount can't be negative!");
                _amount = value;
            }
        }
        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("The price can't be negative!");
                _price = value;
            }
        }
    }

}
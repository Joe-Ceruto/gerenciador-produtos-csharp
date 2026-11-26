using System.ComponentModel;
using System.Security.Permissions;

namespace Products
{
    public class Produto
    {
        public int code { get; init; }
        public string name { get; set; } = "";
        public int amount { get; set; }
        public decimal price {get; set;}
    }
}
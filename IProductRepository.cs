using Products;
public interface IProductRepository
{
    List<Produto> loadJson();
    void saveJson(List<Produto> produtos);
}
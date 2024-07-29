namespace Catalog.API.Execptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Product not found!")        
        { 
        
        }
    }
}

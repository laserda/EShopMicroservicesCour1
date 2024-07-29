namespace Catalog.API.Execptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id)        
        { 
        
        }
    }
}

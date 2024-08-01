namespace Catalog.API.Products.GetProductByCategorie;

public record GetProductByCategoryQuery(string Categpry) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Categpry))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}

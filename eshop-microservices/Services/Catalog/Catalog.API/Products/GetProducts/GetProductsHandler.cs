using Marten.Pagination;

namespace Catalog.API.Products.GetProduct;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var productsQuery = session.Query<Product>();
        var pageSize = query.PageSize ?? 10;

        var products = await productsQuery.ToPagedListAsync(query.PageNumber ?? 1, pageSize, cancellationToken);
        //var productsCount = productsQuery.ToList().Count();
        //var pageTotal = Math.Ceiling( productsCount / (double)pageSize);


        return new GetProductsResult(products);
    }
}

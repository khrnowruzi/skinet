using Core.Entities;

namespace Core.Specifications;

public class BrandlistSpecification : BaseSpecification<Product, string>
{
    public BrandlistSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}

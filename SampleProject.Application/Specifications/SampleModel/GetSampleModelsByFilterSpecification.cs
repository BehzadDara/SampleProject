using SampleProject.Domain.BaseSpecificationConfig;
using SampleProject.Application.Features.SampleModel.Queries.GetSampleModelsByFilter;

namespace SampleProject.Application.Specifications.SampleModel;

public class GetSampleModelsByFilterSpecification : BaseSpecification<Domain.Models.SampleModel>
{
    public GetSampleModelsByFilterSpecification(GetSampleModelsByFilterQuery query)
    {
        if (query.MinAge.HasValue)
        {
            AddCriteria(x => x.Age >= query.MinAge);
        }
        if (query.MaxAge.HasValue)
        {
            AddCriteria(x => x.Age <= query.MaxAge);
        }
        if (query.Gender.HasValue)
        {
            AddCriteria(x => (int)x.Gender == query.Gender);
        }

        switch (query.OrderBy)
        {
            case OrderSampleModelByFilter.FirstName:
                AddOrderBy(a => a.FirstName, query.OrderType);
                break;
            case OrderSampleModelByFilter.LastName:
                AddOrderBy(a => a.LastName, query.OrderType);
                break;
            case OrderSampleModelByFilter.Age:
                AddOrderBy(a => a.Age, query.OrderType);
                break;
            default:
                AddOrderBy(a => a.CreatedAt, query.OrderType);
                break;
        }

        AddPaging(query.PageSize, query.PageNumber);
    }
}

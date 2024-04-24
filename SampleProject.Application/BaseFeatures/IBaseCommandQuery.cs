using MediatR;

namespace SampleProject.Application.BaseFeatures;

public interface IBaseCommandQuery<TResult> : IRequest<BaseResult<TResult>>, IBaseRequest
{
}

public interface IBaseCommandQuery : IRequest<BaseResult>, IBaseRequest
{
}

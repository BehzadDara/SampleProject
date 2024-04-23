using MediatR;

namespace SampleProject.Application.BaseFeature;

public interface IBaseCommandQuery<TResult> : IRequest<BaseResult<TResult>>, IBaseRequest
{
}

public interface IBaseCommandQuery : IRequest<BaseResult>, IBaseRequest
{
}

using MediatR;

namespace SampleProject.Application.BaseFeature;

public interface IBaseCommandQueryHandler<in TRequest, TResult> : IRequestHandler<TRequest, BaseResult<TResult>> 
    where TRequest : IRequest<BaseResult<TResult>>
{
}

public interface IBaseCommandQueryHandler<in TRequest> : IRequestHandler<TRequest, BaseResult> 
    where TRequest : IRequest<BaseResult>
{
}

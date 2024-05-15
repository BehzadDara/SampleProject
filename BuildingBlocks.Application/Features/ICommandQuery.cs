using MediatR;

namespace BuildingBlocks.Application.Features;

public interface ICommandQuery<TResult> : IRequest<Result<TResult>>, IBaseRequest
{
}

public interface ICommandQuery : IRequest<Result>, IBaseRequest
{
}

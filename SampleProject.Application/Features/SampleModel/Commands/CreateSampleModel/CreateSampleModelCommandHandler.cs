using BuildingBlocks.Application.Features;
using SampleProject.Domain.Interfaces;
using StackExchange.Redis;

namespace SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;

public class CreateSampleModelCommandHandler(
    ISampleProjectUnitOfWork unitOfWork,
    IConnectionMultiplexer connectionMultiplexer
    ) : ICommandQueryHandler<CreateSampleModelCommand>
{
    private readonly IDatabase redisDatabase = connectionMultiplexer.GetDatabase();

    public async Task<Result> Handle(CreateSampleModelCommand request, CancellationToken cancellationToken)
    {
        /*
        var entities = await unitOfWork.SampleModelRepository.GetAllAsync(cancellationToken);
        if (entities.Any(x => x.Address.Equals(request.Address, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ConflictException(BuildingBlocks.Resources.Messages.Conflict);
        }
        */

        var entity = request.ToEntity();
        await unitOfWork.SampleModelRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await redisDatabase.KeyDeleteAsync("SampleModelTotalCount");

        var result = new Result();
        result.OK();
        return result;
    }
}

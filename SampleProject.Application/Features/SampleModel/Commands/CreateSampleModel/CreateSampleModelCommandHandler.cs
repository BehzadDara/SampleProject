using BuildingBlocks.Application.Features;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;

public class CreateSampleModelCommandHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<CreateSampleModelCommand>
{
    public async Task<Result> Handle(CreateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();
        await unitOfWork.SampleModelRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new Result();
        result.OK();
        return result;
    }
}

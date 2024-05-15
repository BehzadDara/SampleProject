using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Features;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public class UpdateSampleModelCommandHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<UpdateSampleModelCommand>
{
    public async Task<Result> Handle(UpdateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException();

        var entity = request.ToEntity(existEntity);
        await unitOfWork.SampleModelRepository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new Result();
        result.OK();
        return result;
    }
}
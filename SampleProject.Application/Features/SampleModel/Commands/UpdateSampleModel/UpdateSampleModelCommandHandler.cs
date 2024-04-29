using SampleProject.Application.BaseExceptions;
using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public class UpdateSampleModelCommandHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<UpdateSampleModelCommand>
{
    public async Task<BaseResult> Handle(UpdateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException();

        var entity = request.ToEntity(existEntity);
        await unitOfWork.SampleModelRepository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var result = new BaseResult();
        result.OK();
        return result;
    }
}
using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public class UpdateSampleModelCommandHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<UpdateSampleModelCommand>
{
    public async Task<BaseResult> Handle(UpdateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseResult();

        var validator = new UpdateSampleModelValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            result.BadRequest(validationResult.Errors);
            return result;
        }

        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existEntity is null)
        {
            result.NotFound();
            return result;
        }

        var entity = request.ToEntity(existEntity);
        await unitOfWork.SampleModelRepository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        result.Success();
        return result;
    }
}
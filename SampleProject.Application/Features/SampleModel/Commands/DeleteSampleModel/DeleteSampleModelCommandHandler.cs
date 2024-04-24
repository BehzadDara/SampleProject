using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;

public class DeleteSampleModelCommandHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<DeleteSampleModelCommand>
{
    public async Task<BaseResult> Handle(DeleteSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseResult();

        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existEntity is null)
        {
            result.NotFound();
            return result;
        }

        await unitOfWork.SampleModelRepository.DeleteAsync(existEntity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        result.Success();
        return result;
    }
}
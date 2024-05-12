using SampleProject.Application.BaseExceptions;
using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;

public class DeleteSampleModelCommandHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<DeleteSampleModelCommand>
{
    public async Task<BaseResult> Handle(DeleteSampleModelCommand request, CancellationToken cancellationToken)
    {
        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new NotFoundException();

        await unitOfWork.SampleModelRepository.DeleteAsync(existEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new BaseResult();
        result.OK();
        return result;
    }
}
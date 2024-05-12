using SampleProject.Application.BaseFeatures;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;

public class CreateSampleModelCommandHandler(IUnitOfWork unitOfWork) : IBaseCommandQueryHandler<CreateSampleModelCommand>
{
    public async Task<BaseResult> Handle(CreateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();
        await unitOfWork.SampleModelRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new BaseResult();
        result.OK();
        return result;
    }
}

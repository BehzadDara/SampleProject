using SampleProject.Application.BaseFeature;
using SampleProject.Application.Mapper;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;

public class CreateSampleModelCommandHandler(IUnitOfWork unitOfWork, SampleModelMapper mapper) : IBaseCommandQueryHandler<CreateSampleModelCommand>
{
    public async Task<BaseResult> Handle(CreateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseResult();

        var entity = mapper.ToEntity(request);
        await unitOfWork.SampleModelRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        result.AddSuccessMessage(Resources.Messages.SuccessAction);
        return result;
    }
}

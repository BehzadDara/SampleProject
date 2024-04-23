using SampleProject.Application.BaseFeature;
using SampleProject.Application.Mapper;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;

public class UpdateSampleModelCommandHandler(IUnitOfWork unitOfWork, SampleModelMapper mapper) : IBaseCommandQueryHandler<UpdateSampleModelCommand>
{
    public async Task<BaseResult> Handle(UpdateSampleModelCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseResult();

        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existEntity is null)
        {
            result.NotFound();
            return result;
        }

        var entity = mapper.ToEntity(request);
        await unitOfWork.SampleModelRepository.UpdateAsync(entity, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        result.Success();
        return result;
    }
}
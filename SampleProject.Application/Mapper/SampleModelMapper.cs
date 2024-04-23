using AutoMapper;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Models;

namespace SampleProject.Application.Mapper;

public class SampleModelMapper(IMapper _mapper)
{
    public SampleModelViewModel ToViewModel(SampleModel input)
    {
        return _mapper.Map<SampleModelViewModel>(input);
    }

    public IList<SampleModelViewModel> ToViewModel(IList<SampleModel> input)
    {
        return _mapper.Map<IList<SampleModelViewModel>>(input);
    }

    public SampleModel ToEntity(CreateSampleModelCommand input)
    {
        return _mapper.Map<SampleModel>(input);
    }

    public SampleModel ToEntity(UpdateSampleModelCommand input)
    {
        return _mapper.Map<SampleModel>(input);
    }
}

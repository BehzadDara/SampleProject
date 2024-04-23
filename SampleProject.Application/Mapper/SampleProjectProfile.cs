using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Models;

namespace SampleProject.Application.Mapper;

public class SampleProjectProfile : AutoMapper.Profile
{
    public SampleProjectProfile()
    {
        CreateMap<SampleModel, SampleModelViewModel>();
        CreateMap<CreateSampleModelCommand, SampleModel>();
        CreateMap<UpdateSampleModelCommand, SampleModel>();
    }
}

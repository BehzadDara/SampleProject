using AutoMapper;
using BuildingBlocks.Application.ViewModels;
using SampleProject.Application.Features.SampleModel.Commands.CreateSampleModel;
using SampleProject.Application.Features.SampleModel.Commands.UpdateSampleModel;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Models;

namespace SampleProject.Application;

public static class SampleModelMapper
{
    public static SampleModelViewModel ToViewModel(this SampleModel input)
    {
        var config = new MapperConfiguration(cfg => 
            cfg.CreateMap<SampleModel, SampleModelViewModel>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(
                       src => new EnumViewModel(src.Gender))));

        var mapper = new Mapper(config);

        return mapper.Map<SampleModelViewModel>(input);
    }

    public static IReadOnlyList<SampleModelViewModel> ToViewModel(this IReadOnlyList<SampleModel> input)
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<SampleModel, SampleModelViewModel>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(
                       src => new EnumViewModel(src.Gender))));

        var mapper = new Mapper(config);

        return mapper.Map<IReadOnlyList<SampleModelViewModel>>(input);
    }

    public static SampleModel ToEntity(this CreateSampleModelCommand input)
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<CreateSampleModelCommand, SampleModel>());

        var mapper = new Mapper(config);

        return mapper.Map<SampleModel>(input);
    }

    public static SampleModel ToEntity(this UpdateSampleModelCommand input, SampleModel entity)
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<UpdateSampleModelCommand, SampleModel>());

        var mapper = new Mapper(config);

        return mapper.Map(input, entity);
    }
}

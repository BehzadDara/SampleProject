using SampleProject.Application.BaseViewModels;

namespace SampleProject.Application.ViewModels;

public class SampleModelViewModel : TrackableEntityViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public int GenderKey { get; set; }
    public string GenderValue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

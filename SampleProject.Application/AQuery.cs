using MediatR;
using SampleProject.Application.BaseFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Application;

public record AQuery(string X) : IRequest<BaseResult<string>>;

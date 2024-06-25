using BuildingBlocks.API.Configs;
using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Features;
using BuildingBlocks.Application.Methods;
using CaptchaGen.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;

namespace BuildingBlocks.API.Controlles;

[SwaggerTag("Captcha service")]
public partial class CaptchaController(IMemoryCache memoryCache) : BaseController
{
    [HttpGet]
    [SwaggerOperation("Generate captcha")]
    public IActionResult Generate()
    {
        string captchaCode;
        string encryptedCaptchaCode;
        do
        {
            captchaCode = ImageFactory.CreateCode(CaptchaSettings.DigitCount);
            encryptedCaptchaCode = EncryptionHelper.Encrypt(captchaCode);
        } 
        while (encryptedCaptchaCode.Contains('/') || encryptedCaptchaCode.Contains('+'));

        var cacheEntryOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };
        memoryCache.Set(encryptedCaptchaCode, captchaCode, cacheEntryOptions);

        var result = new Result<string>();
        result.AddValue(encryptedCaptchaCode);
        result.OK();

        return ApiResult(result);
    }

    [HttpGet("{encryptedCaptchaCode}")]
    [SwaggerOperation("Get Captcha Image")]
    public IActionResult GetImage(string encryptedCaptchaCode)
    {
        var captchaCode = EncryptionHelper.Decrypt(encryptedCaptchaCode);
        using var captchaImage = ImageFactory.BuildImage(
            captchaCode, 
            CaptchaSettings.Height, 
            CaptchaSettings.Width,
            CaptchaSettings.FontSize,
            CaptchaSettings.Distortion
            );

        return File(captchaImage.ToArray(), "image/jpg");
    }
}
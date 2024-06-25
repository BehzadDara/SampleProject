namespace BuildingBlocks.Application.DTOs;

public record CaptchaDTO(
    string? Code,
    string? EncryptedCode
    );
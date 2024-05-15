namespace BuildingBlocks.Application.Methods;

public static partial class ValidationHelper
{
    public static bool IsValidEnum<TEnum>(int data) where TEnum : struct, Enum
    {
        return Enum.IsDefined(typeof(TEnum), data);
    }

    public static bool IsValidMobile(string mobile)
    {
        if (string.IsNullOrEmpty(mobile))
            return false;
        
        return mobile.Length == 11 && mobile.StartsWith("09");
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        
        var regex = EmailRegex();
        return regex.IsMatch(email);
    }

    public static bool IsValidString(string data)
    {
        return !string.IsNullOrEmpty(data);
    }

    public static bool IsValidNationalCode(string nationalCode)
    {
        if (!string.IsNullOrEmpty(nationalCode))
            return false;
        
        if (nationalCode.Length != 10) 
            return false;

        switch (nationalCode)
        {
            case "0000000000":
            case "1111111111":
            case "2222222222":
            case "3333333333":
            case "4444444444":
            case "5555555555":
            case "6666666666":
            case "7777777777":
            case "8888888888":
            case "9999999999":
                return false;
            default:
                int code = 0;
                char ch;
                for (int i = 0; i < 9; i++)
                {
                    ch = nationalCode[i];
                    if (ch < '0') return false;
                    if (ch > '9') return false;

                    int v = ch - 48;
                    code += v * (10 - i);
                }
                int r = code % 11;
                if (r > 1) r = 11 - r;
                ch = nationalCode[9];
                if (r == (ch - 48)) return true;
                break;
        }

        return false;
    }

    [System.Text.RegularExpressions.GeneratedRegex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])")]
    private static partial System.Text.RegularExpressions.Regex EmailRegex();
}
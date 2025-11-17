using System;

namespace ETR.Nine.Services.Forex.Infrastructure.Exceptions;

public class ForexApiException : Exception
{
    public string Code { get; }
    public string Description {get;}

    public ForexApiException(string code, string description) : base(description)
    {
        Code = code;
        Description = description;
    }
}

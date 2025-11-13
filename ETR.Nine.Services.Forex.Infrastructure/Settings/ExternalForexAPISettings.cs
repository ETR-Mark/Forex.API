using System;

namespace ETR.Nine.Services.Forex.Infrastructure.Settings;

public class ExternalForexAPISettings
{
    public string BaseUrl {get; set;} = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}

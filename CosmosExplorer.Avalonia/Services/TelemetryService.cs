using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace CosmosExplorer.Avalonia.Services;

public class TelemetryService 
{
    private readonly TelemetryClient _telemetryClient;

    public TelemetryService(string instrumentationKey)
    {
        var config = TelemetryConfiguration.CreateDefault();
        config.InstrumentationKey = instrumentationKey;
            
        _telemetryClient = new TelemetryClient(config);
            
        _telemetryClient.Context.Component.Version = typeof(App).Assembly.GetName().Version?.ToString();
        _telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
    }

    public void TrackPageView(string pageName)
    {
        _telemetryClient.TrackPageView(pageName);
    }

    public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
    {
        _telemetryClient.TrackEvent(eventName, properties);
    }

    public void TrackException(Exception exception, IDictionary<string, string> properties = null)
    {
        _telemetryClient.TrackException(exception, properties);
    }

    public void Flush()
    {
        _telemetryClient.Flush();
    }
}
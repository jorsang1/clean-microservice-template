using System.Diagnostics.Metrics;

namespace CleanCompanyName.DDDMicroservice.Api.Telemetry;

public class ApplicationMetrics
{
    public const string ServiceMetricName = "DDDMicroservice";
    private static readonly Meter _meter = new(ServiceMetricName, "1.0.0");
    private static readonly Counter<int> _productsAdded = _meter.CreateCounter<int>("products-added");

    public static void NewProductAdded() => _productsAdded.Add(1);
}
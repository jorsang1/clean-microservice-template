namespace CleanCompanyName.DDDMicroservice.Infrastructure.Clients;

public static class UriExtensions
{
    public static Uri EnsureSlashed(this Uri uri) => new(uri.ToString().TrimEnd('/') + "/");
}
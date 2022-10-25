namespace CleanCompanyName.DDDMicroservice.Api.Endpoints;

internal class SwaggerEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
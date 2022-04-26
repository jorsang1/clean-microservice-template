namespace CleanCompanyName.DDDMicroservice.Api.Endpoints;

public class SwaggerEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
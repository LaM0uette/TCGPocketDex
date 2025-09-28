using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TCGPocketDex.Api.Endpoints;

public static class ImagesEndpoints
{
    public static IEndpointRouteBuilder MapImages(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/images");

        // GET /img/cards/{name}
        group.MapGet("/cards/{name}", (IWebHostEnvironment env, string name) =>
        {
            // Basic sanitization: allow only letters, numbers, dash and underscore
            if (string.IsNullOrWhiteSpace(name) || !System.Text.RegularExpressions.Regex.IsMatch(name, "^[A-Za-z0-9_-]+$"))
            {
                return Results.BadRequest("Invalid image name.");
            }

            var webRoot = env.WebRootPath;
            var filePath = Path.Combine(webRoot, "img", "cards", name + ".png");
            if (!File.Exists(filePath))
            {
                return Results.NotFound();
            }

            return Results.File(filePath, contentType: "image/png");
        });

        // GET /img/cards -> list of image base names (without extension)
        group.MapGet("/cards", (IWebHostEnvironment env) =>
        {
            var dir = Path.Combine(env.WebRootPath, "img", "cards");
            if (!Directory.Exists(dir))
            {
                return Results.Ok(Array.Empty<string>());
            }

            var names = Directory.GetFiles(dir, "*.png")
                .Select(Path.GetFileNameWithoutExtension)
                .OrderBy(n => n, StringComparer.OrdinalIgnoreCase)
                .ToArray();
            return Results.Ok(names);
        });

        return app;
    }
}
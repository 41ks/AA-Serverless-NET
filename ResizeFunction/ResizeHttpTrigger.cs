using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Company.Function
{
    public static class ResizeHttpTrigger
    {
        [FunctionName("ResizeHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (!int.TryParse(req.Query["w"], out int w) || w <= 0)
            {
                return new BadRequestObjectResult("Invalid or missing 'w' parameter.");
            }

            if (!int.TryParse(req.Query["h"], out int h) || h <= 0)
            {
                return new BadRequestObjectResult("Invalid or missing 'h' parameter.");
            }

            byte[] targetImageBytes;
            using (var msInput = new MemoryStream())
            {
                // Récupère le corps du message en mémoire
                await req.Body.CopyToAsync(msInput);
                msInput.Position = 0;

                // Check if image exists
                if (msInput.Length == 0)
                {
                    return new BadRequestObjectResult("Image not found in the request body.");
                }
                // Check if the image is a valid format
                try
                {
                    using (var image = Image.Load(msInput))
                    {
                        // Check if the image is a valid format
                        if (image == null)
                        {
                            return new BadRequestObjectResult("Invalid image format.");
                        }

                        // Effectue la transformation
                        image.Mutate(x => x.Resize(w, h));

                        // Sauvegarde en mémoire               
                        using (var msOutput = new MemoryStream())
                        {
                            image.SaveAsJpeg(msOutput);
                            targetImageBytes = msOutput.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.LogError(ex, "Error loading image from request body.");
                    return new BadRequestObjectResult("Invalid image format.");
                }
            }
            // Renvoie le contenu avec le content-type correspondant à une image jpeg
            // TODO renvoyer les octets de l'image
            // TODO ... ainsi que le content-type correspondant à une image Jpeg
            return new FileContentResult(targetImageBytes, "image/jpeg"); ;

        }
    }
}
public class MediaTypeHelper
{
    public static string GetMediaType(string fileExtension)
    {
        var mediaTypeMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".doc", "application/msword" },
            { ".pdf", "application/pdf" },
            { ".csv", "text/csv" },
            { ".xml", "application/xml" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
            { ".ppt", "application/vnd.ms-powerpoint" }
            // Add more mappings as needed
        };

        if (mediaTypeMappings.TryGetValue(fileExtension, out var mediaType))
        {
            return mediaType;
        }

        // Default to application/octet-stream if the extension is not found
        return "application/octet-stream";
    }
}

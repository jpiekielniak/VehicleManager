namespace VehicleManager.Tests.Unit.Vehicles.Helpers;

public static class FormFileHelper
{
    private const string ContentType = "image/jpeg";
    public static Faker<IFormFile> FormFileFaker()
    {
        return new Faker<IFormFile>()
            .CustomInstantiator(f => CreateMockFormFile(
                fileName: f.System.FileName(),
                contentType: ContentType,
                content: f.Lorem.Paragraph()
            ));
    }

    private static IFormFile CreateMockFormFile(
        string fileName,
        string contentType,
        string content)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);
        
        var formFile = new FormFile(
            baseStream: stream,
            baseStreamOffset: 0,
            length: bytes.Length,
            name: fileName,
            fileName: fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };

        return formFile;
    }
}
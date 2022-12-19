namespace OpenAIDemo.Models
{
    // serves as our input model
    public class TextToImageForm
    {
        public string? prompt { get; set; }
        public short? n { get; set; }
        public string? size { get; set; }
    }
}

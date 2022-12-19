namespace OpenAIDemo.Models
{
    // model for the DALL E api response
    public class ImageResponse
    {
        public long created { get; set; }
        public List<Link>? data { get; set; }
    }
}

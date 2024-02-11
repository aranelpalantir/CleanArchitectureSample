using System.Text.Json;

namespace CleanArchSample.Application.Exceptions
{
    internal class ExceptionModel
    {
        public int StatusCode { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherTests
{
    internal static class HttpContentExtensions
    {
        internal static Task<T> ReadAsExample<T>(this HttpContent content, T example) => content.ReadAsAsync<T>();
    }
}

using System.Threading.Tasks;

namespace Spread2Text.Services;

public interface ISttService
{
    Task<string> TranscribeAsync(byte[] audio);

}

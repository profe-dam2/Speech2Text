using System.Threading.Tasks;

namespace Spread2Text.Services;

public interface IAudioRecorder
{
    Task<byte[]> RecordAsync(int seconds);
}

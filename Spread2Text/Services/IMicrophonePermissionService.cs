using System.Threading.Tasks;

namespace Spread2Text.Services;

public interface IMicrophonePermissionService
{
    Task<bool> EnsurePermissionAsync();
}

namespace Spread2Text.Services;

public class AppServices
{
    public static IAudioRecorder AudioRecorder { get; set; }
    public static ISttService SttService { get; set; }
    public static IMicrophonePermissionService MicrophonePermission { get; set; }

}
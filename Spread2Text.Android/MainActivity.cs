using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Renderscripts;
using Avalonia;
using Avalonia.Android;
using Spread2Text.Services;
using Spread2Text.ViewModels;

namespace Spread2Text.Android;

[Activity(
    Label = "Spread2Text.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
    
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        AppServices.MicrophonePermission =
            new AndroidMicrophonePermissionService(this);

        AppServices.AudioRecorder = new AndroidAudioRecorder();
        AppServices.SttService =
            new AndroidSystemSttService(this);

    }
    
    protected override void OnActivityResult(
        int requestCode,
        Result resultCode,
        Intent? data)
    {
        base.OnActivityResult(requestCode, resultCode, data);

        if (AppServices.SttService is AndroidSystemSttService sys)
        {
            sys.OnActivityResult(requestCode, resultCode, data);
        }
    }

    public override void OnRequestPermissionsResult(
        int requestCode,
        string[] permissions,
        Permission[] grantResults)
    {
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        // Delegar en el servicio de permisos
        if (AppServices.MicrophonePermission
            is AndroidMicrophonePermissionService permissionService)
        {
            permissionService.OnRequestPermissionsResult(
                requestCode, grantResults);
        }
    }
    
}
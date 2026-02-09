using System;
using System.IO;
using Android.Media;
using Avalonia.Platform;

namespace Spread2Text.Services;

public class AndroidAudioPlayer
{
    public void PlayFromAsset(string avaresPath)
    {
        using var stream =
            AssetLoader.Open(new Uri(avaresPath));

        var tempPath = Path.Combine(
            Android.App.Application.Context.CacheDir!.AbsolutePath,
            "temp.mp3");

        using (var file = File.Create(tempPath))
            stream.CopyTo(file);

        var player = new MediaPlayer();
        player.SetDataSource(tempPath);
        player.Prepare();
        player.Start();
    }
}
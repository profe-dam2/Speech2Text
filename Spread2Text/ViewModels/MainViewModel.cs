using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Spread2Text.Services;

namespace Spread2Text.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _text = "App para reconocimiento de voz";
    
    [RelayCommand]
    public async Task RecordAsync()
    {   
   
        try
        {
            if (!await AppServices.MicrophonePermission.EnsurePermissionAsync())
            {
                Text = "Permiso denegado";
                return;
            }
            
            AppServices.AudioPlayer
                .PlayFromAsset("avares://Spread2Text/Assets/endrecord.mp3");

            
            Text = "Preparando…";
            var audio = await AppServices.AudioRecorder.RecordAsync(0);

            Text = "Escuchando…";
            Text = await AppServices.SttService.TranscribeAsync(audio);

            AppServices.AudioPlayer
                .PlayFromAsset("avares://Spread2Text/Assets/record.mp3");


        }
        catch (Exception ex)
        {
            Text = "ERROR: " + ex.GetType().Name;
            Console.WriteLine(ex);
        }
        

        
    }
    
    

}
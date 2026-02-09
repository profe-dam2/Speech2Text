using Android.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Spread2Text.Services;


namespace Spread2Text.Android;


public class AndroidAudioRecorder : IAudioRecorder
{
    private const int SampleRate = 16000;
    private const ChannelIn Channel = ChannelIn.Mono;
    private const Encoding AudioEncoding = Encoding.Pcm16bit;

    private bool _isRecording;

    public async Task<byte[]> RecordAsync(int seconds)
    {
        if (_isRecording)
            throw new InvalidOperationException("Ya grabando");

        _isRecording = true;
        await Task.Delay(500);

        int minBufferSize = AudioRecord.GetMinBufferSize(
            SampleRate, Channel, AudioEncoding);

        var recorder = new AudioRecord(
            AudioSource.Mic,
            SampleRate,
            Channel,
            AudioEncoding,
            minBufferSize);

        if (recorder.State != State.Initialized)
            throw new InvalidOperationException("AudioRecord no inicializado");

        try
        {
            using var stream = new MemoryStream();
            var buffer = new byte[minBufferSize];

            recorder.StartRecording();
            Console.WriteLine("[AUDIO] StartRecording");

            var end = DateTime.UtcNow.AddSeconds(seconds);

            while (DateTime.UtcNow < end)
            {
                int read = recorder.Read(buffer, 0, buffer.Length);
                if (read > 0)
                    stream.Write(buffer, 0, read);
            }

            recorder.Stop();
            Console.WriteLine("[AUDIO] StopRecording");

            return stream.ToArray();
        }
        finally
        {
            recorder.Release();
            recorder.Dispose();
            _isRecording = false;

            Console.WriteLine("[AUDIO] Released");
        }
    }
}
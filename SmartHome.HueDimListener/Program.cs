using Microsoft.Extensions.Configuration;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using SmartHome.HueDimListener;
using SmartHome.HueDimListener.ConsoleHotKey;

var hueAppKey = new ConfigurationBuilder().AddUserSecrets<UserSecrets>().Build().GetSection("HueAppKey").Value;
ILocalHueClient client = new LocalHueClient("192.168.178.40");
client.Initialize(hueAppKey);
var groups = await client.GetGroupsAsync();
var bedroom = groups.Single(group => group.Name == "Bedroom");
var brightness = bedroom.Action.Brightness;

HotKeyManager.RegisterHotKey(Keys.F13, KeyModifiers.NoRepeat);
HotKeyManager.RegisterHotKey(Keys.F14, KeyModifiers.NoRepeat);
HotKeyManager.HotKeyPressed += KeyPressed;

void KeyPressed(object? sender, HotKeyEventArgs e)
{
    byte brightnessStep = 12;

    switch (e.Key)
    {
        case Keys.F13:
            brightness = byte.Min(brightness, (byte) (brightness - brightnessStep));
            client.SendCommandAsync(new LightCommand { Brightness = brightness }, bedroom.Lights).GetAwaiter().GetResult();
            break;
        case Keys.F14:
            brightness = byte.Max(brightness, (byte) (brightness + brightnessStep));
            client.SendCommandAsync(new LightCommand { Brightness = brightness }, bedroom.Lights).GetAwaiter().GetResult();
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

Console.ReadLine();

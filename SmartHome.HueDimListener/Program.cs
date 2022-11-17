using Microsoft.Extensions.Configuration;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using SmartHome.HueDimListener;
using SmartHome.HueDimListener.ConsoleHotKey;

var config = new ConfigurationBuilder().AddUserSecrets<UserSecrets>().Build();
var hueAppKey = config.GetSection("HueAppKey").Value;
var hueBridgeIp = config.GetSection("HueBridgeIp").Value;
if (hueAppKey == null || hueBridgeIp == null) throw new ArgumentNullException(nameof(config));

ILocalHueClient client = new LocalHueClient(hueBridgeIp);
client.Initialize(hueAppKey);

var groups = await client.GetGroupsAsync();
var bedroom = groups.Single(group => group.Name == "Bedroom");
var brightness = bedroom.Action.Brightness;

HotKeyManager.RegisterHotKey(Keys.F13, KeyModifiers.NoRepeat);
HotKeyManager.RegisterHotKey(Keys.F14, KeyModifiers.NoRepeat);
HotKeyManager.RegisterHotKey(Keys.F15, KeyModifiers.NoRepeat);
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
        case Keys.F15:
            switch (bedroom.State.AnyOn)
            {
                case null:
                    return;
                case true:
                    client.SendCommandAsync(new LightCommand().TurnOff());
                    bedroom.State.AnyOn = false;
                    break;
                default:
                    client.SendCommandAsync(new LightCommand().TurnOn());
                    bedroom.State.AnyOn = true;
                    break;
            }

            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

Console.ReadLine();

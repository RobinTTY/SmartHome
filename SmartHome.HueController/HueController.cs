using SmartHome.HueDimListener;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Groups;

namespace SmartHome.HueController
{
    internal class HueController
    {
        private readonly ILocalHueClient _hueClient;
        private Group? _bedroomLightGroup;
        private readonly DateTime _lastGroupFetch;
        private readonly List<int> _registeredKeys;

        public HueController()
        {
            var config = new ConfigurationBuilder().AddJsonFile("secrets.json").Build();
            var hueAppKey = config.GetSection("HueAppKey").Value;
            var hueBridgeIp = config.GetSection("HueBridgeIp").Value;
            if (hueAppKey == null || hueBridgeIp == null) throw new ArgumentNullException(nameof(config));

            _hueClient = new LocalHueClient(hueBridgeIp);
            _hueClient.Initialize(hueAppKey);
            _lastGroupFetch = DateTime.MinValue;
            _registeredKeys = new List<int>();
        }

        public void RegisterHotKeys()
        {
            var keysToRegister = new List<Keys>{ Keys.F13, Keys.F14, Keys.F15 };
            keysToRegister.ForEach(key =>
            {
                var id = HotKeyManager.RegisterHotKey(key, KeyModifiers.NoRepeat);
                _registeredKeys.Add(id);
            });
            HotKeyManager.HotKeyPressed += OnKeyPressed;
        }

        public void UnregisterHotKeys()
        {
            _registeredKeys.ForEach(HotKeyManager.UnregisterHotKey);
        }

        private void OnKeyPressed(object? sender, HotKeyEventArgs e)
        {
            const byte brightnessStep = 12;
            byte brightness = 0;

            // Refresh brightness information if older than 5 seconds (other device could have changed information)
            if (DateTime.Now - _lastGroupFetch > TimeSpan.FromSeconds(5))
            {
                var groups = _hueClient.GetGroupsAsync().GetAwaiter().GetResult();
                _bedroomLightGroup = groups.SingleOrDefault(group => group.Name == "Bedroom");
                if(_bedroomLightGroup == null) return;
                brightness = _bedroomLightGroup.Action.Brightness;
            }

            if(_bedroomLightGroup == null)
                return;

            switch (e.Key)
            {
                case Keys.F13:
                    brightness = byte.Min(brightness, (byte)(brightness - brightnessStep));
                    _hueClient.SendCommandAsync(new LightCommand { Brightness = brightness }, _bedroomLightGroup.Lights).GetAwaiter().GetResult();
                    break;
                case Keys.F14:
                    brightness = byte.Max(brightness, (byte)(brightness + brightnessStep));
                    _hueClient.SendCommandAsync(new LightCommand { Brightness = brightness }, _bedroomLightGroup.Lights).GetAwaiter().GetResult();
                    break;
                case Keys.F15:
                    switch (_bedroomLightGroup.State.AnyOn)
                    {
                        case null:
                            return;
                        case true:
                            _hueClient.SendCommandAsync(new LightCommand().TurnOff());
                            _bedroomLightGroup.State.AnyOn = false;
                            break;
                        default:
                            _hueClient.SendCommandAsync(new LightCommand().TurnOn());
                            _bedroomLightGroup.State.AnyOn = true;
                            break;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

using System.Runtime.InteropServices;
using Application = System.Windows.Forms.Application;

namespace SmartHome.HueDimListener
{
    namespace ConsoleHotKey
    {
        /// <summary>
        /// Courtesy of: https://stackoverflow.com/questions/3654787/global-hotkey-in-console-application
        /// </summary>
        public static class HotKeyManager
        {
            public static event EventHandler<HotKeyEventArgs>? HotKeyPressed;

            public static int RegisterHotKey(Keys key, KeyModifiers modifiers)
            {
                _windowReadyEvent.WaitOne();
                var id = Interlocked.Increment(ref _id);
                _wnd?.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _windowHandle, id, (uint)modifiers, (uint)key);
                return id;
            }

            public static void UnregisterHotKey(int id)
            {
                _wnd.Invoke(new UnRegisterHotKeyDelegate(UnRegisterHotKeyInternal), _windowHandle, id);
            }

            delegate void RegisterHotKeyDelegate(IntPtr windowHandle, int id, uint modifiers, uint key);
            delegate void UnRegisterHotKeyDelegate(IntPtr windowHandle, int id);

            private static void RegisterHotKeyInternal(IntPtr windowHandle, int id, uint modifiers, uint key)
            {
                RegisterHotKey(windowHandle, id, modifiers, key);
            }

            private static void UnRegisterHotKeyInternal(IntPtr windowHandle, int id)
            {
                UnregisterHotKey(_windowHandle, id);
            }

            private static void OnHotKeyPressed(HotKeyEventArgs e)
            {
                if (HotKeyManager.HotKeyPressed != null)
                {
                    HotKeyManager.HotKeyPressed(null, e);
                }
            }

            private static volatile MessageWindow? _wnd;
            private static volatile IntPtr _windowHandle;
            private static readonly ManualResetEvent _windowReadyEvent = new(false);
            static HotKeyManager()
            {
                var messageLoop = new Thread(delegate ()
                {
                    Application.Run(new MessageWindow());
                });

                messageLoop.Name = "MessageLoopThread";
                messageLoop.IsBackground = true;
                messageLoop.Start();
            }

            private class MessageWindow : Form
            {
                public MessageWindow()
                {
                    _wnd = this;
                    _windowHandle = Handle;
                    _windowReadyEvent.Set();
                }

                protected override void WndProc(ref Message m)
                {
                    if (m.Msg == WmHotKey)
                    {
                        HotKeyEventArgs e = new HotKeyEventArgs(m.LParam);
                        HotKeyManager.OnHotKeyPressed(e);
                    }

                    base.WndProc(ref m);
                }

                protected override void SetVisibleCore(bool value)
                {
                    // Ensure the window never becomes visible
                    base.SetVisibleCore(false);
                }

                private const int WmHotKey = 0x312;
            }

            [DllImport("user32", SetLastError = true)]
            private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

            [DllImport("user32", SetLastError = true)]
            private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            private static int _id = 0;
        }


        public class HotKeyEventArgs : EventArgs
        {
            public readonly Keys Key;
            public readonly KeyModifiers Modifiers;

            public HotKeyEventArgs(Keys key, KeyModifiers modifiers)
            {
                this.Key = key;
                this.Modifiers = modifiers;
            }

            public HotKeyEventArgs(IntPtr hotKeyParam)
            {
                uint param = (uint)hotKeyParam.ToInt64();
                Key = (Keys)((param & 0xffff0000) >> 16);
                Modifiers = (KeyModifiers)(param & 0x0000ffff);
            }
        }

        [Flags]
        public enum KeyModifiers
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8,
            NoRepeat = 0x4000
        }
    }
}

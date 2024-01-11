using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;


namespace KeyboardHookMain //Was first created by refactorsaurusrex
{
    /// <summary>
    /// Equivalent of the Keys enumerations, but includes a value allowing Windows Logo keys to act as a modifer key.
    /// </summary>
    [Flags]
    public enum KeysEx
    {
        /// <summary>
        /// The bitmask to extract modifiers from a key value.
        /// </summary>
        Modifiers = -65536,
        /// <summary>
        /// No key pressed.
        /// </summary>
        None = 0,
        //
        // Summary:
        //     The left mouse button.
        LButton = 1,
        //
        // Summary:
        //     The right mouse button.
        RButton = 2,
        //
        // Summary:
        //     The CANCEL key.
        Cancel = 3,
        //
        // Summary:
        //     The middle mouse button (three-button mouse).
        MButton = 4,
        //
        // Summary:
        //     The first x mouse button (five-button mouse).
        XButton1 = 5,
        //
        // Summary:
        //     The second x mouse button (five-button mouse).
        XButton2 = 6,
        //
        // Summary:
        //     The BACKSPACE key.
        Back = 8,
        //
        // Summary:
        //     The TAB key.
        Tab = 9,
        //
        // Summary:
        //     The LINEFEED key.
        LineFeed = 10,
        //
        // Summary:
        //     The CLEAR key.
        Clear = 12,
        //
        // Summary:
        //     The ENTER key.
        Enter = 13,
        //
        // Summary:
        //     The RETURN key.
        Return = 13,
        //
        // Summary:
        //     The SHIFT key.
        ShiftKey = 16,
        //
        // Summary:
        //     The CTRL key.
        ControlKey = 17,
        //
        // Summary:
        //     The ALT key.
        Menu = 18,
        //
        // Summary:
        //     The PAUSE key.
        Pause = 19,
        //
        // Summary:
        //     The CAPS LOCK key.
        CapsLock = 20,
        //
        // Summary:
        //     The CAPS LOCK key.
        Capital = 20,
        //
        // Summary:
        //     The IME Kana mode key.
        KanaMode = 21,
        //
        // Summary:
        //     The IME Hanguel mode key. (maintained for compatibility; use HangulMode)
        HanguelMode = 21,
        //
        // Summary:
        //     The IME Hangul mode key.
        HangulMode = 21,
        //
        // Summary:
        //     The IME Junja mode key.
        JunjaMode = 23,
        //
        // Summary:
        //     The IME final mode key.
        FinalMode = 24,
        //
        // Summary:
        //     The IME Kanji mode key.
        KanjiMode = 25,
        //
        // Summary:
        //     The IME Hanja mode key.
        HanjaMode = 25,
        //
        // Summary:
        //     The ESC key.
        Escape = 27,
        //
        // Summary:
        //     The IME convert key.
        IMEConvert = 28,
        //
        // Summary:
        //     The IME nonconvert key.
        IMENonconvert = 29,
        //
        // Summary:
        //     The IME accept key. Obsolete, use System.Windows.Forms.Keys.IMEAccept instead.
        IMEAceept = 30,
        //
        // Summary:
        //     The IME accept key, replaces System.Windows.Forms.Keys.IMEAceept.
        IMEAccept = 30,
        //
        // Summary:
        //     The IME mode change key.
        IMEModeChange = 31,
        //
        // Summary:
        //     The SPACEBAR key.
        Space = 32,
        //
        // Summary:
        //     The PAGE UP key.
        Prior = 33,
        //
        // Summary:
        //     The PAGE UP key.
        PageUp = 33,
        //
        // Summary:
        //     The PAGE DOWN key.
        Next = 34,
        //
        // Summary:
        //     The PAGE DOWN key.
        PageDown = 34,
        //
        // Summary:
        //     The END key.
        End = 35,
        //
        // Summary:
        //     The HOME key.
        Home = 36,
        //
        // Summary:
        //     The LEFT ARROW key.
        Left = 37,
        //
        // Summary:
        //     The UP ARROW key.
        Up = 38,
        //
        // Summary:
        //     The RIGHT ARROW key.
        Right = 39,
        //
        // Summary:
        //     The DOWN ARROW key.
        Down = 40,
        //
        // Summary:
        //     The SELECT key.
        Select = 41,
        //
        // Summary:
        //     The PRINT key.
        Print = 42,
        //
        // Summary:
        //     The EXECUTE key.
        Execute = 43,
        //
        // Summary:
        //     The PRINT SCREEN key.
        PrintScreen = 44,
        //
        // Summary:
        //     The PRINT SCREEN key.
        Snapshot = 44,
        //
        // Summary:
        //     The INS key.
        Insert = 45,
        //
        // Summary:
        //     The DEL key.
        Delete = 46,
        //
        // Summary:
        //     The HELP key.
        Help = 47,
        //
        // Summary:
        //     The 0 key.
        D0 = 48,
        //
        // Summary:
        //     The 1 key.
        D1 = 49,
        //
        // Summary:
        //     The 2 key.
        D2 = 50,
        //
        // Summary:
        //     The 3 key.
        D3 = 51,
        //
        // Summary:
        //     The 4 key.
        D4 = 52,
        //
        // Summary:
        //     The 5 key.
        D5 = 53,
        //
        // Summary:
        //     The 6 key.
        D6 = 54,
        //
        // Summary:
        //     The 7 key.
        D7 = 55,
        //
        // Summary:
        //     The 8 key.
        D8 = 56,
        //
        // Summary:
        //     The 9 key.
        D9 = 57,
        //
        // Summary:
        //     The A key.
        A = 65,
        //
        // Summary:
        //     The B key.
        B = 66,
        //
        // Summary:
        //     The C key.
        C = 67,
        //
        // Summary:
        //     The D key.
        D = 68,
        //
        // Summary:
        //     The E key.
        E = 69,
        //
        // Summary:
        //     The F key.
        F = 70,
        //
        // Summary:
        //     The G key.
        G = 71,
        //
        // Summary:
        //     The H key.
        H = 72,
        //
        // Summary:
        //     The I key.
        I = 73,
        //
        // Summary:
        //     The J key.
        J = 74,
        //
        // Summary:
        //     The K key.
        K = 75,
        //
        // Summary:
        //     The L key.
        L = 76,
        //
        // Summary:
        //     The M key.
        M = 77,
        //
        // Summary:
        //     The N key.
        N = 78,
        //
        // Summary:
        //     The O key.
        O = 79,
        //
        // Summary:
        //     The P key.
        P = 80,
        //
        // Summary:
        //     The Q key.
        Q = 81,
        //
        // Summary:
        //     The R key.
        R = 82,
        //
        // Summary:
        //     The S key.
        S = 83,
        //
        // Summary:
        //     The T key.
        T = 84,
        //
        // Summary:
        //     The U key.
        U = 85,
        //
        // Summary:
        //     The V key.
        V = 86,
        //
        // Summary:
        //     The W key.
        W = 87,
        //
        // Summary:
        //     The X key.
        X = 88,
        //
        // Summary:
        //     The Y key.
        Y = 89,
        //
        // Summary:
        //     The Z key.
        Z = 90,
        //
        // Summary:
        //     The left Windows logo key (Microsoft Natural Keyboard).
        LWin = 91,
        //
        // Summary:
        //     The right Windows logo key (Microsoft Natural Keyboard).
        RWin = 92,
        //
        // Summary:
        //     The application key (Microsoft Natural Keyboard).
        Apps = 93,
        //
        // Summary:
        //     The computer sleep key.
        Sleep = 95,
        //
        // Summary:
        //     The 0 key on the numeric keypad.
        NumPad0 = 96,
        //
        // Summary:
        //     The 1 key on the numeric keypad.
        NumPad1 = 97,
        //
        // Summary:
        //     The 2 key on the numeric keypad.
        NumPad2 = 98,
        //
        // Summary:
        //     The 3 key on the numeric keypad.
        NumPad3 = 99,
        //
        // Summary:
        //     The 4 key on the numeric keypad.
        NumPad4 = 100,
        //
        // Summary:
        //     The 5 key on the numeric keypad.
        NumPad5 = 101,
        //
        // Summary:
        //     The 6 key on the numeric keypad.
        NumPad6 = 102,
        //
        // Summary:
        //     The 7 key on the numeric keypad.
        NumPad7 = 103,
        //
        // Summary:
        //     The 8 key on the numeric keypad.
        NumPad8 = 104,
        //
        // Summary:
        //     The 9 key on the numeric keypad.
        NumPad9 = 105,
        //
        // Summary:
        //     The multiply key.
        Multiply = 106,
        //
        // Summary:
        //     The add key.
        Add = 107,
        //
        // Summary:
        //     The separator key.
        Separator = 108,
        //
        // Summary:
        //     The subtract key.
        Subtract = 109,
        //
        // Summary:
        //     The decimal key.
        Decimal = 110,
        //
        // Summary:
        //     The divide key.
        Divide = 111,
        //
        // Summary:
        //     The F1 key.
        F1 = 112,
        //
        // Summary:
        //     The F2 key.
        F2 = 113,
        //
        // Summary:
        //     The F3 key.
        F3 = 114,
        //
        // Summary:
        //     The F4 key.
        F4 = 115,
        //
        // Summary:
        //     The F5 key.
        F5 = 116,
        //
        // Summary:
        //     The F6 key.
        F6 = 117,
        //
        // Summary:
        //     The F7 key.
        F7 = 118,
        //
        // Summary:
        //     The F8 key.
        F8 = 119,
        //
        // Summary:
        //     The F9 key.
        F9 = 120,
        //
        // Summary:
        //     The F10 key.
        F10 = 121,
        //
        // Summary:
        //     The F11 key.
        F11 = 122,
        //
        // Summary:
        //     The F12 key.
        F12 = 123,
        //
        // Summary:
        //     The F13 key.
        F13 = 124,
        //
        // Summary:
        //     The F14 key.
        F14 = 125,
        //
        // Summary:
        //     The F15 key.
        F15 = 126,
        //
        // Summary:
        //     The F16 key.
        F16 = 127,
        //
        // Summary:
        //     The F17 key.
        F17 = 128,
        //
        // Summary:
        //     The F18 key.
        F18 = 129,
        //
        // Summary:
        //     The F19 key.
        F19 = 130,
        //
        // Summary:
        //     The F20 key.
        F20 = 131,
        //
        // Summary:
        //     The F21 key.
        F21 = 132,
        //
        // Summary:
        //     The F22 key.
        F22 = 133,
        //
        // Summary:
        //     The F23 key.
        F23 = 134,
        //
        // Summary:
        //     The F24 key.
        F24 = 135,
        //
        // Summary:
        //     The NUM LOCK key.
        NumLock = 144,
        //
        // Summary:
        //     The SCROLL LOCK key.
        Scroll = 145,
        //
        // Summary:
        //     The left SHIFT key.
        LShiftKey = 160,
        //
        // Summary:
        //     The right SHIFT key.
        RShiftKey = 161,
        //
        // Summary:
        //     The left CTRL key.
        LControlKey = 162,
        //
        // Summary:
        //     The right CTRL key.
        RControlKey = 163,
        //
        // Summary:
        //     The left ALT key.
        LMenu = 164,
        //
        // Summary:
        //     The right ALT key.
        RMenu = 165,
        //
        // Summary:
        //     The browser back key (Windows 2000 or later).
        BrowserBack = 166,
        //
        // Summary:
        //     The browser forward key (Windows 2000 or later).
        BrowserForward = 167,
        //
        // Summary:
        //     The browser refresh key (Windows 2000 or later).
        BrowserRefresh = 168,
        //
        // Summary:
        //     The browser stop key (Windows 2000 or later).
        BrowserStop = 169,
        //
        // Summary:
        //     The browser search key (Windows 2000 or later).
        BrowserSearch = 170,
        //
        // Summary:
        //     The browser favorites key (Windows 2000 or later).
        BrowserFavorites = 171,
        //
        // Summary:
        //     The browser home key (Windows 2000 or later).
        BrowserHome = 172,
        //
        // Summary:
        //     The volume mute key (Windows 2000 or later).
        VolumeMute = 173,
        //
        // Summary:
        //     The volume down key (Windows 2000 or later).
        VolumeDown = 174,
        //
        // Summary:
        //     The volume up key (Windows 2000 or later).
        VolumeUp = 175,
        /// <summary>
        /// The media next track key (Windows 2000 or later).
        /// </summary>
        MediaNextTrack = 176,
        /// <summary>
        /// The media previous track key (Windows 2000 or later).
        /// </summary>
        MediaPreviousTrack = 177,
        /// <summary>
        /// The media Stop key (Windows 2000 or later).
        /// </summary>
        MediaStop = 178,
        /// <summary>
        /// The media play pause key (Windows 2000 or later).
        /// </summary>
        MediaPlayPause = 179,
        /// <summary>
        /// The launch mail key (Windows 2000 or later).
        /// </summary>
        LaunchMail = 180,
        /// <summary>
        /// The select media key (Windows 2000 or later).
        /// </summary>
        SelectMedia = 181,
        /// <summary>
        /// The start application one key (Windows 2000 or later).
        /// </summary>
        LaunchApplication1 = 182,
        /// <summary>
        /// The start application two key (Windows 2000 or later).
        /// </summary>
        LaunchApplication2 = 183,
        /// <summary>
        /// The OEM 1 key.
        /// </summary>
        Oem1 = 186,
        /// <summary>
        /// The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).
        /// </summary>
        OemSemicolon = 186,
        /// <summary>
        /// The OEM plus key on any country/region keyboard (Windows 2000 or later).
        /// </summary>
        Oemplus = 187,
        /// <summary>
        /// The OEM comma key on any country/region keyboard (Windows 2000 or later).
        /// </summary>
        Oemcomma = 188,
        /// <summary>
        /// The OEM minus key on any country/region keyboard (Windows 2000 or later).
        /// </summary>
        OemMinus = 189,
        /// <summary>
        /// The OEM period key on any country/region keyboard (Windows 2000 or later).
        /// </summary>
        OemPeriod = 190,
        /// <summary>
        /// The OEM question mark key on a US standard keyboard (Windows 2000 or later).
        /// </summary>
        OemQuestion = 191,
        /// <summary>
        /// The OEM 2 key.
        /// </summary>
        Oem2 = 191,
        /// <summary>
        /// The OEM tilde key on a US standard keyboard (Windows 2000 or later).
        /// </summary>
        Oemtilde = 192,
        /// <summary>
        /// The OEM 3 key.
        /// </summary>
        Oem3 = 192,
        /// <summary>
        /// The OEM 4 key.
        /// </summary>
        Oem4 = 219,
        /// <summary>
        /// The OEM open bracket key on a US standard keyboard (Windows 2000 or later).
        /// </summary>
        OemOpenBrackets = 219,
        /// <summary>
        /// The OEM pipe key on a US standard keyboard (Windows 2000 or later).
        /// </summary>
        OemPipe = 220,
        /// <summary>
        /// The OEM 5 key.
        /// </summary>
        Oem5 = 220,
        /// <summary>
        /// The OEM 6 key.
        /// </summary>
        Oem6 = 221,
        /// <summary>
        /// The OEM close bracket key on a US standard keyboard (Windows 2000 or later).
        /// </summary>
        OemCloseBrackets = 221,
        /// <summary>
        /// The OEM 7 key.
        /// </summary>
        Oem7 = 222,
        /// <summary>
        /// The OEM singled/double quote key on a US standard keyboard (Windows 2000
        /// or later).
        /// </summary>
        OemQuotes = 222,
        /// <summary>
        /// The OEM 8 key.
        /// </summary>
        Oem8 = 223,
        /// <summary>
        /// The OEM 102 key.
        /// </summary>
        Oem102 = 226,
        /// <summary>
        /// The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows
        /// 2000 or later).
        /// </summary>
        OemBackslash = 226,
        /// <summary>
        /// The PROCESS KEY key.
        /// </summary>
        ProcessKey = 229,
        /// <summary>
        /// Used to pass Unicode characters as if they were keystrokes. The Packet key
        /// value is the low word of a 32-bit virtual-key value used for non-keyboard
        /// input methods.
        /// </summary>
        Packet = 231,
        /// <summary>
        /// The ATTN key.
        /// </summary>
        Attn = 246,
        /// <summary>
        /// The CRSEL key.
        /// </summary>
        Crsel = 247,
        /// <summary>
        /// The EXSEL key.
        /// </summary>
        Exsel = 248,
        /// <summary>
        /// The ERASE EOF key.
        /// </summary>
        EraseEof = 249,
        /// <summary>
        /// The PLAY key.
        /// </summary>
        Play = 250,
        /// <summary>
        /// The ZOOM key.
        /// </summary>
        Zoom = 251,
        /// <summary>
        /// A constant reserved for future use.
        /// </summary>
        NoName = 252,
        /// <summary>
        /// The PA1 key.
        /// </summary>
        Pa1 = 253,
        /// <summary>
        /// The CLEAR key.
        /// </summary>
        OemClear = 254,
        /// <summary>
        /// The bitmask to extract a key code from a key value.
        /// </summary>
        KeyCode = 65535,
        /// <summary>
        /// The SHIFT modifier key.
        /// </summary>
        Shift = 65536,
        /// <summary>
        /// The CTRL modifier key.
        /// </summary>
        Control = 131072,
        /// <summary>
        /// The ALT modifier key.
        /// </summary>
        Alt = 262144,
        /// <summary>
        /// The WINLOGO modifier key.
        /// </summary>
        WinLogo = 524288
    }

    public enum KeyState
    {
        /// <summary>
        /// Indicates that the key is pressed down.
        /// </summary>
        Down,
        /// <summary>
        /// Indicates that the key has been released.
        /// </summary>
        Up
    }

    static class NativeMethods
    {
        internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

        internal static class KeyStateConstants
        {
            internal const int KEY_PRESSED = 0x8000;
            internal const int WM_KEYDOWN = 0x0100;
            internal const int WM_KEYUP = 0x0101;
            internal const int WM_SYSKEYDOWN = 0x0104;
            internal const int WM_SYSKEYUP = 0x0105;
            internal const int WH_KEYBOARD_LL = 13;
        }
    }

    public class KeyCombination : IEquatable<KeyCombination>
    {
        /// <summary>
        /// The backing keys for this combination.
        /// </summary>
        KeysEx keyChain;

        /// <summary>
        /// Creates a new KeyCombination based on the provided archetype. The new combination is a deep copy
        /// of the original.
        /// </summary>
        /// <param name="combo">The combination to copy from.</param>
        /// <returns>A new KeyCombination object.</returns>
        public static KeyCombination CopyFrom(KeyCombination combo)
        {
            return new KeyCombination(combo.Keys);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCombination"/> class.
        /// </summary>
        /// <param name="keys">The keys that constitute a keyboard combination.</param>
        public KeyCombination(Keys keys)
        {
            keyChain = keys.ToKeysEx();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCombination"/> class.
        /// </summary>
        /// <param name="keys">The keys that constitute a keyboard combination.</param>
        public KeyCombination(KeysEx keys)
        {
            keyChain = keys;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCombination"/> class.
        /// </summary>
        /// <param name="keys">The keys that constitute a keyboard combination, express in the following
        /// format: "Modifer + Modifier + Key".</param>
        public KeyCombination(string keys)
        {
            if (!string.IsNullOrEmpty(keys))
                FromStringToKeys(keys);
        }

        /// <summary>
        /// Returns true if the values of its operands are equal, otherwise false.
        /// </summary>
        public static bool operator ==(KeyCombination x, KeyCombination y)
        {
            if (ReferenceEquals(null, x))
                return ReferenceEquals(null, y);

            return x.Equals(y);
        }

        /// <summary>
        /// returns false if its operands are equal, otherwise true.
        /// </summary>
        public static bool operator !=(KeyCombination x, KeyCombination y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Adds the specified keys to this combination. Note that a combination can only contain a single
        /// non-modifier key. If one already exists, it will be overwritten.
        /// </summary>
        /// <param name="keys">The keys to add.</param>
        public void Add(KeysEx keys)
        {
            KeysEx keyCode = keys & KeysEx.KeyCode;

            if (keyCode != KeysEx.None)
                keyChain = keyCode | keyChain & KeysEx.Modifiers;

            KeysEx modifiers = keys & KeysEx.Modifiers;

            if (modifiers != KeysEx.None)
                keyChain = modifiers | keyChain & KeysEx.KeyCode | keyChain & KeysEx.Modifiers;
        }

        /// <summary>
        /// Removes the specified key from this combination. Must be called once for each key to be removed.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public void Remove(KeysEx key)
        {
            if (Contains(key))
                keyChain ^= key;

            if (key == KeysEx.LWin || key == KeysEx.RWin)
            {
                if (Contains(KeysEx.WinLogo))
                    keyChain ^= KeysEx.WinLogo;
            }
        }

        /// <summary>
        /// Determines whether the specified key exists in this combination.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the specified key exists; otherwise, false.</returns>
        public bool Contains(KeysEx key)
        {
            switch (key)
            {
                case KeysEx.Control:
                case KeysEx.Shift:
                case KeysEx.Alt:
                case KeysEx.WinLogo:
                    return (keyChain & KeysEx.Modifiers) == key;

                default:
                    return (keyChain & KeysEx.KeyCode) == key;
            }
        }

        /// <summary>
        /// Gets the keys that constitute this combination.
        /// </summary>
        public KeysEx Keys
        {
            get { return keyChain; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return Keys == KeysEx.None; }
        }

        /// <summary>
        /// Gets the unmodified key for this combination.
        /// </summary>
        public KeysEx UnmodifiedKey
        {
            get { return keyChain & KeysEx.KeyCode; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return keyChain.ToFriendlyString();
        }

        /// <summary>
        /// Gets a value indicating whether this combination is valid.
        /// </summary>
        /// <remarks>A valid combination is one which can be used as a keyboard shortcut. Certain 
        /// keys such as Enter or CapsLock cannot be used for a shortcut. A combination also cannot 
        /// consist solely of modifier keys.</remarks>
        public bool IsValid
        {
            get
            {
                KeysEx keyCode = keyChain & KeysEx.KeyCode;
                switch (keyCode)
                {
                    case KeysEx.Enter:
                    case KeysEx.CapsLock:
                    case KeysEx.NumLock:
                    case KeysEx.Tab:
                    case KeysEx.None:
                    case KeysEx.ShiftKey:
                    case KeysEx.LShiftKey:
                    case KeysEx.RShiftKey:
                    case KeysEx.ControlKey:
                    case KeysEx.LControlKey:
                    case KeysEx.RControlKey:
                    case KeysEx.Menu:
                    case KeysEx.LMenu:
                    case KeysEx.RMenu:
                    case KeysEx.LWin:
                    case KeysEx.RWin:
                        return false;

                    case KeysEx.Delete:
                        if ((keyChain & KeysEx.Modifiers) == (KeysEx.Control | KeysEx.Alt))
                            return false;
                        break;
                }

                return true;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as KeyCombination);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return 17 * 23 + ToString().GetHashCode();
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals(KeyCombination other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return ToString() == other.ToString();
        }

        /// <summary>
        /// Froms the string to keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        void FromStringToKeys(string keys)
        {
            IEnumerable<string> segments = keys.Split(new[] { '+' });

            foreach (string segment in segments)
            {
                string modifiedSegment = segment.ToLowerInvariant().Trim() == "ctrl" ? "Control" : segment;
                keyChain |= EnumParser.Parse<KeysEx>(modifiedSegment.Trim());
            }
        }
    }

    public static class EnumParser
    {
        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        public static T Parse<T>(string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        public static T Parse<T>(string value, bool ignoreCase) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Converts the specified Keys to a KeysEx enumeration.
        /// </summary>
        /// <param name="keys">The keys to convert.</param>
        public static KeysEx ToKeysEx(this Keys keys)
        {
            return EnumParser.Parse<KeysEx>(keys.ToString());
        }
    }

    public static class KeysExtensions
    {
        /// <summary>
        /// Returns a friendly string representation of this KeysEx instance.
        /// </summary>
        /// <param name="keys">The keys to convert to a friendly string.</param>
        public static string ToFriendlyString(this KeysEx keys)
        {
            var friendlyString = new StringBuilder();

            if (IsWinLogoModified(keys))
                friendlyString.Append("WinLogo + ");

            if (IsControlModified(keys))
                friendlyString.Append("Ctrl + ");

            if (IsShiftModified(keys))
                friendlyString.Append("Shift + ");

            if (IsAltModified(keys))
                friendlyString.Append("Alt + ");

            string unmodifiedKey = UnmodifiedKey(keys);

            if (string.IsNullOrEmpty(unmodifiedKey) && friendlyString.Length >= 3)
                friendlyString.Remove(friendlyString.Length - 3, 3);
            else
                friendlyString.Append(unmodifiedKey);

            return friendlyString.ToString();
        }

        /// <summary>
        /// Returns KeyCode portion of this instance. That is, the key stripped of modifiers.
        /// </summary>
        /// <param name="keys">The keys to remove modifier keys from.</param>
        static string UnmodifiedKey(KeysEx keys)
        {
            KeysEx keyCode = keys & KeysEx.KeyCode;

            switch (keyCode)
            {
                case KeysEx.Menu:
                case KeysEx.LMenu:
                case KeysEx.RMenu:
                case KeysEx.ShiftKey:
                case KeysEx.LShiftKey:
                case KeysEx.RShiftKey:
                case KeysEx.ControlKey:
                case KeysEx.LControlKey:
                case KeysEx.RControlKey:
                case KeysEx.LWin:
                case KeysEx.RWin:
                    return string.Empty;
            }

            switch (keyCode)
            {
                case KeysEx.D0:
                case KeysEx.D1:
                case KeysEx.D2:
                case KeysEx.D3:
                case KeysEx.D4:
                case KeysEx.D5:
                case KeysEx.D6:
                case KeysEx.D7:
                case KeysEx.D8:
                case KeysEx.D9:
                    return keyCode.ToString().Remove(0, 1);
            }

            if (keyCode.ToString().ToUpperInvariant().StartsWith("OEM"))
            {
                const uint convertToChar = 2;
                uint keyLiteral = NativeMethods.MapVirtualKey((uint)keyCode, convertToChar);
                return Convert.ToChar(keyLiteral).ToString(CultureInfo.InvariantCulture);
            }

            return keyCode.ToString();
        }

        /// <summary>
        /// Determines whether the specified keys contains the WinLogo modifier, the LWin key, or the RWin key.
        /// </summary>
        /// <param name="keys">The keys to check for logo key modifiers.</param>
        /// <returns>True if this instance contains a logo key modifier; otherwise, false.</returns>
        static bool IsWinLogoModified(KeysEx keys)
        {
            if ((keys & KeysEx.WinLogo) == KeysEx.WinLogo)
                return true;

            KeysEx keyCode = keys & KeysEx.KeyCode;
            switch (keyCode)
            {
                case KeysEx.LWin:
                case KeysEx.RWin:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether this enumeration contains the Alt modifier.
        /// </summary>
        /// <param name="keys">The keys to check for the Alt modifier.</param>
        /// <returns>True if this enumeration contains the Alt modifier; otherwise false.</returns>
        static bool IsAltModified(KeysEx keys)
        {
            if ((keys & KeysEx.Alt) == KeysEx.Alt)
                return true;

            KeysEx keyCode = keys & KeysEx.KeyCode;
            switch (keyCode)
            {
                case KeysEx.Menu:
                case KeysEx.LMenu:
                case KeysEx.RMenu:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether this enumeration contains the Shift modifier.
        /// </summary>
        /// <param name="keys">The keys to check for the Shift modifier.</param>
        /// <returns>True if this enumeration contains the Shift modifier; otherwise false.</returns>
        static bool IsShiftModified(KeysEx keys)
        {
            if ((keys & KeysEx.Shift) == KeysEx.Shift)
                return true;

            KeysEx keyCode = keys & KeysEx.KeyCode;
            switch (keyCode)
            {
                case KeysEx.ShiftKey:
                case KeysEx.LShiftKey:
                case KeysEx.RShiftKey:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether this enumeration contains the Control modifier.
        /// </summary>
        /// <param name="keys">The keys to check for the Control modifier.</param>
        /// <returns>True if this enumeration contains the Control modifier; otherwise false.</returns>
        static bool IsControlModified(KeysEx keys)
        {
            if ((keys & KeysEx.Control) == KeysEx.Control)
                return true;

            KeysEx keyCode = keys & KeysEx.KeyCode;
            switch (keyCode)
            {
                case KeysEx.ControlKey:
                case KeysEx.LControlKey:
                case KeysEx.RControlKey:
                    return true;
            }

            return false;
        }
    }

    public class KeyboardHook : IDisposable
    {
        const int keyUp = NativeMethods.KeyStateConstants.WM_KEYUP;
        const int systemKeyUp = NativeMethods.KeyStateConstants.WM_SYSKEYUP;
        const int keyDown = NativeMethods.KeyStateConstants.WM_KEYDOWN;
        const int systemKeyDown = NativeMethods.KeyStateConstants.WM_SYSKEYDOWN;

        static readonly HashSet<KeyCombination> currentHookKeys = new HashSet<KeyCombination>();
        static readonly NativeMethods.LowLevelKeyboardProc lockdownHookCallBack = LockdownHookCallBack;
        static bool isKeyboardLockedDown;
        static IntPtr lockdownHookId;
        static KeysEx lockedDownKeys;

        IntPtr hookId;
        bool isDisposed;
        bool keyReleased;
        readonly NativeMethods.LowLevelKeyboardProc hookCallback;

        /// <summary>
        /// Occurs when a key is pressed while keyboard lockdown is engaged.
        /// </summary>
        public static event EventHandler<KeyboardLockDownKeyPressedEventArgs> LockedDownKeyboardKeyPressed = delegate { };

        /// <summary>
        /// Engages a full keyboard lockdown, which disables all keyboard processing beyond the LockedDownKeyboardKeyPressed event.
        /// </summary>
        public static void EngageFullKeyboardLockdown()
        {
            lockdownHookId = NativeMethods.SetWindowsHookEx(
                NativeMethods.KeyStateConstants.WH_KEYBOARD_LL,
                lockdownHookCallBack,
                IntPtr.Zero,
                0);

            if (lockdownHookId == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            isKeyboardLockedDown = true;
        }

        /// <summary>
        /// Releases keyboard lockdown and allows keyboard processing as normal.
        /// </summary>
        public static void ReleaseFullKeyboardLockdown()
        {
            if (lockdownHookId != IntPtr.Zero)
            {
                NativeMethods.UnhookWindowsHookEx(lockdownHookId);
                lockdownHookId = IntPtr.Zero;
            }

            isKeyboardLockedDown = false;
        }

        /// <summary>
        /// Determines whether the specified key combination is already in use.
        /// </summary>
        /// <param name="combination">The combination to check.</param>
        /// <returns>True if the key combination is already in use; otherwise, false.</returns>
        public static bool IsKeyCombinationTaken(KeyCombination combination)
        {
            return currentHookKeys.Contains(combination);
        }

        /// <summary>
        /// Raises the KeyboardLockDownKeyPressed event by invoking each subscribed delegate asynchronously.
        /// </summary>
        /// <param name="pressedKeys">The keys that are pressed.</param>
        /// <param name="state">The state of the keys.</param>
        static void OnLockedDownKeyboardKeyPressed(KeysEx pressedKeys, KeyState state)
        {
            foreach (EventHandler<KeyboardLockDownKeyPressedEventArgs> pressedEvent in LockedDownKeyboardKeyPressed.GetInvocationList())
            {
                var args = new KeyboardLockDownKeyPressedEventArgs(pressedKeys, state);
                AsyncCallback callback = ar => ((EventHandler<KeyboardLockDownKeyPressedEventArgs>)ar.AsyncState).EndInvoke(ar);
                pressedEvent.BeginInvoke(null, args, callback, pressedEvent);
            }
        }

        /// <summary>
        /// Returns a value indicating whether a non-modifier key is currently pressed.
        /// </summary>
        /// <param name="key">The key to check. Key must not be a modifier bit mask.</param>
        /// <returns>True if the key is currently pressed; otherwise false.</returns>
        /// <exception cref="ArgumentException">Occurs if key is a modifier bit mask. </exception>
        static bool IsKeyPressed(KeysEx key)
        {
            if ((key & KeysEx.Modifiers) == KeysEx.Modifiers)
            {
                throw new ArgumentException("Key cannot contain any modifiers.", "key");
            }

            const int keyPressed = NativeMethods.KeyStateConstants.KEY_PRESSED;
            return (NativeMethods.GetKeyState((int)key) & keyPressed) == keyPressed;
        }

        /// <summary>
        /// Callback handler for keyboard lockdowns.
        /// </summary>
        static IntPtr LockdownHookCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int keyStateParam = (int)wParam;
            KeysEx pressedKey = (KeysEx)Marshal.ReadInt32(lParam);

            switch (pressedKey)
            {
                case KeysEx.LControlKey:
                case KeysEx.RControlKey:
                    pressedKey = KeysEx.Control;
                    break;

                case KeysEx.RMenu:
                case KeysEx.LMenu:
                    pressedKey = KeysEx.Alt;
                    break;

                case KeysEx.LShiftKey:
                case KeysEx.RShiftKey:
                    pressedKey = KeysEx.Shift;
                    break;

                case KeysEx.LWin:
                case KeysEx.RWin:
                    pressedKey = KeysEx.WinLogo;
                    break;
            }

            KeyState keyState;
            if (keyStateParam == keyUp || keyStateParam == systemKeyUp)
            {
                keyState = KeyState.Up;
                lockedDownKeys ^= pressedKey;
            }
            else if (keyStateParam == keyDown || keyStateParam == systemKeyDown)
            {
                keyState = KeyState.Down;
                lockedDownKeys |= pressedKey;
            }
            else
            {
                throw new ArgumentOutOfRangeException("wParam", "Invalid key state detected.");
            }

            OnLockedDownKeyboardKeyPressed(lockedDownKeys, keyState);
            return new IntPtr(1);
        }

        /// <summary>
        /// Initializes a new instance of the KeyboardHook class.
        /// </summary>
        public KeyboardHook()
        {
            hookCallback = HookCallBack;
            Combination = new KeyCombination(KeysEx.None);
        }

        /// <summary>
        /// Finalizes an instance of the KeyboardHook class.
        /// </summary>
        ~KeyboardHook()
        {
            Dispose(false);
        }

        /// <summary>
        /// Occurs when the KeyboardHook's assigned key combination is pressed.
        /// </summary>
        public event EventHandler Pressed = delegate { };

        /// <summary>
        /// Gets the key combination for this hook.
        /// </summary>
        public KeyCombination Combination { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow normal processing of key strokes after 
        /// the hook has finished processing it.
        /// </summary>
        public bool AllowPassThrough { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the KeyboardHook Pressed event is fired repeatedly for the duration
        /// of the key press or only once per key press.
        /// </summary>
        public bool AutoRepeat { get; set; }

        /// <summary>
        /// Gets a value indicating whether the KeyboardHook is currently active.
        /// </summary>
        public bool IsEngaged
        {
            get { return hookId != IntPtr.Zero; }
        }

        /// <summary>
        /// Removes the keys associated with this hook. This can only be performed when the hook is not active.
        /// </summary>
        /// <exception cref="InvalidOperationException">Occurs if the hook is currently engaged.</exception>
        public void RemoveKeys() //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------Was only public void before
        {
            if (Combination.Keys == KeysEx.None)
                return;

            if (IsEngaged)
                throw new InvalidOperationException("Cannot remove keys while hook is engaged.");

            if (Combination != null && currentHookKeys.Contains(Combination))
                currentHookKeys.Remove(Combination);

            Combination = new KeyCombination(KeysEx.None);
        }

        /// <summary>
        /// Associates the specified key combination with this hook. This can only be performed when the hook is not active.
        /// </summary>
        /// <param name="combination">The key combination.</param>
        /// <exception cref="InvalidOperationException">Occurs if this hook is currently engaged -OR- if the key combination is invalid
        /// -OR- if the key combination is already in use by another hook.</exception>
        public void SetKeys(KeyCombination combination)
        {
            if (Combination == combination)
                return;

            if (IsEngaged)
                throw new InvalidOperationException("Cannot set keys while hook is engaged.");

            if (!combination.IsValid)
                throw new InvalidOperationException("Key combination is not valid.");

            if (currentHookKeys.Contains(combination))
                throw new InvalidOperationException(string.Format("The combination '{0}' is already in use.", combination));

            if (Combination != null && currentHookKeys.Contains(Combination))
                currentHookKeys.Remove(Combination);

            currentHookKeys.Add(combination);
            Combination = combination;
        }

        /// <summary>
        /// Activates the KeyboardHook.
        /// </summary>
        /// <exception cref="InvalidOperationException">Occurs if the KeyboardHook is empty OR if the KeyboardHook is already engaged.</exception>
        /// <exception cref="ObjectDisposedException">Occurs if the KeyboardHook has been disposed.</exception>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void Engage()
        {
            if (isDisposed)
                throw new ObjectDisposedException("KeyboardHook");

            if (Combination == null)
                throw new InvalidOperationException("Cannot engage hook when Combination is null.");

            if (IsEngaged)
                return;

            hookId = NativeMethods.SetWindowsHookEx(
                NativeMethods.KeyStateConstants.WH_KEYBOARD_LL,
                hookCallback,
                IntPtr.Zero,
                0);

            if (hookId == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Removes the KeyboardHook from the system.
        /// </summary>
        /// <remarks>Disengage removes the hook from the system, but maintains all its data. Use Engage to re-install the hook. To
        /// discard the hook and all its data, use Dispose. It is not necessary to call Disengage prior to Dispose.</remarks>
        public void Disengage()
        {
            if (hookId != IntPtr.Zero)
            {
                NativeMethods.UnhookWindowsHookEx(hookId);
                hookId = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Returns the string representation of the KeyboardHook.
        /// </summary>
        /// <returns>A string representing the KeyboardHook.</returns>
        public override string ToString()
        {
            return Combination == null ? string.Empty : Combination.ToString();
        }

        /// <summary>
        /// Disengages the KeyboardHook and releases all associated resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disengages the KeyboardHook, releases all unmanaged resources, and optionally releases
        /// all managed resources.
        /// </summary>
        /// <param name="isDisposing">True to release both managed and unmanaged resources; false to 
        /// release only unmanaged resources.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            if (isDisposing)
            {
                // No managed resources to dispose.
            }

            Disengage();

            if (Combination != null)
                currentHookKeys.Remove(Combination);

            isDisposed = true;
        }

        /// <summary>
        /// Raises the Pressed event.
        /// </summary>
        /// <remarks>The Pressed event is executed asynchronously.</remarks>
        protected virtual void OnPressed()
        {
            // Invoke these asychronously in case they're slow. (Testing revealed that Windows will reassert  
            // responsibility for the key stroke(s) if the hookcallback doesn't immediately return.)
            foreach (EventHandler pressedEvent in Pressed.GetInvocationList())
                pressedEvent.BeginInvoke(this, EventArgs.Empty, ar => ((EventHandler)ar.AsyncState).EndInvoke(ar), pressedEvent);
        }

        /// <summary>
        /// Determines whether the specified key, combined with all currently pressed modifier keys, matches this hook's key combination.
        /// </summary>
        /// <param name="unmodifiedKey">The unmodified key to combine with all currently pressed keyboard modifier keys.</param>
        /// <returns>True if unmodifiedKey matches Combination.UnmodifiedKey and all modifier keys contained in the 
        /// Combination property are currently pressed; otherwise, false.</returns>
        protected bool CheckIsHookKeyCombination(KeysEx unmodifiedKey)
        {
            KeyCombination pressedKeyCombo = new KeyCombination(unmodifiedKey);

            if (IsKeyPressed(KeysEx.ControlKey))
                pressedKeyCombo.Add(KeysEx.Control);

            if (IsKeyPressed(KeysEx.ShiftKey))
                pressedKeyCombo.Add(KeysEx.Shift);

            if (IsKeyPressed(KeysEx.LMenu) || IsKeyPressed(KeysEx.RMenu))
                pressedKeyCombo.Add(KeysEx.Alt);

            if ((IsKeyPressed(KeysEx.LWin) || IsKeyPressed(KeysEx.RWin)))
                pressedKeyCombo.Add(KeysEx.WinLogo);

            return Combination == pressedKeyCombo;
        }

        /// <summary>
        /// The callback proceedure for the installed KeyboardHook. 
        /// </summary>
        /// <param name="nCode">A code the KeyboardHook procedure uses to determine how to process the message.</param>
        /// <param name="wParam">The key state.</param>
        /// <param name="lParam">The key pressed.</param>
        /// <returns>A value indicating whether or not to process additional hooks in the current hook chain.</returns>
        IntPtr HookCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (isKeyboardLockedDown)
                return new IntPtr(1); // A non-zero return value blocks additional processing of key strokes.

            // MSDN documentation indicates that nCodes less than 0 should always only invoke CallNextHookEx.
            if (nCode < 0)
                return NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam);

            int keyStateParam = (int)wParam;
            KeyState keyState;

            if (keyStateParam == keyUp || keyStateParam == systemKeyUp)
                keyState = KeyState.Up;
            else if (keyStateParam == keyDown || keyStateParam == systemKeyDown)
                keyState = KeyState.Down;
            else
                throw new ArgumentOutOfRangeException("wParam", "Invalid key state detected.");

            var pressedKey = (KeysEx)Marshal.ReadInt32(lParam);

            if (!CheckIsHookKeyCombination(pressedKey))
                return NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam);

            if (keyState == KeyState.Up)
            {
                keyReleased = true;
                return NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam);
            }

            // If AutoRepeat is on, always process hook; otherwise, only process hook if they key has been released and re-pressed.
            if (AutoRepeat || keyReleased)
            {
                keyReleased = false;
                OnPressed();
            }

            if (AllowPassThrough)
                return NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam);

            return new IntPtr(1);
        }
    }

    public class KeyboardLockDownKeyPressedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardLockDownKeyPressedEventArgs"/> class.
        /// </summary>
        /// <param name="keys">The keys that were pressed.</param>
        /// <param name="state">The state of the pressed keys.</param>
        /// <remarks></remarks>
        public KeyboardLockDownKeyPressedEventArgs(KeysEx keys, KeyState state)
        {
            Keys = keys;
            State = state;
        }

        /// <summary>
        /// Gets the keys that were pressed.
        /// </summary>
        public KeysEx Keys { get; private set; }

        /// <summary>
        /// Gets the state of the pressed keys.
        /// </summary>
        public KeyState State { get; private set; }
    }
}
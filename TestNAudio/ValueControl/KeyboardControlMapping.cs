using System.Windows.Input;

namespace TestNAudio.ValueControl
{
    public class KeyboardKeyControlMapping
    {

    }

    public enum KeyboardLineControlMode
    {
        Linear,
        ThreeThirds
    }

    public class KeyboardLineControlMapping
    {
        public ModifierKeys ModifierKeys { get; set; }

        public KeyboardLineControlMode ControlMode { get; set; }

        internal Key LowUpKey { get; private set; }

        internal Key LowDownKey { get; private set; }

        internal Key HighUpKey { get; private set; }

        internal Key HighDownKey { get; private set; }

        public KeyboardLineControlMapping(Key highUpKey = Key.None, Key lowUpKey = Key.None,
            Key lowDownKey = Key.None, Key highDownKey = Key.None, ModifierKeys modifier = ModifierKeys.None)
        {
            LowUpKey = lowUpKey;
            LowDownKey = lowDownKey;
            HighUpKey = highUpKey;
            HighDownKey = highDownKey;
            ModifierKeys = modifier;
        }

        public static KeyboardLineControlMapping AQ_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D1, Key.A, Key.Q, Key.W, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping ZS_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D2, Key.Z, Key.S, Key.X, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping ED_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D3, Key.E, Key.D, Key.C, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping RF_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D4, Key.R, Key.F, Key.V, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping TG_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D5, Key.T, Key.G, Key.B, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping YH_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D6, Key.Y, Key.H, Key.N, modifier);
        }

        public static KeyboardLineControlMapping UJ_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D7, Key.U, Key.J, Key.OemComma, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping IK_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D8, Key.I, Key.K, Key.OemPeriod, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping OL_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D9, Key.O, Key.L, Key.OemQuestion, modifier)
            {
                ControlMode = controlMode
            };
        }

        public static KeyboardLineControlMapping PM_KeyboardLine(
            KeyboardLineControlMode controlMode = KeyboardLineControlMode.Linear,
            ModifierKeys modifier = ModifierKeys.None)
        {
            return new KeyboardLineControlMapping(Key.D0, Key.P, Key.M, Key.Oem8, modifier)
            {
                ControlMode = controlMode
            };
        }
    }
}

using System.Windows.Input;

namespace TestNAudio.ValueControl
{
    public enum KeyboardControlMode
    {
        Linear,
        ThreeThirds
    }

    public class KeyboardControlMapping
    {
        public ModifierKeys ModifierKeys { get; set; }

        public KeyboardControlMode ControlMode { get; set; }

        internal Key LowUpKey { get; private set; }

        internal Key LowDownKey { get; private set; }

        internal Key HighUpKey { get; private set; }

        internal Key HighDownKey { get; private set; }

        public KeyboardControlMapping(Key highUpKey = Key.None, Key lowUpKey = Key.None, Key lowDownKey = Key.None, Key highDownKey = Key.None)
        {
            this.LowUpKey = lowUpKey;
            this.LowDownKey = lowDownKey;
            this.HighUpKey = highUpKey;
            this.HighDownKey = highDownKey;
        }

        public static KeyboardControlMapping AQ_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D1, Key.A, Key.Q, Key.W)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping ZS_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D2, Key.Z, Key.S, Key.X)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping ED_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D3, Key.E, Key.D, Key.C)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping RF_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D4, Key.R, Key.F, Key.V)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping TG_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D5, Key.T, Key.G, Key.B)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping YH_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D6, Key.Y, Key.H, Key.N);
        }

        public static KeyboardControlMapping UJ_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D7, Key.U, Key.J, Key.OemComma)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping IK_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D8, Key.I, Key.K, Key.OemPeriod)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping OL_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D9, Key.O, Key.L, Key.OemQuestion)
                       {
                           ControlMode = controlMode
                       };
        }

        public static KeyboardControlMapping PM_KeyboardLine(KeyboardControlMode controlMode = KeyboardControlMode.Linear)
        {
            return new KeyboardControlMapping(Key.D0, Key.P, Key.M, Key.Oem8)
                       {
                           ControlMode = controlMode
                       };
        }
    }
}

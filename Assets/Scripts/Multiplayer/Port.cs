using System;

namespace Assets.Scripts.Multiplayer
{
    public class Port
    {
        public int PortNumber { get; private set; }
        private const int MAX_VALUE = 65535;
        public Port(int PortNumber)
        {
            if (PortNumber > MAX_VALUE)
            {
                throw new TypeInitializationException(typeof(Port).FullName, new Exception($"Port value out of port maximum value ({MAX_VALUE})"));
            }
            else
            {
                this.PortNumber = PortNumber;
            }
        }

        public static implicit operator int(Port v)
        {
            return v.PortNumber; // allows you to use it like an int
        }
    }
}

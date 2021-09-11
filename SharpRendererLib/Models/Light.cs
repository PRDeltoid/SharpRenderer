using System.Numerics;

namespace SharpRendererLib.Models
{
    public class Light
    {
        private readonly Vector3 _value;

        private Light(Vector3 value)
        {
            _value = value;
        }
        
        public static implicit operator Light(Vector3 value)
        {
            return new Light(value);
        }
        
        public static implicit operator Vector3(Light light)
        {
            return light._value;
        }
    }
}
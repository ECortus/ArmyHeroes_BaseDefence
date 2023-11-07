// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("rMPOG74MRwkuxEDbTrqrv3l3PjKi6ZIWUAq49niR1/Vw4Y2+LC4eahiblZqqGJuQmBibm5pUjvPb6UgnJLY1r44ahtbmnSBhpUOLrJWnVczEKSZlRStCayscMlWRpzFsjk+a1y+K93LZmZ+Rm8/3NfC5eggMdK0rQvCGJDqUdVvel1EgJY0R+X7JPEFovziDjIhSamjIDxLIo11oqTf8s2cV/vi4SvlaJWUl57a8qgJoXRFYAn0sNwv6kgDrOq703y0A0vf2Xxf7lj+phL9lVyWuGA8KMCmBW4AaAc62IPFhzRvYbwXZMWoZIWCymHybqhibuKqXnJOwHNIcbZebm5ufmpkP9WNtz0XwKSmGL6RVG2w00+AfTMFDZQBoD3TFH5iZ5zqb");
        private static int[] order = new int[] { 8,5,5,3,5,10,9,12,12,11,10,13,12,13,14 };
        private static int key = 154;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}

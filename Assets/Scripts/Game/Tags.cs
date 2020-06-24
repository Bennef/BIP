using UnityEngine;

namespace Scripts.Game
{
    public class Tags : MonoBehaviour
    {
        // This file stores all the tags. Whenever checking a tag, reference this script. Ensures that if we need to change a tag in future, we only need to change it in one file.
        public static string UI = "UI";
        public static string Player = "Player";
        public static string Enemies = "Enemy";
        public static string levelBounds = "Level Bounds";
        public static string fader = "Fader";
        public static string WallLaser = "Wall Laser";
        public static string Pickup = "Pickup";
        public static string Waypoint = "Waypoint";
        public static string Laser = "Laser";
        public static string Jammer = "Jammer";
        public static string EMP = "EMP";
        public static string Key = "Key";
        public static string Moveable = "Moveable";
        public static string MovingPlatform = "Moving Platform";
        public static string Buddy = "Buddy";
    }
}
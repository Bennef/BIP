using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Scripts.Game.Level_Dynamics
{
    public class PathDefinition : MonoBehaviour
    {
        private Transform[] _points;

        public IEnumerator<Transform> GetPathsEnumerator()
        {
            if (_points == null || _points.Length < 1)
                yield break;

            int direction = 1;
            int index = 0;
            // Infinite loop here we come
            while (true)
            {
                // Without this, the program will crash.
                // Yield will Yield execution to the object running this enumerator
                // until it is called again
                // So it's not REALLY an infinite loop, if the object stops looking for the next point
                // this will terminate
                yield return _points[index];

                if (_points.Length == 1)
                    continue;
                if (index <= 0)
                    direction = 1; // Move towards next point
                else if (index >= _points.Length - 1)
                    direction = -1; // move towards previous point
                index += direction;
            }
        }

        public void OnDrawGizmos()
        {
            // This function runs in the Unity Editor, and draws the path in the Scene View.
            if (_points == null || _points.Length < 2)
            {
                // Make sure we have enough _points to actually make a line, otherwise return
                return;
            }
            var Points = _points.Where(t => t != null).ToList();
            if (Points.Count < 2)
            {
                return;
            }
            // Draw that line
            for (int i = 1; i < Points.Count; i++)
                Gizmos.DrawLine(Points[i - 1].position, Points[i].position);
        }
    }
}
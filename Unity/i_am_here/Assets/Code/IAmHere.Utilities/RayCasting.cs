using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace IAmHere.Utilities
{
    public struct RayInfo {
        public Vector2 rayDirection;
        public float rayAngle;
        public float rayLenght;
        public Collider2D collider;

        public RayInfo(Vector3 _rayDirection, float _rayAngle, float _rayLenght, Collider2D _collider) {
            rayDirection = _rayDirection;
            rayAngle = _rayAngle;
            rayLenght = _rayLenght;
            collider = _collider;
        }
    };
    
    public class RayCasting : MonoBehaviour
    {
        
        private static int objectsLayerMask = (1 << 10) | (1 << 11);
        public static Vector2 AngleToDir(float angle) {
            return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        }
        
        public static List<RayInfo> CastRays(Vector2 pos, int numRays, bool debugRay)
        {
            List<RayInfo> rays = new List<RayInfo>();
            if (numRays <= 0)
            {
                Assert.IsFalse(false, "Num of rays needs to be larger than 0!");
                return rays;           
            }

            const float fullCircle = 360.0f;
            float angleIncrement = fullCircle / numRays;

            for (float angle = 0; angle < fullCircle; angle += angleIncrement)
            {
                Vector2 rayDirection = AngleToDir(angle);
                // TODO(Rok Kos): DO Normalization with the width and height of the level
                float rayLenght = float.MaxValue;


                RaycastHit2D hit = Physics2D.Raycast(pos, rayDirection, rayLenght, objectsLayerMask);
                if (hit.collider != null)
                {
                    rayLenght = hit.distance;
                }

                RayInfo ray = new RayInfo(rayDirection, angle, rayLenght, hit.collider);
                rays.Add(ray);
            }

            if (debugRay) {
                foreach (RayInfo ri in rays){
                    Debug.DrawRay(pos, ri.rayDirection * ri.rayLenght, Color.red);
                }
            }

            return rays;
        }

        public static Collider2D CastRayToHit(Vector2 pos, Vector2 goal, bool debugRay)
        {
            Collider2D collider2D = null;
            float rayLenght = float.MaxValue;
            Vector2 rayDirection = (goal - pos).normalized;

            RaycastHit2D hit = Physics2D.Raycast(pos, rayDirection, rayLenght, objectsLayerMask);
            if (hit.collider != null)
            {
                rayLenght = hit.distance;
                collider2D = hit.collider;
            }
            

            if (debugRay)
            {
                Debug.DrawRay(pos, rayDirection * rayLenght, Color.red);
            }

            return collider2D;
        }
    }
}
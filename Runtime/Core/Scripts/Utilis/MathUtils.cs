using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    /// <summary>
    /// General math and geometry utilities commonly used in Unity gameplay, AI, and UI systems.
    /// Focused on spatial queries, smoothing, and normalization.
    /// </summary>
    public static class MathUtils
    {
        #region Value Remapping & Normalization

        /// <summary>
        /// Remaps a value from one range to another.
        /// Example: Remap(5, 0, 10, 0, 1) → 0.5f
        /// </summary>
        public static float Remap(float value, float inMin, float inMax, float outMin, float outMax)
        {
            if (Mathf.Approximately(inMax, inMin)) return outMin;
            float t = Mathf.InverseLerp(inMin, inMax, value);
            return Mathf.Lerp(outMin, outMax, t);
        }

        /// <summary>
        /// Maps a value to [0,1] range safely, returning 0 if min == max.
        /// </summary>
        public static float Normalize01(float value, float min, float max)
        {
            if (Mathf.Approximately(max, min)) return 0f;
            return Mathf.Clamp01((value - min) / (max - min));
        }

        /// <summary>
        /// Returns a smoothed (eased) interpolation value for more natural motion.
        /// </summary>
        public static float Smooth01(float t) => t * t * (3f - 2f * t);

        #endregion

        #region Angles & Directions

        /// <summary>
        /// Returns the signed angle (-180 to 180) between two directions, relative to an up axis.
        /// </summary>
        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 up)
        {
            return Mathf.Atan2(
                Vector3.Dot(up, Vector3.Cross(from, to)),
                Vector3.Dot(from, to)
            ) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Clamp an angle between min and max degrees, handling wraparound.
        /// </summary>
        public static float ClampAngle(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f;
            return Mathf.Clamp(angle, min, max);
        }

        /// <summary>
        /// Checks if a target is within a field of view cone.
        /// </summary>
        public static bool IsWithinViewAngle(Vector3 origin, Vector3 forward, Vector3 target, float viewAngle)
        {
            Vector3 dirToTarget = (target - origin).normalized;
            float angle = Vector3.Angle(forward, dirToTarget);
            return angle <= viewAngle * 0.5f;
        }

        /// <summary>
        /// Returns a normalized direction vector from one point to another (zero if same position).
        /// </summary>
        public static Vector3 DirectionTo(Vector3 from, Vector3 to)
        {
            Vector3 dir = to - from;
            return dir.sqrMagnitude > 0.0001f ? dir.normalized : Vector3.zero;
        }

        #endregion

        #region Distance & Spatial Queries

        /// <summary>
        /// Returns squared distance between two points (avoids sqrt for performance).
        /// </summary>
        public static float SqrDistance(Vector3 a, Vector3 b)
            => (a - b).sqrMagnitude;

        /// <summary>
        /// Returns true if a position is within a radius of another position.
        /// </summary>
        public static bool IsWithinRadius(Vector3 origin, Vector3 point, float radius)
            => SqrDistance(origin, point) <= radius * radius;

        /// <summary>
        /// Linearly scales a value based on distance (1 near, 0 at max distance).
        /// Useful for sound falloff, detection sensitivity, etc.
        /// </summary>
        public static float DistanceFalloff(Vector3 a, Vector3 b, float maxDistance)
        {
            float dist = Vector3.Distance(a, b);
            return Mathf.Clamp01(1f - dist / maxDistance);
        }

        /// <summary>
        /// Returns a random point within a circle projected on the XZ plane.
        /// Useful for patrol targets or item spawns.
        /// </summary>
        public static Vector3 RandomPointInCircle(Vector3 center, float radius)
        {
            Vector2 rand = Random.insideUnitCircle * radius;
            return new Vector3(center.x + rand.x, center.y, center.z + rand.y);
        }

        /// <summary>
        /// Returns a random point within a sphere.
        /// </summary>
        public static Vector3 RandomPointInSphere(Vector3 center, float radius)
            => center + Random.insideUnitSphere * radius;

        #endregion

        #region Smoothing, Lerp & Damping

        /// <summary>
        /// Smoothly damp a float toward a target value (with velocity tracking).
        /// </summary>
        public static float SmoothDamp(float current, float target, ref float velocity, float smoothTime)
            => Mathf.SmoothDamp(current, target, ref velocity, smoothTime);

        /// <summary>
        /// Smoothly damp a Vector3 toward a target value (with velocity tracking).
        /// </summary>
        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 velocity, float smoothTime)
            => Vector3.SmoothDamp(current, target, ref velocity, smoothTime);

        /// <summary>
        /// Smoothly interpolate rotation between two quaternions using SmoothDamp-like behavior.
        /// Useful for AI head-turning or flashlight rotation.
        /// </summary>
        public static Quaternion SmoothDampRotation(Quaternion current, Quaternion target, ref float velocity,
            float smoothTime)
        {
            float angle = Quaternion.Angle(current, target);
            if (Mathf.Approximately(angle, 0f))
                return target;

            float t = Mathf.SmoothDampAngle(0f, angle, ref velocity, smoothTime) / angle;
            return Quaternion.Slerp(current, target, t);
        }

        /// <summary>
        /// Returns a smoothed oscillating value (sin wave) — great for flickering lights, idle motion, etc.
        /// </summary>
        public static float Oscillate(float speed, float amplitude = 1f)
            => Mathf.Sin(Time.time * speed) * amplitude;

        #endregion

        #region Line of Sight & Raycasting

        /// <summary>
        /// Returns true if there’s line of sight between two points (Physics.Raycast check).
        /// </summary>
        public static bool HasLineOfSight(Vector3 origin, Vector3 target, LayerMask obstacleMask, float maxDistance)
        {
            Vector3 dir = target - origin;
            if (Physics.Raycast(origin, dir.normalized, out var hit, maxDistance, obstacleMask))
                return hit.transform == null || hit.transform.position == target;
            return true;
        }

        /// <summary>
        /// Returns distance to obstacle or maxDistance if clear line.
        /// </summary>
        public static float DistanceToObstacle(Vector3 origin, Vector3 direction, LayerMask obstacleMask,
            float maxDistance)
        {
            return Physics.Raycast(origin, direction, out var hit, maxDistance, obstacleMask)
                ? hit.distance
                : maxDistance;
        }

        #endregion

        #region Utility

        /// <summary>
        /// Returns a random sign (-1 or +1).
        /// </summary>
        public static int RandomSign() => Random.value < 0.5f ? -1 : 1;

        /// <summary>
        /// Snap a value to the nearest increment (useful for grid snapping).
        /// </summary>
        public static float Snap(float value, float increment)
            => Mathf.Round(value / increment) * increment;

        /// <summary>
        /// Returns a point clamped inside bounds.
        /// </summary>
        public static Vector3 ClampToBounds(Vector3 point, Bounds bounds)
        {
            return new Vector3(
                Mathf.Clamp(point.x, bounds.min.x, bounds.max.x),
                Mathf.Clamp(point.y, bounds.min.y, bounds.max.y),
                Mathf.Clamp(point.z, bounds.min.z, bounds.max.z)
            );
        }

        /// <summary>
        /// Wrap a float between min and max (useful for angles and looping indices).
        /// </summary>
        public static float Wrap(float value, float min, float max)
        {
            float range = max - min;
            if (range == 0) return min;
            return value - range * Mathf.Floor((value - min) / range);
        }

        /// <summary>
        /// Get a random Vector3 inside specified ranges per axis.
        /// </summary>
        public static Vector3 RandomVector3(Vector3 min, Vector3 max)
        {
            return new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
            );
        }

        #endregion
    }
}
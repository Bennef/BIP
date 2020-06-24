using UnityEngine;

namespace Scripts.Animation
{
    public class IKSolver : MonoBehaviour
    {
        // IK will stop if the end effector gets within this distance of the target
        private const float IK_POS_THRESH = 0.25f;
        // IK will only run this amount of times, maximum, per frame.
        private const int MAX_IK_TRIES = 10;

        [System.Serializable]
        public class JointEntity
        {
            public Transform Joint;
            public AngleRestriction AngleRestrictionRange;
            internal Quaternion _initialRotation;
        }

        /// <summary>
        /// Rotation AngleRestriction values in degrees (from -180 to 180).
        /// </summary>
        [System.Serializable]
        public class AngleRestriction
        {
            public bool xAxis = false;
            public float xMin = -180f;
            public float xMax = 180.0f;
            public bool yAxis = false;
            public float yMin = -180f;
            public float yMax = 180f;
            public bool zAxis = false;
            public float zMin = 180f;
            public float zMax = 180f;
        }

        public bool IsActive = true;
        public Vector3 Target;
        public JointEntity[] JointEntities;

        public bool IsDamping = false;
        public float DampingMax = 0.5f;

        void Start()
        {
            // Set up the initial rotation for every joint in the bone chain
            for (int i = 0; i < JointEntities.Length; i++)
            {
                JointEntities[i]._initialRotation = JointEntities[i].Joint.localRotation;
            }
        }

        void LateUpdate()
        {
            // We run IK in LateUpdate, so it runs after the animation every frame.
            // We don't solve IK if our target is 0,0,0
            if (IsActive && Target != Vector3.zero)
            {
                Solve();
            }
        }

        void Solve()
        {
            // The meat and potatoes of the IK solver.
            // The endEffector is the last joint in the chain, so the "hand" or "foot".
            // This is the bone that tries to reach the target. The rest of them are rotated based on this.
            Transform endEffector = JointEntities[JointEntities.Length - 1].Joint;
            // rootPosition is the position of the current link in the bone chain.
            Vector3 rootPosition;

            Vector3 targetVector = Vector3.zero;
            Vector3 currentVector = Vector3.zero;
            Vector3 crossResult = Vector3.zero;

            float cosineAngle, turnAngle;

            // IK Starts at the last link in the chain, and works backwards
            int link = JointEntities.Length - 1;
            int tries = 0;

            // endEffector checks if it is already close enough to the target
            // While the distance between the endEffector.position and the Target is less than the Threshold,
            // and we haven't exceeded our max number of tries.
            while (tries++ < MAX_IK_TRIES && Vector3.Distance(endEffector.position, Target) > IK_POS_THRESH)
            {
                if (link < 0)
                {
                    // 0 would be our last link, if link gets below that, reset to first
                    link = JointEntities.Length - 1;
                }

                // rootPosition is the position of the current link in the bone chain.
                // endEffector.position is the position of the endEffector.
                rootPosition = JointEntities[link].Joint.position;

                // Create a Vector between the current end position and root position.
                // We're gonna make a triangle!
                currentVector = endEffector.position - rootPosition;
                // Create the desired effector position vector.
                targetVector = Target - rootPosition;

                // Normalize the vectors.
                currentVector.Normalize();
                targetVector.Normalize();

                // The Dot product of the two gives us the cosine of the desired angle
                cosineAngle = Vector3.Dot(currentVector, targetVector);

                // If the Dot product returns 1.0, we don't rotate as our desired angle is 0 degrees.
                if (cosineAngle < 0.99999f)
                {
                    // We do a Vector3.Cross to check which way to rotate the bone.
                    crossResult = Vector3.Cross(currentVector, targetVector);
                    // and normalize it.
                    crossResult.Normalize();
                    // Turn Angle is the Arc Cosine of the cosineAngle.
                    turnAngle = Mathf.Acos(cosineAngle);
                    // Apply damping. Smaller numbers mean less drastic rotations.
                    if (IsDamping)
                    {
                        if (turnAngle > DampingMax)
                        {
                            turnAngle = DampingMax;
                        }
                    }
                    // We convert turnAngle into Degrees, from Radeons.
                    turnAngle = turnAngle * Mathf.Rad2Deg;

                    // We rotate the current link in the chain.
                    JointEntities[link].Joint.rotation = Quaternion.AngleAxis(turnAngle, crossResult) * JointEntities[link].Joint.rotation;
                    // and make sure that the new angle does not break the restrictions set upon the joint.
                    CheckAngleRestrictions(JointEntities[link]);
                }
                // Iterate downwards through the links.
                link--;
            }
        }

        /// <summary>
        /// Checks the angle restrictions.
        /// </summary>
        /// <param name="jointEntity">Joint entity</param>
        void CheckAngleRestrictions(JointEntity jointEntity)
        {
            Vector3 euler = jointEntity.Joint.localRotation.eulerAngles;

            if (jointEntity.AngleRestrictionRange.xAxis)
            {
                if (euler.x > 180f)
                    euler.x -= 360f;
                euler.x = Mathf.Clamp(euler.x, jointEntity.AngleRestrictionRange.xMin, jointEntity.AngleRestrictionRange.xMax);
            }

            if (jointEntity.AngleRestrictionRange.yAxis)
            {
                if (euler.y > 180f)
                    euler.y -= 360f;
                euler.y = Mathf.Clamp(euler.y, jointEntity.AngleRestrictionRange.yMin, jointEntity.AngleRestrictionRange.yMax);
            }

            if (jointEntity.AngleRestrictionRange.zAxis)
            {
                if (euler.z > 180f)
                    euler.z -= 360f;
                euler.z = Mathf.Clamp(euler.z, jointEntity.AngleRestrictionRange.zMin, jointEntity.AngleRestrictionRange.zMax);
            }

            jointEntity.Joint.localEulerAngles = euler;
        }

        /// <summary>
        /// Reset joints position
        /// </summary>
        public void ResetJoints()
        {
            foreach (JointEntity jointEntity in JointEntities)
            {
                jointEntity.Joint.localRotation = jointEntity._initialRotation;
            }
        }

        public void SetTarget(Vector3 value)
        {
            Target = value;
        }
    }
}
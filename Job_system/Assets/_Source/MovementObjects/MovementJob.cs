using UnityEngine;
using UnityEngine.Jobs;

namespace _Source.MovementObjects
{
    public struct MovementJob : IJobParallelForTransform
    {
        public float Speed;
        public float DeltaTime;
        public Vector3 CenterPoint;

        public void Execute(int index, TransformAccess transform)
        {
            float radius = Vector3.Distance(CenterPoint, transform.position);
            float angle = Mathf.Atan2(transform.position.z - CenterPoint.z, transform.position.x - CenterPoint.x) + Speed * DeltaTime;
            transform.position = new Vector3(CenterPoint.x + radius * Mathf.Cos(angle), 0, CenterPoint.z + radius * Mathf.Sin(angle));
        }
    }
}
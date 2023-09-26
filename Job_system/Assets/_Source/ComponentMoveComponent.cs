using System;
using System.Collections.Generic;
using _Source.MovementObjects;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace _Source
{
    public class ComponentMoveComponent : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        [SerializeField] private Transform centerPoint;
        [SerializeField] private int count;
        [SerializeField] private float speed;
        [SerializeField] private bool isJob;
        [SerializeField] private LogCompilerComponent logCompiler;

        private List<Transform> _poolTransforms;
        private TransformAccessArray _transforms;
        private MovementJob _movementJob;

        private void Awake()
        {
            logCompiler.SetLenghtArray(count);
        }

        private void Start()
        {
            _poolTransforms = new List<Transform>(count);
            _movementJob = new MovementJob
            {
                Speed = speed,
                CenterPoint = centerPoint.position
            };

            for (int i = 0; i < count; i++)
            {
                float pointX = Random.Range(-20, 20);
                float pointZ = Random.Range(-20, 20);
                var obj = Instantiate(body, new Vector3(pointX, 0, pointZ), Quaternion.identity);
                _poolTransforms.Add(obj.transform);
            }
            _transforms = new TransformAccessArray(_poolTransforms.ToArray());
        }

        private void Update()
        {
            if (isJob)
            {
                MoveWithJob();
            }
            else
            {
                MoveWithoutJob();
            }
        }

        private void MoveWithJob()
        {
            _movementJob.DeltaTime = Time.deltaTime;
            var handle = _movementJob.Schedule(_transforms);
            handle.Complete();
        }

        private void MoveWithoutJob()
        {
            foreach (var obj in _poolTransforms)
            {
                var position = obj.position;
                var position1 = centerPoint.position;
                float radius = Vector3.Distance(position1, position);
                float angle = Mathf.Atan2(position.z - position1.z, position.x - position1.x) + speed * Time.deltaTime;
                position = new Vector3(position1.x + radius * Mathf.Cos(angle), 0, position1.z + radius * Mathf.Sin(angle));
                obj.position = position;
            }
        }

        private void OnDestroy()
        {
            _transforms.Dispose();
        }
    }
}

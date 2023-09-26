using Unity.Jobs;
using UnityEngine;

namespace _Source.MovementObjects
{
    public class LogCompilerComponent : MonoBehaviour
    {
        public const float TimeInvoke = 3f;
        private int _countTransforms;

        private LogMatchJob _logMatch;

        private void Awake()
        {
            _logMatch = new LogMatchJob();
        }

        private void Start()
        {
            InvokeRepeating("CompileLog", 0,TimeInvoke);
        }

        private void CompileLog()
        {
            float randomNumber = Random.Range(1f, 100f);
            _logMatch.Number = randomNumber;
            JobHandle jobHandle = _logMatch.Schedule();
            jobHandle.Complete();
        }
    }
}
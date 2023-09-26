using Unity.Jobs;
using UnityEngine;

namespace _Source.MovementObjects
{
    public class LogCompilerComponent : MonoBehaviour
    {
        [SerializeField] private float timeInvoke;
        private int _countTransforms;

        private LogMatchJob _logMatch;

        public void SetLenghtArray(int count)
        {
            _countTransforms = count;
            Debug.Log(_countTransforms);
        }

        private void Awake()
        {
            _logMatch = new LogMatchJob();
        }

        private void Start()
        {
            InvokeRepeating("CompileLog", 0,timeInvoke);
        }

        private void CompileLog()
        {
            for (int i = 0; i < _countTransforms; i++)
            {
                float randomNumber = Random.Range(1f, 100f);
                _logMatch.Number = randomNumber;
                JobHandle jobHandle = _logMatch.Schedule();
                jobHandle.Complete();
            }
        }
    }
}
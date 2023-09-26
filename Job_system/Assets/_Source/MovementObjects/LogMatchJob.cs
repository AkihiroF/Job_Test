using System;
using Unity.Jobs;
using UnityEngine;

namespace _Source.MovementObjects
{
    public struct LogMatchJob : IJob
    {
        public float Number;
        public void Execute()
        {
            Debug.Log(Math.Log(Number));
        }
    }
}
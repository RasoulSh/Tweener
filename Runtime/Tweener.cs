using System.Collections;
using System.Collections.Generic;
using TweenerSystem.Enums;
using UnityEngine;

namespace TweenerSystem
{
    public abstract class Tweener : MonoBehaviour
    {
        [SerializeField] public TweenerDelegation delegation;
        public abstract float TotalDuration { get; }
        private List<Coroutine> currentRoutines;
        protected List<Coroutine> CurrentRoutines => currentRoutines ??= new List<Coroutine>();
        public TweenerDelegation Delegation => delegation;

        public void Play(TweenerPlayOrder playOrder, TweenerDirection direction)
        {
            switch (playOrder)
            {
                case TweenerPlayOrder.Once:
                    Play(direction);
                    break;
                case TweenerPlayOrder.Loop:
                    PlayLoop(direction);
                    break;
                case TweenerPlayOrder.PingPong:
                    PlayPingPong();
                    break;
            }
        }

        public void Play(bool isForward, bool ignoreAnimate = false)
        {
            Play(isForward ? TweenerDirection.Forward : TweenerDirection.Backward,
                ignoreAnimate);
        }

        public void Play(TweenerDirection direction, bool ignoreAnimate = false)
        {
            if (ignoreAnimate || gameObject.activeInHierarchy == false)
            {
                if (direction == TweenerDirection.Forward)
                {
                    GotoLastFrame();
                }
                else
                {
                    GotoFirstFrame();
                }
                delegation.InvokeFinishDelegates(direction);
            }
            else
            {
                StopCurrentRoutines();
                CurrentRoutines.Add(StartCoroutine(PlayRoutine(direction)));
            }
        }
        
        [ContextMenu("Stop")]
        public void Stop() => StopCurrentRoutines();
        
        [ContextMenu("PlayForward")]
        public void PlayForward() => Play(true);
        
        [ContextMenu("PlayBackward")]
        public void PlayBackward() => Play(false);
        
        [ContextMenu("PlayLoopForward")]
        public void PlayLoopForward() => PlayLoop(TweenerDirection.Forward);
        
        [ContextMenu("PlayLoopBackward")]
        public void PlayLoopBackward() => PlayLoop(TweenerDirection.Backward);
        
        [ContextMenu("PlayPingPongForward")]
        public void PlayPingPongForward() => PlayPingPong(TweenerDirection.Forward);
        
        [ContextMenu("PlayPingPongBackward")]
        public void PlayPingPongBackward() => PlayPingPong(TweenerDirection.Backward);

        public void PlayLoop(TweenerDirection direction)
        {
            StopCurrentRoutines();
            CurrentRoutines.Add(StartCoroutine(PlayLoopRoutine(direction)));
        }

        private IEnumerator PlayLoopRoutine(TweenerDirection direction)
        {
            while (true)
            {
                var playCoroutine = StartCoroutine(PlayRoutine(direction));
                CurrentRoutines.Add(playCoroutine);
                yield return playCoroutine;
            }
        }

        public void PlayPingPong() => PlayPingPong(TweenerDirection.Forward);
        public void PlayPingPong(TweenerDirection direction)
        {
            StopCurrentRoutines();
            CurrentRoutines.Add(StartCoroutine(PlayPingPongRoutine(direction)));
        }
        
        private IEnumerator PlayPingPongRoutine(TweenerDirection direction)
        {
            while (true)
            {
                var firstPlayCoroutine = StartCoroutine(PlayRoutine(direction));
                CurrentRoutines.Add(firstPlayCoroutine);
                yield return firstPlayCoroutine;
                var secondPlayCoroutine = StartCoroutine(
                    PlayRoutine(direction == TweenerDirection.Forward
                        ? TweenerDirection.Backward
                        : TweenerDirection.Forward));
                CurrentRoutines.Add(secondPlayCoroutine);
                yield return secondPlayCoroutine;
            }
        }

        internal abstract IEnumerator PlayRoutine(TweenerDirection direction);
        internal abstract void GotoFirstFrame();
        internal abstract void GotoLastFrame();
        
        private void StopCurrentRoutines()
        {
            CurrentRoutines.ForEach(StopCoroutine);
            CurrentRoutines.Clear();
        }
    }
}

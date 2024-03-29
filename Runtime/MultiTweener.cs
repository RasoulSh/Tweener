﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TweenerSystem.Enums;

namespace TweenerSystem
{
    public class MultiTweener : Tweener
    {
        [SerializeField] private List<Tweener> tweeners;
        [SerializeField] private MultiTweenerPlayMode playMode;
        [field: SerializeField] public bool RealTime { get; set; } = true;
        public List<Tweener> Tweeners => tweeners;

        public override float TotalDuration
        {
            get
            {
                switch (playMode)
                {
                    case MultiTweenerPlayMode.Simultaneous:
                        return tweeners.Max(tweener => tweener.TotalDuration);
                    case MultiTweenerPlayMode.Queue:
                        return tweeners.Sum(tweener => tweener.TotalDuration);
                    default:
                        return 0f;
                }
            }
        }
        internal override IEnumerator PlayRoutine(TweenerDirection direction)
        {
            Delegation.InvokeStartDelegates(direction);
            switch (playMode)
            {
                case MultiTweenerPlayMode.Simultaneous:
                    tweeners.ForEach(tweener =>
                    {
                        CurrentRoutines.Add(StartCoroutine(tweener.PlayRoutine(direction)));
                    });
                    if (RealTime)
                        yield return new WaitForSecondsRealtime(TotalDuration);
                    else
                        yield return new WaitForSeconds(TotalDuration);
                    break;
                case MultiTweenerPlayMode.Queue:
                    foreach (var tweener in tweeners)
                    {
                        var playCoroutine = StartCoroutine(tweener.PlayRoutine(direction));
                        CurrentRoutines.Add(playCoroutine);
                        yield return playCoroutine;
                    }
                    break;
            }
            Delegation.InvokeFinishDelegates(direction);
        }

        internal override void GotoFirstFrame()
        {
            tweeners.ForEach(tweener => tweener.GotoFirstFrame());
        }

        internal override void GotoLastFrame()
        {
            tweeners.ForEach(tweener => tweener.GotoLastFrame());
        }
    }
}
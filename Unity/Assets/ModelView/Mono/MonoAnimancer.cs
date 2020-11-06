using Animancer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public enum AnimationKey
    {
        Idle,
        Run,
        Atk,
        Hurt,
        Other,
    }
    [Serializable]
    public struct AnimationClipInfo
    {
        public AnimationKey Key;

        public AnimationClip AnimationClip;

    }
    public class MonoAnimancer : AnimancerComponent
    {
        [SerializeField]
        private AnimationClipInfo[] _Clips;

        private Dictionary<AnimationKey, AnimationClip> _AnimationClipDic;

        private AnimationClip _AnimationClip;
        public AnimationClip CurrClip => _AnimationClip;

        private bool isAtk;
        protected override void OnEnable()
        {
            base.OnEnable();
            _AnimationClipDic = new Dictionary<AnimationKey, AnimationClip>();
            foreach (var item in _Clips)
            {
                _AnimationClipDic[item.Key] = item.AnimationClip;
            }
            if (_AnimationClipDic.ContainsKey(AnimationKey.Other))
            {
                _AnimationClip = _AnimationClipDic[AnimationKey.Other];
            }
            else
                PlayIdle();
        }

        public void PlayIdle()
        {
            isAtk = false;
            _AnimationClip = _AnimationClipDic[AnimationKey.Idle];
            Play(_AnimationClip, 0.1f);

        }
        public AnimancerState PlayRun(float fadeDuration = 0.1f)
        {
            isAtk = false;
            _AnimationClip = _AnimationClipDic[AnimationKey.Run];
            var state = Play(_AnimationClip, fadeDuration);
            if (!_AnimationClip.isLooping)
            {
                state.Events.OnEnd = PlayIdle;
            }
            return state;
        }


        public AnimancerState PlayAtk(float fadeDuration = 0.1f)
        {
            isAtk = true;
            _AnimationClip = _AnimationClipDic[AnimationKey.Atk];
            var state = Play(_AnimationClip, fadeDuration);
            if (!_AnimationClip.isLooping)
            {
                state.Events.OnEnd = PlayIdle;
            }
            return state;
        }
        public AnimancerState PlayHurt(float fadeDuration = 0.1f)
        {
            if (isAtk) return States.Current;
           _AnimationClip = _AnimationClipDic[AnimationKey.Hurt];
            var state = Play(_AnimationClip, fadeDuration);
            if (!_AnimationClip.isLooping)
            {
                state.Events.OnEnd = PlayIdle;
            }
            return state;
        }
        public AnimancerState PlayOther(Action endEvent = null, float fadeDuration = 0.1f)
        {
            isAtk = false;
            _AnimationClip = _AnimationClipDic[AnimationKey.Other];
            var state = Play(_AnimationClip, fadeDuration);
            if (!_AnimationClip.isLooping)
            {
                state.Events.OnEnd = () =>
                {
                    state.Stop();
                    endEvent?.Invoke();
                };
            }
            return state;
        }
        
        public void SetClips(AnimationClipInfo[] arr)
        {
            _Clips = arr;
        }
        public AnimationClipInfo[] GetClips()
        {
            return _Clips;
        }
    }
}

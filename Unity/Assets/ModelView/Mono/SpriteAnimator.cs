using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine.U2D;

namespace ET
{
    public static class AnimatorIdGenerater
    {
        public static long GetId(string textureName,AinmationKey ainmationKey,Ainmation8DirectionKey ainmation8DirectionKey)
        {
            return textureName.GetHashCode()*100L + ((long)ainmationKey) * 10 + (long)ainmation8DirectionKey;
        }

    }
    public class SpriteAnimator : SerializedMonoBehaviour
    {
        public Texture2D texture2D;
        public AinmationKey aimpationKey;
        [ShowIf("aimpationKey", AinmationKey.Walk)]
        public Ainmation8DirectionKey ainmation8DirectionKey;
        public int framesPerSecond = 6;

        private List<Sprite> _frames;
        public bool loop = true;
        public delegate void OnLoopDel();
        public OnLoopDel onLoop;
        public bool useUnscaledDeltaTime;
        public bool destroyOnLoop = false;
        private bool isActive = true;
        private float timer;
        private float timerMax;
        private int currentFrame;
        private SpriteRenderer spriteRenderer;



        private void Awake()
        {
            timerMax = 1f / framesPerSecond;
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
            //string path = $"Assets/Download/Config/Sprites/{texture2D.name}_{aimpationKey}_{ainmation8DirectionKey}.json";
            ////var file = ResourceHelper.LoadAsset<TextAsset>(path);
            //var file = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            //var spriteInfoList = MongoHelper.FromJson<List<SpriteInfo>>(file.text);
            _frames = new List<Sprite>();
            //foreach (var item in spriteInfoList)
            //{
            //    Sprite sprite = Sprite.Create(texture2D, new Rect(item.x, item.y, item.width, item.height), new Vector2(0.5f, 0.5f));
            //    _frames.Add(sprite);
            //}
            if (_frames .Count>0)
            {
                spriteRenderer.sprite = _frames[0];
            }
            else
            {
                isActive = false;
            }
        }

        private void Update()
        {
            if (!isActive) return;
            timer += useUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
            bool newFrame = false;
            while (timer >= timerMax)
            {
                timer -= timerMax;
                //Next frame
                currentFrame = (currentFrame + 1) % _frames.Count;
                newFrame = true;
                if (currentFrame == 0)
                {
                    //Looped
                    if (!loop)
                    {
                        isActive = false;
                        newFrame = false;
                    }
                    onLoop?.Invoke();
                    if (destroyOnLoop)
                    {
                        Destroy(gameObject);
                        return;
                    }
                }
            }
            if (newFrame)
            {
                spriteRenderer.sprite = _frames[currentFrame];
            }
        }

        public void PlayAnimation(AinmationKey animationKey, Vector3 vector3 = default)
        {
            switch (animationKey)
            {
                case AinmationKey.None:
                    break;
                case AinmationKey.Idle:
                    break;
                case AinmationKey.Walk:
                    var key = AnimationHelper.GetAnimation8DirectionKey(vector3);
                    this.aimpationKey = animationKey;
                    this.ainmation8DirectionKey = key;
                    break;
                case AinmationKey.Atk:
                    break;
                case AinmationKey.Dead:
                    break;
                default:
                    break;
            }
            Setup();
        }

        public void Setup()
        {
            timerMax = 1f / framesPerSecond;
            var spriteInfoConig = ConfigHelper.Get<SpriteInfoConig>(AnimatorIdGenerater.GetId(texture2D.name, aimpationKey, ainmation8DirectionKey));
            _frames.Clear();
            foreach (var item in spriteInfoConig.list)
            {
                Sprite sprite = SpriteComponent.Create(item, texture2D);
                _frames.Add(sprite);
            }
         if (_frames .Count>0)
            {
                PlayStart();
            }
            else
            {
                isActive = false;
            }
        }

        private void PlayStart()
        {
            timer = 0;
            currentFrame = 0;
            spriteRenderer.sprite = _frames[currentFrame];
            isActive = true;
        }

    }
}
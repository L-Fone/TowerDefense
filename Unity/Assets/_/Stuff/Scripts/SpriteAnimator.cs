//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using Sirenix.OdinInspector;
//using Sirenix.Utilities.Editor;
//using System.Collections.Generic;
//using UnityEditor;

//public class SpriteAnimator : SerializedMonoBehaviour
//{
//    public enum AinmationKey { None,Idle,Walk,Atk,Dead}
//    public enum Ainmation8DirectionKey {Left_Up,Left_Down,Right_Up,Right_Down}
//    public Texture2D texture2D;
//    public AinmationKey aimpationKey;
//    [ShowIf("aimpationKey",AinmationKey.Walk)]
//    public Ainmation8DirectionKey ainmation8DirectionKey;
//    public int firstFrame;
//    public int endFrame;
//    public int framesPerSecond = 30;
//    [Button()]
//    private void AutoFillSprite()
//    {
//        string assetPath = AssetDatabase.GetAssetPath(texture2D);
//        TextureImporter texImport = AssetImporter.GetAtPath(assetPath) as TextureImporter;
//        _frames = new List<Sprite>();
//        for (int i =firstFrame; i <= endFrame; i++)
//        {
//            var item = texImport.spritesheet[i];
//            Sprite sprite = Sprite.Create(texture2D, item.rect, item.pivot);
//            AnimationSprite
//        }
//    }

//    public Sprite[] frames;
//    private List<Sprite> _frames;
//    public bool loop = true;
//    public delegate void OnLoopDel();
//    public OnLoopDel onLoop;
//    public bool useUnscaledDeltaTime;
//    public bool destroyOnLoop = false;
//    private bool isActive = true;
//    private float timer;
//    private float timerMax;
//    private int currentFrame;
//    private SpriteRenderer spriteRenderer;

//    private void Awake()
//    {
//        timerMax = 1f / framesPerSecond;
//        spriteRenderer = transform.GetComponent<SpriteRenderer>();
//        if (frames != null)
//        {
//            //_frames = new List<Sprite>();
//            //for (int i = 0; i < frames.Length; i++)
//            //{
//            //    if (i >= firstFrame && i <= endFrame)
//            //        _frames.Add(frames[i]);
//            //}
//            spriteRenderer.sprite = _frames[0];
//        }
//        else
//        {
//            isActive = false;
//        }
//    }

//    private void Update()
//    {
//        if (!isActive) return;
//        timer += useUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
//        bool newFrame = false;
//        while (timer >= timerMax)
//        {
//            timer -= timerMax;
//            //Next frame
//            currentFrame = (currentFrame + 1) % _frames.Count;
//            newFrame = true;
//            if (currentFrame == 0)
//            {
//                //Looped
//                if (!loop)
//                {
//                    isActive = false;
//                    newFrame = false;
//                }
//                if (onLoop != null)
//                {
//                    onLoop();
//                }
//                if (destroyOnLoop)
//                {
//                    Destroy(gameObject);
//                    return;
//                }
//            }
//        }
//        if (newFrame)
//        {
//            spriteRenderer.sprite = _frames[currentFrame];
//        }
//    }

//    public void Setup(Sprite[] frames, int framesPerSecond)
//    {
//        this.frames = frames;
//        _frames = new List<Sprite>();
//        for (int i = 0; i < frames.Length; i++)
//        {
//            if (i >= firstFrame && i <= endFrame)
//                _frames.Add(frames[i]);
//        }
//        this.framesPerSecond = framesPerSecond;
//        timerMax = 1f / framesPerSecond;
//        timer = 0f;
//        PlayStart();
//    }

//    public void PlayStart()
//    {
//        timer = 0;
//        currentFrame = 0;
//        spriteRenderer.sprite = _frames[currentFrame];
//        isActive = true;
//    }

//}

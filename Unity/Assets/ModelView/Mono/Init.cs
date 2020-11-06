using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ET
{
    public class Init : MonoBehaviour
    {
        [SerializeField]
        public string key;
        [SerializeField]
        public string keyIV;
        public static byte[] xorKey;
        [SerializeField]
        public string _xorKey;
        public string XorKey => _xorKey;
        private void Start()
        {
            this.StartAsync();
        }
        private void StartAsync()
        {
            try
            {
                SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

                DontDestroyOnLoad(gameObject);
                string[] assemblyNames = { "Unity.Model.dll", "Unity.ModelView.dll"};

                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    string assemblyName = assembly.ManifestModule.Name;
                    if (!assemblyNames.Contains(assemblyName))
                    {
                        continue;
                    }
                    Game.EventSystem.Add(assembly);
                }

                xorKey = Encoding.UTF8.GetBytes(_xorKey);

                Game.EventSystem.Publish(new ET.EventType.AppStart
                {
                    key = key,
                    keyIV = keyIV,
                    xorKey = xorKey,
                }).Coroutine();

            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void Update()
        {
            OneThreadSynchronizationContext.Instance.Update();
            Game.EventSystem.Update();

        }
        private void LateUpdate()
        {
            Game.EventSystem.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            Game.Close();
        }
        public static void Quit()
        {
#if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}
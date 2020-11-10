using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class TriggerMono:UnityEngine.MonoBehaviour
    {
        private event Action<GameObject> _onTriggerEnter2D;
        public event Action<GameObject> onTriggerEnter2D
        {
            add
            {
                _onTriggerEnter2D -= value;
                _onTriggerEnter2D += value;
            }
            remove
            {
                _onTriggerEnter2D -= value;
            }
        }
        private event Action<GameObject> _onTriggerExit2D;
        public event Action<GameObject> onTriggerExit2D
        {
            add
            {
                _onTriggerExit2D -= value;
                _onTriggerExit2D += value;
            }
            remove
            {
                _onTriggerExit2D -= value;
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onTriggerEnter2D?.Invoke(collision.gameObject);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _onTriggerExit2D?.Invoke(collision.gameObject);
        }
    }
}

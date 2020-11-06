using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


    [AddComponentMenu("UI/DoubleClickButton")]
    public class DoubleClickButton : Button
    {
        [Serializable]
        public class DoubleClickedEvent : UnityEvent<DoubleClickButton> { }

        [SerializeField]
        private DoubleClickedEvent m_onDoubleClick = new DoubleClickedEvent();
        public DoubleClickedEvent onDoubleClick
        {
            get { return m_onDoubleClick; }
            set { m_onDoubleClick = value; }
        }

        [Serializable]
        public class OnClickedEvent : UnityEvent<DoubleClickButton> { }

        [SerializeField]
        private OnClickedEvent m_onButtonClick = new OnClickedEvent();
        public OnClickedEvent onButtonClick
        {
            get { return m_onButtonClick; }
            set { m_onButtonClick = value; }
        }

        public int Id { get; set; }
        private DateTime m_firstTime;
        private DateTime m_secondTime;

        private void Press()
        {
            if (null != onDoubleClick)
                onDoubleClick.Invoke(this);
            ResetTime();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            onButtonClick?.Invoke(this);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (m_firstTime.Equals(default(DateTime)))
                m_firstTime = DateTime.Now;
            else
                m_secondTime = DateTime.Now;
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (!m_firstTime.Equals(default(DateTime)) && !m_secondTime.Equals(default(DateTime)))
            {
                var intervalTime = m_secondTime - m_firstTime;
                float milliSeconds = intervalTime.Seconds * 1000 + intervalTime.Milliseconds;
                if (milliSeconds < 400)
                    Press();
                else
                    ResetTime();
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            ResetTime();
        }

        private void ResetTime()
        {
            m_firstTime = default(DateTime);
            m_secondTime = default(DateTime);
        }
    }

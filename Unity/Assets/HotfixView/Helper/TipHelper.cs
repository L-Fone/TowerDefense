using ET;
using FairyGUI;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class TipHelper
    {

        public static FUI_TipUI OpenUI(string tipContent, bool isClearIpt = true, TipType tipType = TipType.Single)
        {
            if (!(FUIComponent.Instance.Get(FUIPackage.Dialog_TipUI) is FUI_TipUI tipUI))
            {
                tipUI = FUI_TipUI.CreateInstance(FUIComponent.Instance);
                tipUI.Name = FUIPackage.Dialog_TipUI;
                tipUI.self.sortingOrder = 9998;
                FUIComponent.Instance.Add(tipUI, true);
            }
            tipUI.Visible = true;
            switch (tipType)
            {
                case TipType.Single:
                    tipUI.m_IptTxt.focusable = false;
                    break;
                case TipType.SingleInput:
                    tipUI.m_IptTxt.focusable = true;
                    break;
                case TipType.Double:
                    tipUI.m_IptTxt.focusable = false;
                    break;
                case TipType.DoubleInput:
                    tipUI.m_IptTxt.focusable = true;
                    break;
                default:
                    break;
            }
            switch (RandomHelper.RandomNumber(0, 4))
            {
                case 0: tipUI.m_t0.Play(); break;
                case 1: tipUI.m_t1.Play(); break;
                case 2: tipUI.m_t2.Play(); break;
                case 3: tipUI.m_t3.Play(); break;
            }
            tipUI.m_c1.SetSelectedIndex((int)tipType);
            tipUI.m_txtContent.text = tipContent;
            if (isClearIpt)
            {
                tipUI.m_IptTxt.text = null;
            }

            tipUI.m_IptTxt.RequestFocus();
            tipUI.self.onClick.Set1(content =>
            {
                if (((Container)content.initiator).gOwner is GButton)
                    tipUI.Visible = false;
            });
            return tipUI;
        }
        public static void AddEventCallBack(this FUI_TipUI self, EventCallback0 eventCallback = null)
        {
            //var tipUIWindow = self.GetOrAddComponent<FUIWindowComponent>();
            self.m_IptTxt.onSubmit.Set(() =>
            {
                //tipUIWindow.Hide();
                self.Visible = false;
                eventCallback?.Invoke();
            });
        }

    }
}

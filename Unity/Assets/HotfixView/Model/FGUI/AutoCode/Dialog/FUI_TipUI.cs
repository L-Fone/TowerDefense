/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_TipUI_AwakeSystem : AwakeSystem<FUI_TipUI, GObject>
    {
        public override void Awake(FUI_TipUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_TipUI : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "TipUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_c1;
    public GImage m_frame;
    public GTextField m_txtContent;
    public GButton m_btnSure;
    public GButton m_btnYes;
    public GButton m_btnNo;
    public GTextInput m_IptTxt;
    public Transition m_t0;
    public Transition m_t1;
    public Transition m_t2;
    public Transition m_t3;
    public const string URL = "ui://cb3q22aoiwo2h";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_TipUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_TipUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_TipUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_TipUI> tcs = new ETTaskCompletionSource<FUI_TipUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_TipUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_TipUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_TipUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_TipUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_TipUI>();

        if(fui == null)
        {
            fui = Create(domain,go);
        }

        fui.isFromFGUIPool = true;

        return fui;
    }
        
    public void Awake(GObject go)
    {
        if(go == null)
        {
            return;
        }
        
        GObject = go;	
        
        if (string.IsNullOrWhiteSpace(Name))
        {
            Name = Id.ToString();
        }
        
        self = (GComponent)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_c1 = com.GetController("c1");
    		m_frame = (GImage)com.GetChild("frame");
    		m_txtContent = (GTextField)com.GetChild("txtContent");
    		m_btnSure = (GButton)com.GetChild("btnSure");
    		m_btnYes = (GButton)com.GetChild("btnYes");
    		m_btnNo = (GButton)com.GetChild("btnNo");
    		m_IptTxt = (GTextInput)com.GetChild("IptTxt");
    		m_t0 = com.GetTransition("t0");
    		m_t1 = com.GetTransition("t1");
    		m_t2 = com.GetTransition("t2");
    		m_t3 = com.GetTransition("t3");
    	}
}
public override void Dispose()
    {
        if(IsDisposed)
        {
            return;
        }
        
        base.Dispose();
        
        self.Remove();
        self = null;
            
		m_c1 = null;
		m_frame = null;
		m_txtContent = null;
		m_btnSure = null;
		m_btnYes = null;
		m_btnNo = null;
		m_IptTxt = null;
		m_t0 = null;
		m_t1 = null;
		m_t2 = null;
		m_t3 = null;
	}
}
}
/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_CheatUI_AwakeSystem : AwakeSystem<FUI_CheatUI, GObject>
    {
        public override void Awake(FUI_CheatUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_CheatUI : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "CheatUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameCheat m_frame;
    public GTextInput m_txtLevel;
    public GTextInput m_txtEquip;
    public GTextInput m_txtGoods;
    public GTextInput m_txtMetarials;
    public FUI_ButtonText m_btnSend;
    public const string URL = "ui://kqsmrpxl4wfcutlh";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_CheatUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_CheatUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_CheatUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_CheatUI> tcs = new ETTaskCompletionSource<FUI_CheatUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_CheatUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_CheatUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_CheatUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_CheatUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_CheatUI>();

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
            
    		m_frame = FUI_FrameCheat.Create(domain, com.GetChild("frame"));
    		m_txtLevel = (GTextInput)com.GetChild("txtLevel");
    		m_txtEquip = (GTextInput)com.GetChild("txtEquip");
    		m_txtGoods = (GTextInput)com.GetChild("txtGoods");
    		m_txtMetarials = (GTextInput)com.GetChild("txtMetarials");
    		m_btnSend = FUI_ButtonText.Create(domain, com.GetChild("btnSend"));
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
            
		m_frame.Dispose();
		m_frame = null;
		m_txtLevel = null;
		m_txtEquip = null;
		m_txtGoods = null;
		m_txtMetarials = null;
		m_btnSend.Dispose();
		m_btnSend = null;
	}
}
}
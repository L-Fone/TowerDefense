/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ButtonMianUISlot_AwakeSystem : AwakeSystem<FUI_ButtonMianUISlot, GObject>
    {
        public override void Awake(FUI_ButtonMianUISlot self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ButtonMianUISlot : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "ButtonMianUISlot";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    public GImage m_imgMask;
    public GTextField m_txtCD;
    public const string URL = "ui://kqsmrpxlhg0f6g";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ButtonMianUISlot CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ButtonMianUISlot, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ButtonMianUISlot> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ButtonMianUISlot> tcs = new ETTaskCompletionSource<FUI_ButtonMianUISlot>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ButtonMianUISlot, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ButtonMianUISlot Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ButtonMianUISlot, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ButtonMianUISlot GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ButtonMianUISlot>();

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
        
        self = (GButton)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_imgMask = (GImage)com.GetChild("imgMask");
    		m_txtCD = (GTextField)com.GetChild("txtCD");
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
            
		m_imgMask = null;
		m_txtCD = null;
	}
}
}
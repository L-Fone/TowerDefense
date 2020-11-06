/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LeftTransPointUI_AwakeSystem : AwakeSystem<FUI_LeftTransPointUI, GObject>
    {
        public override void Awake(FUI_LeftTransPointUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LeftTransPointUI : FUI
    {	
        public const string UIPackageName = "TransPointUI";
        public const string UIResName = "LeftTransPointUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameLeftTransPointUI m_frame;
    public GButton m_btn1;
    public GButton m_btn2;
    public GButton m_btn3;
    public GButton m_btn4;
    public GTextField m_txtName;
    public const string URL = "ui://7kiucqowptlyd";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LeftTransPointUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LeftTransPointUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LeftTransPointUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LeftTransPointUI> tcs = new ETTaskCompletionSource<FUI_LeftTransPointUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LeftTransPointUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LeftTransPointUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LeftTransPointUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LeftTransPointUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LeftTransPointUI>();

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
            
    		m_frame = FUI_FrameLeftTransPointUI.Create(domain, com.GetChild("frame"));
    		m_btn1 = (GButton)com.GetChild("btn1");
    		m_btn2 = (GButton)com.GetChild("btn2");
    		m_btn3 = (GButton)com.GetChild("btn3");
    		m_btn4 = (GButton)com.GetChild("btn4");
    		m_txtName = (GTextField)com.GetChild("txtName");
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
		m_btn1 = null;
		m_btn2 = null;
		m_btn3 = null;
		m_btn4 = null;
		m_txtName = null;
	}
}
}
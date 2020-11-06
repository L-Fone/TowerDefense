/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_FrameLeftTransPointUI_AwakeSystem : AwakeSystem<FUI_FrameLeftTransPointUI, GObject>
    {
        public override void Awake(FUI_FrameLeftTransPointUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_FrameLeftTransPointUI : FUI
    {	
        public const string UIPackageName = "TransPointUI";
        public const string UIResName = "FrameLeftTransPointUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public GImage m_bg;
    public GButton m_closeButton;
    public GGraph m_dragArea;
    public GGraph m_contentArea;
    public const string URL = "ui://7kiucqowptly1";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_FrameLeftTransPointUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_FrameLeftTransPointUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_FrameLeftTransPointUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_FrameLeftTransPointUI> tcs = new ETTaskCompletionSource<FUI_FrameLeftTransPointUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_FrameLeftTransPointUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_FrameLeftTransPointUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_FrameLeftTransPointUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_FrameLeftTransPointUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_FrameLeftTransPointUI>();

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
        
        self = (GLabel)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_bg = (GImage)com.GetChild("bg");
    		m_closeButton = (GButton)com.GetChild("closeButton");
    		m_dragArea = (GGraph)com.GetChild("dragArea");
    		m_contentArea = (GGraph)com.GetChild("contentArea");
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
            
		m_bg = null;
		m_closeButton = null;
		m_dragArea = null;
		m_contentArea = null;
	}
}
}
/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_NoticeUI_AwakeSystem : AwakeSystem<FUI_NoticeUI, GObject>
    {
        public override void Awake(FUI_NoticeUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_NoticeUI : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "NoticeUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameNotice m_frame;
    public GRichTextField m_title;
    public GButton m_btnNotDisplay;
    public const string URL = "ui://cb3q22aojafgk";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_NoticeUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_NoticeUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_NoticeUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_NoticeUI> tcs = new ETTaskCompletionSource<FUI_NoticeUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_NoticeUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_NoticeUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_NoticeUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_NoticeUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_NoticeUI>();

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
            
    		m_frame = FUI_FrameNotice.Create(domain, com.GetChild("frame"));
    		m_title = (GRichTextField)com.GetChild("title");
    		m_btnNotDisplay = (GButton)com.GetChild("btnNotDisplay");
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
		m_title = null;
		m_btnNotDisplay = null;
	}
}
}
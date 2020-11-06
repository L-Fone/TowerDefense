/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_RightTransPointUI_AwakeSystem : AwakeSystem<FUI_RightTransPointUI, GObject>
    {
        public override void Awake(FUI_RightTransPointUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_RightTransPointUI : FUI
    {	
        public const string UIPackageName = "TransPointUI";
        public const string UIResName = "RightTransPointUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_contrl;
    public FUI_FrameRightTransPointUI m_frame;
    public GButton m_btn1;
    public GButton m_btn2;
    public GButton m_btn3;
    public GButton m_btn4;
    public GButton m_btn5;
    public GButton m_btn6;
    public GTextField m_txtLevel;
    public GList m_mainstoryList;
    public GList m_bossList;
    public GList m_activeList;
    public const string URL = "ui://7kiucqowiha8y";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_RightTransPointUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_RightTransPointUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_RightTransPointUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_RightTransPointUI> tcs = new ETTaskCompletionSource<FUI_RightTransPointUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_RightTransPointUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_RightTransPointUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_RightTransPointUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_RightTransPointUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_RightTransPointUI>();

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
            
    		m_contrl = com.GetController("contrl");
    		m_frame = FUI_FrameRightTransPointUI.Create(domain, com.GetChild("frame"));
    		m_btn1 = (GButton)com.GetChild("btn1");
    		m_btn2 = (GButton)com.GetChild("btn2");
    		m_btn3 = (GButton)com.GetChild("btn3");
    		m_btn4 = (GButton)com.GetChild("btn4");
    		m_btn5 = (GButton)com.GetChild("btn5");
    		m_btn6 = (GButton)com.GetChild("btn6");
    		m_txtLevel = (GTextField)com.GetChild("txtLevel");
    		m_mainstoryList = (GList)com.GetChild("mainstoryList");
    		m_bossList = (GList)com.GetChild("bossList");
    		m_activeList = (GList)com.GetChild("activeList");
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
            
		m_contrl = null;
		m_frame.Dispose();
		m_frame = null;
		m_btn1 = null;
		m_btn2 = null;
		m_btn3 = null;
		m_btn4 = null;
		m_btn5 = null;
		m_btn6 = null;
		m_txtLevel = null;
		m_mainstoryList = null;
		m_bossList = null;
		m_activeList = null;
	}
}
}
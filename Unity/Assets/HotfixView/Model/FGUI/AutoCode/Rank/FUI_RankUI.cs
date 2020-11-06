/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_RankUI_AwakeSystem : AwakeSystem<FUI_RankUI, GObject>
    {
        public override void Awake(FUI_RankUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_RankUI : FUI
    {	
        public const string UIPackageName = "Rank";
        public const string UIResName = "RankUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_control;
    public FUI_FrameRank m_frame;
    public GList m_rankList;
    public const string URL = "ui://g4444q3avj0yb";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_RankUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_RankUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_RankUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_RankUI> tcs = new ETTaskCompletionSource<FUI_RankUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_RankUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_RankUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_RankUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_RankUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_RankUI>();

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
            
    		m_control = com.GetController("control");
    		m_frame = FUI_FrameRank.Create(domain, com.GetChild("frame"));
    		m_rankList = (GList)com.GetChild("rankList");
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
            
		m_control = null;
		m_frame.Dispose();
		m_frame = null;
		m_rankList = null;
	}
}
}
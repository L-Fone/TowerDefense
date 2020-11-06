/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_UpgradeTreeUI_AwakeSystem : AwakeSystem<FUI_UpgradeTreeUI, GObject>
    {
        public override void Awake(FUI_UpgradeTreeUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_UpgradeTreeUI : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "UpgradeTreeUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameUpgradeUI m_frame;
    public GList m_itemList;
    public Transition m_Effect;
    public const string URL = "ui://71ktouo7il72kl";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_UpgradeTreeUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_UpgradeTreeUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_UpgradeTreeUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_UpgradeTreeUI> tcs = new ETTaskCompletionSource<FUI_UpgradeTreeUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_UpgradeTreeUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_UpgradeTreeUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_UpgradeTreeUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_UpgradeTreeUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_UpgradeTreeUI>();

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
            
    		m_frame = FUI_FrameUpgradeUI.Create(domain, com.GetChild("frame"));
    		m_itemList = (GList)com.GetChild("itemList");
    		m_Effect = com.GetTransition("Effect");
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
		m_itemList = null;
		m_Effect = null;
	}
}
}
/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_UpgradeUI_AwakeSystem : AwakeSystem<FUI_UpgradeUI, GObject>
    {
        public override void Awake(FUI_UpgradeUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_UpgradeUI : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "UpgradeUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameMeltingUI m_frame;
    public GButton m_btnFinalItem;
    public GButton m_btnItem;
    public GButton m_btnUpgrade;
    public GButton m_btnTree;
    public GTextField m_txtMaterial;
    public Transition m_Effect;
    public const string URL = "ui://71ktouo7il72ki";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_UpgradeUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_UpgradeUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_UpgradeUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_UpgradeUI> tcs = new ETTaskCompletionSource<FUI_UpgradeUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_UpgradeUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_UpgradeUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_UpgradeUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_UpgradeUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_UpgradeUI>();

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
            
    		m_frame = FUI_FrameMeltingUI.Create(domain, com.GetChild("frame"));
    		m_btnFinalItem = (GButton)com.GetChild("btnFinalItem");
    		m_btnItem = (GButton)com.GetChild("btnItem");
    		m_btnUpgrade = (GButton)com.GetChild("btnUpgrade");
    		m_btnTree = (GButton)com.GetChild("btnTree");
    		m_txtMaterial = (GTextField)com.GetChild("txtMaterial");
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
		m_btnFinalItem = null;
		m_btnItem = null;
		m_btnUpgrade = null;
		m_btnTree = null;
		m_txtMaterial = null;
		m_Effect = null;
	}
}
}
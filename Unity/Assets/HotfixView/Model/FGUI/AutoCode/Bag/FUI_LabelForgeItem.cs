/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LabelForgeItem_AwakeSystem : AwakeSystem<FUI_LabelForgeItem, GObject>
    {
        public override void Awake(FUI_LabelForgeItem self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LabelForgeItem : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "LabelForgeItem";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public Controller m_canForge;
    public GButton m_btnForge;
    public GLoader m_loaderIcon;
    public GTextField m_txtName;
    public GTextField m_txtLevel;
    public GTextField m_txtGold;
    public GTextField m_txtSliver;
    public GTextField m_txtCoin;
    public const string URL = "ui://71ktouo7mj9wjr";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LabelForgeItem CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LabelForgeItem, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LabelForgeItem> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LabelForgeItem> tcs = new ETTaskCompletionSource<FUI_LabelForgeItem>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LabelForgeItem, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LabelForgeItem Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LabelForgeItem, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LabelForgeItem GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LabelForgeItem>();

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
            
    		m_canForge = com.GetController("canForge");
    		m_btnForge = (GButton)com.GetChild("btnForge");
    		m_loaderIcon = (GLoader)com.GetChild("loaderIcon");
    		m_txtName = (GTextField)com.GetChild("txtName");
    		m_txtLevel = (GTextField)com.GetChild("txtLevel");
    		m_txtGold = (GTextField)com.GetChild("txtGold");
    		m_txtSliver = (GTextField)com.GetChild("txtSliver");
    		m_txtCoin = (GTextField)com.GetChild("txtCoin");
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
            
		m_canForge = null;
		m_btnForge = null;
		m_loaderIcon = null;
		m_txtName = null;
		m_txtLevel = null;
		m_txtGold = null;
		m_txtSliver = null;
		m_txtCoin = null;
	}
}
}
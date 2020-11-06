/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_BagUI_AwakeSystem : AwakeSystem<FUI_BagUI, GObject>
    {
        public override void Awake(FUI_BagUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_BagUI : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "BagUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameBag m_frame;
    public GList m_slotList;
    public GTextField m_txtTip;
    public GTextField m_txtGold;
    public GTextField m_txtSliver;
    public GTextField m_txtCoin;
    public GTextField m_txtYunabao;
    public GTextField m_txtDaijinquan;
    public GButton m_btnSort;
    public GButton m_btnUpgrade;
    public GButton m_btnHelp;
    public GGroup m_gorup;
    public Transition m_Effect;
    public const string URL = "ui://71ktouo7fyvbq";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_BagUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_BagUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_BagUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_BagUI> tcs = new ETTaskCompletionSource<FUI_BagUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_BagUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_BagUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_BagUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_BagUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_BagUI>();

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
            
    		m_frame = FUI_FrameBag.Create(domain, com.GetChild("frame"));
    		m_slotList = (GList)com.GetChild("slotList");
    		m_txtTip = (GTextField)com.GetChild("txtTip");
    		m_txtGold = (GTextField)com.GetChild("txtGold");
    		m_txtSliver = (GTextField)com.GetChild("txtSliver");
    		m_txtCoin = (GTextField)com.GetChild("txtCoin");
    		m_txtYunabao = (GTextField)com.GetChild("txtYunabao");
    		m_txtDaijinquan = (GTextField)com.GetChild("txtDaijinquan");
    		m_btnSort = (GButton)com.GetChild("btnSort");
    		m_btnUpgrade = (GButton)com.GetChild("btnUpgrade");
    		m_btnHelp = (GButton)com.GetChild("btnHelp");
    		m_gorup = (GGroup)com.GetChild("gorup");
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
		m_slotList = null;
		m_txtTip = null;
		m_txtGold = null;
		m_txtSliver = null;
		m_txtCoin = null;
		m_txtYunabao = null;
		m_txtDaijinquan = null;
		m_btnSort = null;
		m_btnUpgrade = null;
		m_btnHelp = null;
		m_gorup = null;
		m_Effect = null;
	}
}
}
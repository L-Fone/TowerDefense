/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_MeltingUI_AwakeSystem : AwakeSystem<FUI_MeltingUI, GObject>
    {
        public override void Awake(FUI_MeltingUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_MeltingUI : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "MeltingUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameMeltingUI m_frame;
    public GButton m_btnMelt;
    public GButton m_btnEquip;
    public GButton m_btnGem;
    public GTextField m_txtGold;
    public GTextField m_txtSliver;
    public GTextField m_txtCoin;
    public GTextField m_txtVoucher;
    public GTextField m_txtYuanBao;
    public GList m_ItemList;
    public Transition m_Effect;
    public const string URL = "ui://71ktouo7o3cnip";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_MeltingUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_MeltingUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_MeltingUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_MeltingUI> tcs = new ETTaskCompletionSource<FUI_MeltingUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_MeltingUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_MeltingUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_MeltingUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_MeltingUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_MeltingUI>();

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
    		m_btnMelt = (GButton)com.GetChild("btnMelt");
    		m_btnEquip = (GButton)com.GetChild("btnEquip");
    		m_btnGem = (GButton)com.GetChild("btnGem");
    		m_txtGold = (GTextField)com.GetChild("txtGold");
    		m_txtSliver = (GTextField)com.GetChild("txtSliver");
    		m_txtCoin = (GTextField)com.GetChild("txtCoin");
    		m_txtVoucher = (GTextField)com.GetChild("txtVoucher");
    		m_txtYuanBao = (GTextField)com.GetChild("txtYuanBao");
    		m_ItemList = (GList)com.GetChild("ItemList");
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
		m_btnMelt = null;
		m_btnEquip = null;
		m_btnGem = null;
		m_txtGold = null;
		m_txtSliver = null;
		m_txtCoin = null;
		m_txtVoucher = null;
		m_txtYuanBao = null;
		m_ItemList = null;
		m_Effect = null;
	}
}
}
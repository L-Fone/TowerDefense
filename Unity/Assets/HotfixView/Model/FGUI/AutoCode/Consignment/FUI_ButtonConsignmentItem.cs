/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ButtonConsignmentItem_AwakeSystem : AwakeSystem<FUI_ButtonConsignmentItem, GObject>
    {
        public override void Awake(FUI_ButtonConsignmentItem self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ButtonConsignmentItem : FUI
    {	
        public const string UIPackageName = "Consignment";
        public const string UIResName = "ButtonConsignmentItem";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    public GImage m_bg;
    public GTextField m_txtName;
    public GTextField m_txtLevel;
    public GTextField m_txtTime;
    public GRichTextField m_txtPrice;
    public GTextField m_txtSeller;
    public GTextField m_txtCount;
    public const string URL = "ui://ivv55tvqih6n7";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ButtonConsignmentItem CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ButtonConsignmentItem, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ButtonConsignmentItem> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ButtonConsignmentItem> tcs = new ETTaskCompletionSource<FUI_ButtonConsignmentItem>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ButtonConsignmentItem, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ButtonConsignmentItem Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ButtonConsignmentItem, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ButtonConsignmentItem GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ButtonConsignmentItem>();

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
        
        self = (GButton)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_bg = (GImage)com.GetChild("bg");
    		m_txtName = (GTextField)com.GetChild("txtName");
    		m_txtLevel = (GTextField)com.GetChild("txtLevel");
    		m_txtTime = (GTextField)com.GetChild("txtTime");
    		m_txtPrice = (GRichTextField)com.GetChild("txtPrice");
    		m_txtSeller = (GTextField)com.GetChild("txtSeller");
    		m_txtCount = (GTextField)com.GetChild("txtCount");
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
		m_txtName = null;
		m_txtLevel = null;
		m_txtTime = null;
		m_txtPrice = null;
		m_txtSeller = null;
		m_txtCount = null;
	}
}
}
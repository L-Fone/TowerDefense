/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ConsignmentPutInUI_AwakeSystem : AwakeSystem<FUI_ConsignmentPutInUI, GObject>
    {
        public override void Awake(FUI_ConsignmentPutInUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ConsignmentPutInUI : FUI
    {	
        public const string UIPackageName = "Consignment";
        public const string UIResName = "ConsignmentPutInUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameConsignmentPutInUI m_frame;
    public GButton m_btnSlot;
    public GTextInput m_inpCount;
    public GButton m_btnSure;
    public GTextInput m_inpGold;
    public GTextInput m_inpSliver;
    public GTextInput m_inpCoin;
    public const string URL = "ui://ivv55tvqm1qup";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ConsignmentPutInUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ConsignmentPutInUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ConsignmentPutInUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ConsignmentPutInUI> tcs = new ETTaskCompletionSource<FUI_ConsignmentPutInUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ConsignmentPutInUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ConsignmentPutInUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ConsignmentPutInUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ConsignmentPutInUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ConsignmentPutInUI>();

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
            
    		m_frame = FUI_FrameConsignmentPutInUI.Create(domain, com.GetChild("frame"));
    		m_btnSlot = (GButton)com.GetChild("btnSlot");
    		m_inpCount = (GTextInput)com.GetChild("inpCount");
    		m_btnSure = (GButton)com.GetChild("btnSure");
    		m_inpGold = (GTextInput)com.GetChild("inpGold");
    		m_inpSliver = (GTextInput)com.GetChild("inpSliver");
    		m_inpCoin = (GTextInput)com.GetChild("inpCoin");
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
		m_btnSlot = null;
		m_inpCount = null;
		m_btnSure = null;
		m_inpGold = null;
		m_inpSliver = null;
		m_inpCoin = null;
	}
}
}
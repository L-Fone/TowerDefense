/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LabelRequestItem_AwakeSystem : AwakeSystem<FUI_LabelRequestItem, GObject>
    {
        public override void Awake(FUI_LabelRequestItem self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LabelRequestItem : FUI
    {	
        public const string UIPackageName = "Friend";
        public const string UIResName = "LabelRequestItem";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public GImage m_bg;
    public GButton m_btnAgree;
    public GButton m_btnRefuse;
    public const string URL = "ui://0nnc7y49j03xk";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LabelRequestItem CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LabelRequestItem, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LabelRequestItem> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LabelRequestItem> tcs = new ETTaskCompletionSource<FUI_LabelRequestItem>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LabelRequestItem, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LabelRequestItem Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LabelRequestItem, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LabelRequestItem GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LabelRequestItem>();

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
            
    		m_bg = (GImage)com.GetChild("bg");
    		m_btnAgree = (GButton)com.GetChild("btnAgree");
    		m_btnRefuse = (GButton)com.GetChild("btnRefuse");
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
		m_btnAgree = null;
		m_btnRefuse = null;
	}
}
}
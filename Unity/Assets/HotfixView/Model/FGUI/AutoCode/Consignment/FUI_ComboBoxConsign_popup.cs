/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ComboBoxConsign_popup_AwakeSystem : AwakeSystem<FUI_ComboBoxConsign_popup, GObject>
    {
        public override void Awake(FUI_ComboBoxConsign_popup self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ComboBoxConsign_popup : FUI
    {	
        public const string UIPackageName = "Consignment";
        public const string UIResName = "ComboBoxConsign_popup";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GList m_list;
    public const string URL = "ui://ivv55tvqih6nl";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ComboBoxConsign_popup CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ComboBoxConsign_popup, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ComboBoxConsign_popup> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ComboBoxConsign_popup> tcs = new ETTaskCompletionSource<FUI_ComboBoxConsign_popup>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ComboBoxConsign_popup, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ComboBoxConsign_popup Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ComboBoxConsign_popup, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ComboBoxConsign_popup GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ComboBoxConsign_popup>();

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
            
    		m_list = (GList)com.GetChild("list");
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
            
		m_list = null;
	}
}
}
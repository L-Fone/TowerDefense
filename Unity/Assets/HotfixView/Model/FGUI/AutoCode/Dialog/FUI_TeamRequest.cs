/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_TeamRequest_AwakeSystem : AwakeSystem<FUI_TeamRequest, GObject>
    {
        public override void Awake(FUI_TeamRequest self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_TeamRequest : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "TeamRequest";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GList m_list;
    public const string URL = "ui://cb3q22aola9r7";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_TeamRequest CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_TeamRequest, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_TeamRequest> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_TeamRequest> tcs = new ETTaskCompletionSource<FUI_TeamRequest>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_TeamRequest, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_TeamRequest Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_TeamRequest, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_TeamRequest GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_TeamRequest>();

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
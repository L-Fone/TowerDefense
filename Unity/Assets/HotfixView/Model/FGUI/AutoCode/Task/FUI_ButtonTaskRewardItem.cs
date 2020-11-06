/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ButtonTaskRewardItem_AwakeSystem : AwakeSystem<FUI_ButtonTaskRewardItem, GObject>
    {
        public override void Awake(FUI_ButtonTaskRewardItem self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ButtonTaskRewardItem : FUI
    {	
        public const string UIPackageName = "Task";
        public const string UIResName = "ButtonTaskRewardItem";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    public GGraph m_bg;
    public GImage m_Slected;
    public const string URL = "ui://thcq7hm4mewwg";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ButtonTaskRewardItem CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ButtonTaskRewardItem, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ButtonTaskRewardItem> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ButtonTaskRewardItem> tcs = new ETTaskCompletionSource<FUI_ButtonTaskRewardItem>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ButtonTaskRewardItem, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ButtonTaskRewardItem Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ButtonTaskRewardItem, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ButtonTaskRewardItem GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ButtonTaskRewardItem>();

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
            
    		m_bg = (GGraph)com.GetChild("bg");
    		m_Slected = (GImage)com.GetChild("Slected");
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
		m_Slected = null;
	}
}
}
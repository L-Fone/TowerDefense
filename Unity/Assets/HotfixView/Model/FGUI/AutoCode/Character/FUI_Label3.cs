/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Label3_AwakeSystem : AwakeSystem<FUI_Label3, GObject>
    {
        public override void Awake(FUI_Label3 self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Label3 : FUI
    {	
        public const string UIPackageName = "Character";
        public const string UIResName = "Label3";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public GTextField m_txt;
    public const string URL = "ui://768jx61w7bwwp";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Label3 CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Label3, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Label3> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Label3> tcs = new ETTaskCompletionSource<FUI_Label3>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Label3, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Label3 Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Label3, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Label3 GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Label3>();

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
            
    		m_txt = (GTextField)com.GetChild("txt");
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
            
		m_txt = null;
	}
}
}
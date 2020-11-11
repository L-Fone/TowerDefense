/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_EnterGame_AwakeSystem : AwakeSystem<FUI_EnterGame, GObject>
    {
        public override void Awake(FUI_EnterGame self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_EnterGame : FUI
    {	
        public const string UIPackageName = "Login";
        public const string UIResName = "EnterGame";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GButton m_btnEnterGame;
    public const string URL = "ui://k3d3mc7geh2ai";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_EnterGame CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_EnterGame, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_EnterGame> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_EnterGame> tcs = new ETTaskCompletionSource<FUI_EnterGame>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_EnterGame, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_EnterGame Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_EnterGame, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_EnterGame GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_EnterGame>();

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
            
    		m_btnEnterGame = (GButton)com.GetChild("btnEnterGame");
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
            
		m_btnEnterGame = null;
	}
}
}
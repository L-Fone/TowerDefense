/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_SkillNameUI_AwakeSystem : AwakeSystem<FUI_SkillNameUI, GObject>
    {
        public override void Awake(FUI_SkillNameUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_SkillNameUI : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "SkillNameUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GTextField m_txtName;
    public Transition m_Effect;
    public const string URL = "ui://kqsmrpxlssp36n";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_SkillNameUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_SkillNameUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_SkillNameUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_SkillNameUI> tcs = new ETTaskCompletionSource<FUI_SkillNameUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_SkillNameUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_SkillNameUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_SkillNameUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_SkillNameUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_SkillNameUI>();

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
            
    		m_txtName = (GTextField)com.GetChild("txtName");
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
            
		m_txtName = null;
		m_Effect = null;
	}
}
}
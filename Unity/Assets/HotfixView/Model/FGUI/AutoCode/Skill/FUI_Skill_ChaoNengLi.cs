/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Skill_ChaoNengLi_AwakeSystem : AwakeSystem<FUI_Skill_ChaoNengLi, GObject>
    {
        public override void Awake(FUI_Skill_ChaoNengLi self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Skill_ChaoNengLi : FUI
    {	
        public const string UIPackageName = "Skill";
        public const string UIResName = "Skill_超能力";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GLabel m_root;
    public GButton m_400001;
    public GButton m_410101;
    public GButton m_410201;
    public GButton m_410301;
    public GButton m_410302;
    public GButton m_410401;
    public GButton m_410402;
    public GButton m_410501;
    public GButton m_410502;
    public GButton m_410601;
    public GButton m_410602;
    public GButton m_410202;
    public GButton m_410303;
    public GButton m_410304;
    public GButton m_410403;
    public GButton m_410404;
    public GButton m_410503;
    public GButton m_410504;
    public GButton m_410603;
    public GButton m_410604;
    public GButton m_420101;
    public GButton m_420201;
    public GGroup m_all;
    public const string URL = "ui://7fc8sjendyl0t2s";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Skill_ChaoNengLi CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Skill_ChaoNengLi, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Skill_ChaoNengLi> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Skill_ChaoNengLi> tcs = new ETTaskCompletionSource<FUI_Skill_ChaoNengLi>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Skill_ChaoNengLi, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Skill_ChaoNengLi Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Skill_ChaoNengLi, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Skill_ChaoNengLi GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Skill_ChaoNengLi>();

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
            
    		m_root = (GLabel)com.GetChild("root");
    		m_400001 = (GButton)com.GetChild("400001");
    		m_410101 = (GButton)com.GetChild("410101");
    		m_410201 = (GButton)com.GetChild("410201");
    		m_410301 = (GButton)com.GetChild("410301");
    		m_410302 = (GButton)com.GetChild("410302");
    		m_410401 = (GButton)com.GetChild("410401");
    		m_410402 = (GButton)com.GetChild("410402");
    		m_410501 = (GButton)com.GetChild("410501");
    		m_410502 = (GButton)com.GetChild("410502");
    		m_410601 = (GButton)com.GetChild("410601");
    		m_410602 = (GButton)com.GetChild("410602");
    		m_410202 = (GButton)com.GetChild("410202");
    		m_410303 = (GButton)com.GetChild("410303");
    		m_410304 = (GButton)com.GetChild("410304");
    		m_410403 = (GButton)com.GetChild("410403");
    		m_410404 = (GButton)com.GetChild("410404");
    		m_410503 = (GButton)com.GetChild("410503");
    		m_410504 = (GButton)com.GetChild("410504");
    		m_410603 = (GButton)com.GetChild("410603");
    		m_410604 = (GButton)com.GetChild("410604");
    		m_420101 = (GButton)com.GetChild("420101");
    		m_420201 = (GButton)com.GetChild("420201");
    		m_all = (GGroup)com.GetChild("all");
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
            
		m_root = null;
		m_400001 = null;
		m_410101 = null;
		m_410201 = null;
		m_410301 = null;
		m_410302 = null;
		m_410401 = null;
		m_410402 = null;
		m_410501 = null;
		m_410502 = null;
		m_410601 = null;
		m_410602 = null;
		m_410202 = null;
		m_410303 = null;
		m_410304 = null;
		m_410403 = null;
		m_410404 = null;
		m_410503 = null;
		m_410504 = null;
		m_410603 = null;
		m_410604 = null;
		m_420101 = null;
		m_420201 = null;
		m_all = null;
	}
}
}
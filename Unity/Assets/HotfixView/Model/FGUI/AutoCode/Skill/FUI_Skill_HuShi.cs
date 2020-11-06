/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Skill_HuShi_AwakeSystem : AwakeSystem<FUI_Skill_HuShi, GObject>
    {
        public override void Awake(FUI_Skill_HuShi self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Skill_HuShi : FUI
    {	
        public const string UIPackageName = "Skill";
        public const string UIResName = "Skill_护士";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GLabel m_root;
    public GButton m_300001;
    public GButton m_310101;
    public GButton m_310201;
    public GButton m_310301;
    public GButton m_310302;
    public GButton m_310401;
    public GButton m_310402;
    public GButton m_310501;
    public GButton m_310502;
    public GButton m_310601;
    public GButton m_310602;
    public GButton m_310202;
    public GButton m_310303;
    public GButton m_310304;
    public GButton m_310403;
    public GButton m_310404;
    public GButton m_310503;
    public GButton m_310504;
    public GButton m_310603;
    public GButton m_310604;
    public GButton m_320101;
    public GButton m_320201;
    public GGroup m_all;
    public const string URL = "ui://7fc8sjendyl0t2r";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Skill_HuShi CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Skill_HuShi, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Skill_HuShi> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Skill_HuShi> tcs = new ETTaskCompletionSource<FUI_Skill_HuShi>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Skill_HuShi, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Skill_HuShi Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Skill_HuShi, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Skill_HuShi GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Skill_HuShi>();

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
    		m_300001 = (GButton)com.GetChild("300001");
    		m_310101 = (GButton)com.GetChild("310101");
    		m_310201 = (GButton)com.GetChild("310201");
    		m_310301 = (GButton)com.GetChild("310301");
    		m_310302 = (GButton)com.GetChild("310302");
    		m_310401 = (GButton)com.GetChild("310401");
    		m_310402 = (GButton)com.GetChild("310402");
    		m_310501 = (GButton)com.GetChild("310501");
    		m_310502 = (GButton)com.GetChild("310502");
    		m_310601 = (GButton)com.GetChild("310601");
    		m_310602 = (GButton)com.GetChild("310602");
    		m_310202 = (GButton)com.GetChild("310202");
    		m_310303 = (GButton)com.GetChild("310303");
    		m_310304 = (GButton)com.GetChild("310304");
    		m_310403 = (GButton)com.GetChild("310403");
    		m_310404 = (GButton)com.GetChild("310404");
    		m_310503 = (GButton)com.GetChild("310503");
    		m_310504 = (GButton)com.GetChild("310504");
    		m_310603 = (GButton)com.GetChild("310603");
    		m_310604 = (GButton)com.GetChild("310604");
    		m_320101 = (GButton)com.GetChild("320101");
    		m_320201 = (GButton)com.GetChild("320201");
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
		m_300001 = null;
		m_310101 = null;
		m_310201 = null;
		m_310301 = null;
		m_310302 = null;
		m_310401 = null;
		m_310402 = null;
		m_310501 = null;
		m_310502 = null;
		m_310601 = null;
		m_310602 = null;
		m_310202 = null;
		m_310303 = null;
		m_310304 = null;
		m_310403 = null;
		m_310404 = null;
		m_310503 = null;
		m_310504 = null;
		m_310603 = null;
		m_310604 = null;
		m_320101 = null;
		m_320201 = null;
		m_all = null;
	}
}
}
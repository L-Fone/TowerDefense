/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_SkillUI_AwakeSystem : AwakeSystem<FUI_SkillUI, GObject>
    {
        public override void Awake(FUI_SkillUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_SkillUI : FUI
    {	
        public const string UIPackageName = "Skill";
        public const string UIResName = "SkillUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_SkillFrame m_frame;
    public GList m_autoSkillList;
    public GButton m_btnSave;
    public GButton m_btnHelper;
    public FUI_Skill_JunGuan m_comJob1;
    public FUI_Skill_YunDongYuan m_comJob2;
    public FUI_Skill_HuShi m_comJob3;
    public FUI_Skill_ChaoNengLi m_comJob4;
    public const string URL = "ui://7fc8sjen8tnc9";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_SkillUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_SkillUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_SkillUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_SkillUI> tcs = new ETTaskCompletionSource<FUI_SkillUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_SkillUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_SkillUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_SkillUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_SkillUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_SkillUI>();

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
            
    		m_frame = FUI_SkillFrame.Create(domain, com.GetChild("frame"));
    		m_autoSkillList = (GList)com.GetChild("autoSkillList");
    		m_btnSave = (GButton)com.GetChild("btnSave");
    		m_btnHelper = (GButton)com.GetChild("btnHelper");
    		m_comJob1 = FUI_Skill_JunGuan.Create(domain, com.GetChild("comJob1"));
    		m_comJob2 = FUI_Skill_YunDongYuan.Create(domain, com.GetChild("comJob2"));
    		m_comJob3 = FUI_Skill_HuShi.Create(domain, com.GetChild("comJob3"));
    		m_comJob4 = FUI_Skill_ChaoNengLi.Create(domain, com.GetChild("comJob4"));
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
		m_autoSkillList = null;
		m_btnSave = null;
		m_btnHelper = null;
		m_comJob1.Dispose();
		m_comJob1 = null;
		m_comJob2.Dispose();
		m_comJob2 = null;
		m_comJob3.Dispose();
		m_comJob3 = null;
		m_comJob4.Dispose();
		m_comJob4 = null;
	}
}
}
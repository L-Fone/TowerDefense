using ET;
using FairyGUI;

namespace ET
{
    public class FUIInitComponentAwakeSystem : AwakeSystem<FUIInitComponent>
    {
        public override void Awake(FUIInitComponent self)
        {
            UIConfig.defaultFont = "FZJiChuXiangSuS-R-GB";
            GRoot.inst.SetContentScaleFactor(1600, 900, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            self.Awake();
        }
    }
    public class FUIInitComponentDestroySystem:DestroySystem<FUIInitComponent>
    {
        public override void Destroy(FUIInitComponent self)
        {
            self.Destroy();
        }
    }

    public class FUIInitComponent : Entity
    {
        public void Awake()
        {
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Login);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Common);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Dialog);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.TransPointUI);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Skill);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Popup);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Character);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Bag);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Task);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Shop);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Consignment);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Mail);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Friend);
             domain.GetComponent<FUIPackageComponent>().AddPackageAsync(FUIPackage.Rank);
        }
        public void Destroy()
        {
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Login);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Common);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Dialog);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.TransPointUI);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Skill);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Popup);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Character);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Bag);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Task);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Shop);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Consignment);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Mail);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Friend);
            domain.GetComponent<FUIPackageComponent>().RemovePackage(FUIPackage.Rank);
        }

    }
}
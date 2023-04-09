using System.Reflection;
using Aki.Reflection.Patching;
using EFT;
using EFT.InventoryLogic;

namespace mortivore
{
    public class OnDeadPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => typeof(Player).GetMethod("OnDead", BindingFlags.Instance | BindingFlags.NonPublic);

        [PatchPostfix]
        private static void PatchPostFix(ref Player __instance)
        {
            if (Plugin.DropBackPack.Value) { __instance.DropBackpack(); }
        }
    }
}
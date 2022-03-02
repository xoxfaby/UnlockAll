using System;
using RoR2;
using BepInEx;

namespace UnlockAll
{
    [BepInPlugin("com.xoxfaby.UnlockAll", "UnlockAll", "1.0.3")]
    public class UnlockAllPlugin : BetterUnityPlugin.BetterUnityPlugin<UnlockAllPlugin>
    {
        public override BaseUnityPlugin typeReference => throw new NotImplementedException();

        protected override void Awake()
        {
            base.Awake();
            UnlockAllPlugin.Hooks.Add<System.Xml.Linq.XElement, string, RoR2.Stats.StatSheet>(typeof(RoR2.XmlUtility), "GetStatsField", XmlUtility_GetStatsField);
            UnlockAllPlugin.Hooks.Add<RoR2.Stats.StatSheet>(typeof(RoR2.Stats.StatSheet), "New", StatSheet_New);
        }
        private static RoR2.Stats.StatSheet StatSheet_New(Func<RoR2.Stats.StatSheet> orig)
        {
            var statSheet = orig();
            foreach (var unlockableDef in UnlockableCatalog.indexToDefTable)
            {
                statSheet.AddUnlockable(unlockableDef);
            }
            return statSheet;
        }

        private static void XmlUtility_GetStatsField(Action<System.Xml.Linq.XElement, string, RoR2.Stats.StatSheet> orig, System.Xml.Linq.XElement container, string fieldName, RoR2.Stats.StatSheet dest)
        {
            orig(container, fieldName, dest);
            foreach(var unlockableDef in UnlockableCatalog.indexToDefTable)
            {
                dest.AddUnlockable(unlockableDef);
            }
        }

    }
}

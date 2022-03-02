using System;
using RoR2;
using RoR2.Skills;
using BepInEx;
using UnityEngine;

using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.SceneManagement;

namespace UnlockAll
{
    [BepInPlugin("com.xoxfaby.UnlockAll", "UnlockAll", "1.0.0")]
    public class UnlockAllPlugin : BetterUnityPlugin.BetterUnityPlugin<UnlockAllPlugin>
    {
        public override BaseUnityPlugin typeReference => throw new NotImplementedException();

        protected override void Awake()
        {
            UnlockAllPlugin.Hooks.Add(typeof(RoR2.XmlUtility), "GetStatsField", XmlUtility_GetStatsField);
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

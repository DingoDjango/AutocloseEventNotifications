using System.Collections.Generic;
using HugsLib;
using HugsLib.Settings;
using RimWorld;
using UnityEngine;
using Verse;

namespace AutocloseEventNotifications
{
	public class Controller : ModBase
	{
		public static SettingHandle<int> Timer;

		public static SettingHandle<bool> ShowMessages;

		public static Dictionary<LetterDef, SettingHandle<bool>> PrefByDef = new Dictionary<LetterDef, SettingHandle<bool>>();

		private string ColouredOptionLabel(LetterDef def)
		{
			string nameString;

			switch (def.defName)
			{
				case "RansomDemand":
					nameString = "ACEN_RansomDemand".Translate();
					break;
				case "ItemStashFeeDemand":
					nameString = "ACEN_ItemStashFeeDemand".Translate();
					break;
				case "ThreatBig":
					nameString = "ACEN_ThreatBig".Translate();
					break;
				case "ThreatSmall":
					nameString = "ACEN_ThreatSmall".Translate();
					break;
				case "NegativeEvent":
					nameString = "ACEN_NegativeEvent".Translate();
					break;
				case "NeutralEvent":
					nameString = "ACEN_NeutralEvent".Translate();
					break;
				case "PositiveEvent":
					nameString = "ACEN_PositiveEvent".Translate();
					break;
				default:
					nameString = def.defName;
					break;
			}

			Color32 color = def.color; //Cast to Color32 for hex conversion
			string hexColor = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
			string colouredLabel = "<b><color=#" + hexColor + ">" + nameString + "</color></b>";

			return "ACEN_SetLetterPref".Translate(new string[] { colouredLabel });
		}

		public override void DefsLoaded()
		{
			base.DefsLoaded();

			Timer = this.Settings.GetHandle("Timer", "ACEN_Timer".Translate(), "ACEN_Tooltip_Timer".Translate(), 12);
			ShowMessages = this.Settings.GetHandle("ShowMessages", "ACEN_ShowMessages".Translate(), "ACEN_Tooltip_ShowMessages".Translate(), false);

			foreach (LetterDef def in DefDatabase<LetterDef>.AllDefs)
			{
				string defName = def.defName;
				bool closedByDefault = def == LetterDefOf.NeutralEvent || def == LetterDefOf.PositiveEvent;

				PrefByDef[def] = this.Settings.GetHandle($"Close{defName}", this.ColouredOptionLabel(def), "ACEN_Tooltip_SetLetterPref".Translate(), closedByDefault);
			}
		}

		public override string ModIdentifier => "AutocloseEventNotifications";
	}
}

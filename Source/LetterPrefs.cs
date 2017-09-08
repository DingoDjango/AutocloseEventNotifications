using Verse;
using UnityEngine;

namespace AutocloseEventNotifications
{
	public class LetterPrefs : IExposable
	{
		public string defName;

		public string customLabel;

		public bool closePreference;

		public string colouredLabel;

		public LetterPrefs()
		{
		}

		public LetterPrefs(LetterDef letterDef, bool shouldClose)
		{
			this.defName = letterDef.defName;

			//Custom names for vanilla letters. Can't account for modded letter types
			switch (this.defName)
			{
				case "BadNonUrgent":
					this.customLabel = "Non-Urgent";
					break;
				case "BadUrgent":
					this.customLabel = "Bad";
					break;
				case "RansomDemand":
					this.customLabel = "Ransom Demand";
					break;
				case "ItemStashFeeDemand":
					this.customLabel = "Item Stash Opportunity";
					break;
				default:
					this.customLabel = this.defName;
					break;
			}

			this.closePreference = shouldClose;

			Color32 color = letterDef.color;

			string hexColor = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");

			this.colouredLabel = "<b><color=#" + hexColor + ">" + this.customLabel + "</color></b>";
		}

		public void ExposeData()
		{
			Scribe_Values.Look(ref this.defName, "defName");
			Scribe_Values.Look(ref this.customLabel, "customLabel");
			Scribe_Values.Look(ref this.closePreference, "closePreference");
			Scribe_Values.Look(ref this.colouredLabel, "colouredLabel");
		}
	}
}

using HugsLib;
using RimWorld;
using Verse;

namespace AutocloseEN
{
	public class ACENMenu : ModBase
	{
		public override string ModIdentifier
		{
			get
			{
				return "AutocloseEventNotifications";
			}
		}

		public override void DefsLoaded()
		{
			ACENDefs();
		}

		public override void SettingsChanged()
		{
			ACENDefs();
		}

		private void ACENDefs()
		{
			var timetoClose = Settings.GetHandle<int>("ACENTimetoClose", "ACEN_setting_timetoClose_label".Translate(), "ACEN_setting_timetoClose_desc".Translate(), 12);
			AutocloseEventBoxes.ACENTimer = GenDate.TicksPerHour * timetoClose.Value;

			var showMessage = Settings.GetHandle<bool>("ACENShowMessage", "ACEN_setting_showMessage_label".Translate(), "ACEN_setting_showMessage_desc".Translate(), false);
			AutocloseEventBoxes.ShowMessage = showMessage.Value;

			var closeGood = Settings.GetHandle<bool>("ACENCloseGood", "ACEN_setting_closeGood_label".Translate(), "ACEN_setting_closeGood_desc".Translate(), true);
			AutocloseEventBoxes.CloseGood = closeGood.Value;

			var closeNonUrgent = Settings.GetHandle<bool>("ACENCloseNonUrgent", "ACEN_setting_closeNonUrgent_label".Translate(), "ACEN_setting_closeNonUrgent_desc".Translate(), true);
			AutocloseEventBoxes.CloseNonUrgent = closeNonUrgent.Value;

			var closeUrgent = Settings.GetHandle<bool>("ACENCloseUrgent", "ACEN_setting_closeUrgent_label".Translate(), "ACEN_setting_closeUrgent_desc".Translate(), false);
			AutocloseEventBoxes.CloseUrgent = closeUrgent.Value;
		}
	}
}
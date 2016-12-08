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
			UpdateDefs();
		}

		public override void SettingsChanged()
		{
			UpdateDefs();
		}

		private void UpdateDefs()
		{
			var timetoClose = Settings.GetHandle<int>("ACENTimetoClose", "setting_timetoClose_label".Translate(), "setting_timetoClose_desc".Translate(), 12);
			AutocloseEventBoxes.ACENTimer = GenDate.TicksPerHour * timetoClose.Value;

			var showMessage = Settings.GetHandle<bool>("ACENShowMessage", "setting_showMessage_label".Translate(), "setting_showMessage_desc".Translate(), false);
			AutocloseEventBoxes.ShowMessage = showMessage.Value;

			var closeGood = Settings.GetHandle<bool>("ACENCloseGood", "setting_closeGood_label".Translate(), "setting_closeGood_desc".Translate(), true);
			AutocloseEventBoxes.CloseGood = closeGood.Value;

			var closeNonUrgent = Settings.GetHandle<bool>("ACENCloseNonUrgent", "setting_closeNonUrgent_label".Translate(), "setting_closeNonUrgent_desc".Translate(), true);
			AutocloseEventBoxes.CloseNonUrgent = closeNonUrgent.Value;

			var closeUrgent = Settings.GetHandle<bool>("ACENCloseUrgent", "setting_closeUrgent_label".Translate(), "setting_closeUrgent_desc".Translate(), false);
			AutocloseEventBoxes.CloseUrgent = closeUrgent.Value;
		}
	}
}
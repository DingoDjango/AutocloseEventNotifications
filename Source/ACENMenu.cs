using HugsLib;
using AutocloseEN;

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
        var showMessage = Settings.GetHandle<bool>("ACENShowMessage", "Show message when closing notifications", "A silent message will be displayed when removing old notifications.", false);
        AutocloseEventBoxes.ShowMessage = showMessage.Value;

        var closeGood = Settings.GetHandle<bool>("ACENCloseGood", "Autoclose good (blue) notifications", "ACEN will automatically close positive (blue) event notifications.", true);
        AutocloseEventBoxes.CloseGood = closeGood.Value;

        var closeNonUrgent = Settings.GetHandle<bool>("ACENCloseNonUrgent", "Autoclose bad (yellow) notifications", "ACEN will automatically close negative (yellow) event notifications.", true);
        AutocloseEventBoxes.CloseNonUrgent = closeNonUrgent.Value;

        var closeUrgent = Settings.GetHandle<bool>("ACENCloseUrgent", "Autoclose urgent (red) notifications", "ACEN will automatically close urgent (red) event notifications.", false);
        AutocloseEventBoxes.CloseUrgent = closeUrgent.Value;
    }
}
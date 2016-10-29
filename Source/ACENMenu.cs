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
        var showMessage = Settings.GetHandle<bool>("ACENShowMessage", "Show a message when removing notifications", "A silent message will be dispalyed when removing an old non-urgent notification.", false);
        AutocloseEventBoxes.ShowMessage = showMessage.Value;
    }
}
using Microsoft.Toolkit.Uwp.Notifications;

namespace BuddyProductivityAssistant.Views.Helpers;
public static class NotificationHelper
{
    public static void Notifications(string title, string message)
    {
        //Send a notification toast to the user
        new ToastContentBuilder()
            .AddText(title)
            .AddText(message)
            .Show();
    }
}
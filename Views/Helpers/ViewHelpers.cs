using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace BuddyProductivityAssistant.Views.Helpers;

public static class ViewHelpers
{
    public static Task WaitForHomeButtonClicked(Button button)
    {
        var tcs = new TaskCompletionSource();

        RoutedEventHandler handler = null;
        handler = (s, e) =>
        {
            button.Click -= handler;
            tcs.SetResult();
        };

        button.Click += handler;

        return tcs.Task;
    }
    //Handles the Fade in and FadeOut of the elements when going to the Main Window
    public static Task FadeAsync(double fadeFrom, double fadeTo, UIElement[] elements, double durationSeconds)
    {

        var tcs = new TaskCompletionSource<bool>();
        int remaining = elements.Length;

        foreach (var element in elements)
        {
            var fade = new DoubleAnimation(fadeFrom, fadeTo, TimeSpan.FromSeconds(durationSeconds));
            fade.FillBehavior = FillBehavior.Stop;

            fade.Completed += (s, e) =>
            {
                element.Opacity = fadeTo;
                if (Interlocked.Decrement(ref remaining) == 0)
                {
                    tcs.SetResult(true);
                }
            };

            element.BeginAnimation(UIElement.OpacityProperty, fade);
        }

        return tcs.Task;
    }
}

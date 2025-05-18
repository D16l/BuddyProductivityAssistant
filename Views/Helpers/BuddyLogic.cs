using BuddyProductivityAssistant.Models;
using System.Windows.Media.Imaging;
using WpfImage = System.Windows.Controls.Image;

namespace BuddyProductivityAssistant.Views.Helpers;
public static class BuddyLogic
{
    private static bool _isBarkingSprite = false;
    private static string[] arrayOfCompliments = { 
        "Ótimo trabalho!", "Continue assim!",
        "Muito bom, não se desconcentre agora", 
        "Está indo bem", "Uau, seu trabalho é incrivel"};

    //Send a compliment notification for the user every minute
    public static void Compliments(object sender, EventArgs e)
    {
        var lastOfArray = arrayOfCompliments.Length;
        Random random = new Random();
        //Choose's a random item of the array
        NotificationHelper.Notifications("Buddy", arrayOfCompliments[random.Next(lastOfArray)]);
    }

    public static async Task Bark(WpfImage buddyImage)
    {
        //Prevents the user from bugging the sprite when repeatedly petting
        if (_isBarkingSprite) return;

        _isBarkingSprite = true;
        //Get the default sprite
        var theBuddyOriginalImage = buddyImage.Source;
        //Changes the sprite
        buddyImage.Source = new BitmapImage(new Uri("pack://application:,,,/Views/Sprites/the_buddy_bark.png", UriKind.Absolute));
        Dog.Instance.Bark();

        await Task.Delay(500);
        //Changes back the default sprite
        buddyImage.Source = theBuddyOriginalImage;
        _isBarkingSprite = false;
    }
}

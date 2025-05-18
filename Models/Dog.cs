using System.Windows.Media;

namespace BuddyProductivityAssistant.Models;

//Here is the Singleton class for the hole application
public sealed class Dog
{
    //Get a single instance of the class
    private static Dog? _instance;
    //Lock to prevent creating another instance of the class
    private static readonly object _lock = new object();
    //Private constructor to prevent creating empty instances
    private Dog() 
    {
        Happinesss = 100;
    }
    //Uses the lock in the getter and creates a single instance
    public static Dog Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new Dog();

                return _instance;
            }
        }
    }
    //Multiplier for the amount of needness the Buddy has
    private int _sadnessMultiplier = 1;
    public int Happinesss { get; private set; } = 100;

    private MediaPlayer _barkPlayer = new MediaPlayer();

    public void Pet()
    {
        Happinesss += 10;
    }
    //Method for updating the happiness of the Buddy and incremeting in the multiplier, this 
    //is important for the growing needness of the Buddy 
    public void UpdateStatus()
    {
        Happinesss = Math.Max(0, Happinesss - 10 * _sadnessMultiplier);
        _sadnessMultiplier++;
    }

    public void Bark()
    {
        _barkPlayer.Open(new Uri("Audio/bark.mp3", UriKind.Relative));
        _barkPlayer.MediaOpened += (s, e) => _barkPlayer.Play();
    }
}
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace matchgame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed, matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text=(tenthOfSecondsElapsed/10F).ToString("0.0s");
            if(matchesFound == 8)
            {
                timer.Stop();
                Label.Content = timeTextBlock.Text;
                timeTextBlock.Text = "Play again?";
                Label.Visibility = Visibility.Visible;
            }
        }

        private void SetUpGame()
        {
            Label.Visibility = Visibility.Hidden;
            List<string> FaceEmoji = new List<string>()
            {
                "😀","😀",
                "😁","😁",
                "😂","😂",
                "🤣","🤣",
                "😃","😃",
                "😄","😄",
                "😅","😅",
                "😆","😆",
            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(FaceEmoji.Count);
                    string nextEmoji = FaceEmoji[index];
                    textBlock.Text = nextEmoji;
                    FaceEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetUpGame();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock; if (findingMatch==false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if(textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch=false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
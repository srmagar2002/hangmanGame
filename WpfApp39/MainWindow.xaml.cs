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
using System.Windows.Threading;
using System.Reflection;

namespace WpfApp39
{

    public partial class MainWindow : Window
    {
        private char difficulty { get; set; } = '2';

        private HashSet<Key> pressedKeys;
        private HashSet<int> prevWordsIndex;
        private char keyPressed { get; set; }
        private string word { get; set; }
        private char[] word_char;
        private char[] blank_Spaces;

        private int winCount { get; set; }
        private int lossCount { get; set; }

        private List<char> wrongLetters;


        private int life { get; set; }

        private Random random;
        private WordsList wordsList;

        private DispatcherTimer timer;

        private bool? win;

        private int time { get; set; } = 10;
        private int sec { get; set; }

        private int hintLength { get; set; } = 1;
        private HashSet<int> hintLetterIndex;
        private int[] letterIndexArray;


        public MainWindow()
        {
            InitializeComponent();
            difficultyScreen();

            pressedKeys = new HashSet<Key>();
            wordsList = new WordsList();
            wrongLetters = new List<char>();
            prevWordsIndex = new HashSet<int>();

            timer = new DispatcherTimer()
            {

                Interval = TimeSpan.FromSeconds(1)
            };


            timer.Tick += theTimer;

            winCount = 0;
            lossCount = 0;
            life = 6;


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainLogic(sender);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z && win == null)
            {
                if (!pressedKeys.Contains(e.Key))
                {
                    pressedKeys.Add(e.Key);
                    keyPressed = (char)('A' + (e.Key - Key.A));
                    mainLogic(sender);
                }

            }
            else if (e.Key == Key.Enter && win != null)
            {
                cleanUp();
            }
        }


        private void newWord(object sender, RoutedEventArgs e)
        {
            cleanUp();
        }

        private void difficultyScreen()
        {
            this.Height = 500;
            this.Width = 800;
            uiCollapser(MyGrid);
            var difficultySelectScreen = new DifficultySelectScreen();
            DifficultySelector.Content = difficultySelectScreen;

            difficultySelectScreen.ButtonClicked += Difficulty;

        }

        private void Difficulty(object sender, string buttonContent)
        {
            if (buttonContent == "Extreme")
            {
                difficulty = '3';
                hintLength = 7;
                time = 20;
            }
            else if (buttonContent == "Hard")
            {
                difficulty = '2';
                hintLength = 4;
                time = 15;
            }
            else if (buttonContent == "Easy")
            {
                difficulty = '1';
                hintLength = 1;
                time = 10;
            }
            DifficultySelector.Content = null;

            this.Width = 800;
            this.Height = 1000;

            uiDCollapser(MyGrid);
            cleanUp();
            wordandHintGenerator();
        }

        private void wordandHintGenerator()
        {
            sec = time - 1;
            timerTextbox.Text = time.ToString(); ;

            random = new Random();

            if (difficulty == '3')
            {
                wordandhint(wordsList.wordCollectionsXtreme);
            }
            else if (difficulty == '2')
            {
                wordandhint(wordsList.wordCollectionsHard);
            }
            else if (difficulty == '1')
            {
                wordandhint(wordsList.wordCollections);
            }

            word_char = word.ToCharArray();

            blankSpaces();


            letterHinter();

            lettersfunction();
            scoreShow();
            showLoss();
        }

        private void wordandhint(WordCollections collection)
        {
            int ranWordIndex;
            do
            {
                ranWordIndex = random.Next(collection.Words.Count);
                if (!prevWordsIndex.Contains(ranWordIndex))
                {
                    word = collection.Words[ranWordIndex].word.ToUpper();
                    hint.Text = collection.Words[ranWordIndex].hint;
                    prevWordsIndex.Add(ranWordIndex);
                    break;
                }
            } while (ranWordIndex < collection.Words.Count);
        }

        private void theTimer(object sender, EventArgs e)
        {
            if (sec >= 0 && sec <= time)
            {
                timerTextbox.Text = sec.ToString();
                sec--;
            }
            else
            {
                timer.Stop();
                lossFunction();
            }
        }

        private void cleanUp()
        {
            life = 6;
            head.Opacity = 0;
            body.Opacity = 0;
            l_arm.Opacity = 0;
            r_arm.Opacity = 0;
            l_leg.Opacity = 0;
            r_leg.Opacity = 0;
            l_x_eye1.Opacity = 0;
            l_x_eye2.Opacity = 0;
            r_x_eye1.Opacity = 0;
            r_x_eye2.Opacity = 0;
            mouth.Opacity = 0;
            wrongLetters.Clear();
            wronglettersfunction();
            pressedKeys.Clear();
            buttonEnabler();
            wordandHintGenerator();
            newword.IsEnabled = false;
            win = null;
        }
        private void mainLogic(object sender)
        {
            timer.Start();
            char letter;
            if (keyPressed == '\0')
            {
                Button button = sender as Button;

                letter = char.Parse((string)button.Content);
            }
            else
            {
                letter = keyPressed;
            }

            bool exists = Array.Exists(word_char, element => element == letter);

            if (exists)
            {


                for (int i = 0; i < word_char.Length; i++)
                {
                    if (word_char[i] == letter)
                    {
                        blank_Spaces[i] = letter;

                    }
                }
                lettersfunction();
            }
            else
            {
                wrongLetters.Add(letter);
                wronglettersfunction();
                life--;

            }

            (FindName($"{letter}") as Button).IsEnabled = false;

            if (life == 5) { head.Opacity = 100; }
            if (life == 4) { body.Opacity = 100; }
            if (life == 3) { r_arm.Opacity = 100; }
            if (life == 2) { l_arm.Opacity = 100; }
            if (life == 1) { r_leg.Opacity = 100; }
            if (life == 0)
            {
                l_leg.Opacity = 100;
                r_x_eye1.Opacity = 100;
                r_x_eye2.Opacity = 100;
                l_x_eye1.Opacity = 100;
                l_x_eye2.Opacity = 100;
                mouth.Opacity = 100;
                lossFunction();
            }

            if (new string(blank_Spaces) == word && win != false)
            {
                winFunction();
            }
        }

        private void winFunction()
        {
            timer.Stop();
            win = true;
            hint.Text = "C O R R E C T !";
            buttonDisEnabler();
            newword.IsEnabled = true;
            winCount++;

        }

        private void lossFunction()
        {
            timer.Stop();
            win = false;
            buttonDisEnabler();
            blank_Spaces = word_char;
            lettersfunction();
            hint.Text = "Y O U L O S E";
            newword.IsEnabled = true;
            lossCount++;
        }


        private void lettersfunction()
        {
            theWord.Text = "";
            foreach (char c in blank_Spaces)
            {
                theWord.Text += c + " ";
            }
        }

        private void letterHinter()
        {
            hintLetterIndex = new HashSet<int>();
            random = new Random();
            letterIndexArray = new int[hintLength];

            for (int i = 0; i < hintLength; i++)
            {
                int hIndex;
                do
                {
                    hIndex = random.Next(word_char.Length);
                } while (hintLetterIndex.Contains(hIndex));

                hintLetterIndex.Add(hIndex);
                letterIndexArray[i] = hIndex;
            }

            for (int i = 0; i < hintLength; i++)
            {
                blank_Spaces[letterIndexArray[i]] = word_char[letterIndexArray[i]];
            }

        }

        private void blankSpaces()
        {
            blank_Spaces = new char[word_char.Length];

            for (int i = 0; i < word_char.Length; i++)
            {
                blank_Spaces[i] = '_';
            }
        }

        private void wronglettersfunction()
        {
            wrongLets.Text = "";
            foreach (char c in wrongLetters)
            {
                wrongLets.Text += c + " ";
            }
        }

        private void buttonDisEnabler()
        {
            for (char i = 'A'; i <= 'Z'; i++)
            {
                var button = $"{i}";

                (FindName(button) as Button).IsEnabled = false;
            }
        }

        private void buttonEnabler()
        {
            for (char i = 'A'; i <= 'Z'; i++)
            {
                var button = $"{i}";

                (FindName(button) as Button).IsEnabled = true;
            }
        }

        private void scoreShow()
        {
            score.Text = $"{winCount}";
        }

        private void showLoss()
        {
            loss.Text = $"{lossCount}";
        }

        private void uiCollapser(Panel container)
        {
            foreach (UIElement uIElement in container.Children)
            {

                if (!(uIElement is ContentControl contentControl) || uIElement is Button button)

                {
                    uIElement.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void uiDCollapser(Panel container)
        {
            foreach (UIElement uIElement in container.Children)
            {

                if (!(uIElement is ContentControl contentControl) || uIElement is Button button)

                {
                    uIElement.Visibility = Visibility.Visible;
                }
            }

        }

        private void selectDif_Click(object sender, RoutedEventArgs e)
        {
            difficultyScreen();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //  extra.Text = Width.ToString();

        }
    }
}
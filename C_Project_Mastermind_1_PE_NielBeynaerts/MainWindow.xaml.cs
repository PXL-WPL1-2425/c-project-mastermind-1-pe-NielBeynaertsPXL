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

namespace C_Project_Mastermind_1_PE_NielBeynaerts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        private Button selectedButton; // Track the currently selected button

        string[] selectedColors = new string[4];
        string[] randomColorSelection = new string[4];
        int selectedColorPosition = 0;

        SolidColorBrush[] chosenColors = new SolidColorBrush[4]; // Initialize array to hold 4 colors

        private DispatcherTimer timer = new DispatcherTimer();

        int attempts = 0;
        public MainWindow()
        {
            InitializeComponent();

            CreateRandomColorCombination();
        }

        public void CreateRandomColorCombination()
        {
            //SolidColorBrush[] colors = { Brushes.Red, Brushes.Yellow, Brushes.Green, Brushes.Blue, Brushes.White, Brushes.Orange };
            string[] colorsName = { "Red", "Yellow", "Green", "Blue", "White", "Orange" };

            int[] randomColors = new int[4];

            for (int i = 0; i < randomColors.Length; i++)
            {
                randomColors[i] = rnd.Next(0, colorsName.Length); // Enters a number into array
            }

            // Display color names in the window title for debugging
            // RandomColors[n] gives a number, this number is entered in 'ColorsName[]', this gives the name of a color and displays it as title
            // this.Title = $"{colorsName[randomColors[0]]}, {colorsName[randomColors[1]]}, {colorsName[randomColors[2]]}, {colorsName[randomColors[3]]}";

            attempts++;
            this.Title = $"Poging {attempts}";

            randomColorSelection[0] = colorsName[randomColors[0]];
            randomColorSelection[1] = colorsName[randomColors[1]];
            randomColorSelection[2] = colorsName[randomColors[2]];
            randomColorSelection[3] = colorsName[randomColors[3]];

            StartCountdown();
        }

        private void color1Button_Click(object sender, RoutedEventArgs e)
        {
            radioButtonsGroupBox.Visibility = Visibility.Visible;
            selectedButton = color1Button;
            selectedColorPosition = 0;
            UncheckRadioButtons();
        }

        private void color2Button_Click(object sender, RoutedEventArgs e)
        {
            radioButtonsGroupBox.Visibility = Visibility.Visible;
            selectedButton = color2Button;
            selectedColorPosition = 1;
            UncheckRadioButtons();
        }

        private void color3Button_Click(object sender, RoutedEventArgs e)
        {
            radioButtonsGroupBox.Visibility = Visibility.Visible;
            selectedButton = color3Button;
            selectedColorPosition = 2;
            UncheckRadioButtons();
        }

        private void color4Button_Click(object sender, RoutedEventArgs e)
        {
            radioButtonsGroupBox.Visibility = Visibility.Visible;
            selectedButton = color4Button;
            selectedColorPosition = 3;
            UncheckRadioButtons();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton colorRadioButton)
            {
                switch (colorRadioButton.Content.ToString())
                {
                    case "Rood":
                        selectedButton.Background = Brushes.Red;
                        selectedColors[selectedColorPosition] = "Red";
                        break;
                    case "Geel":
                        selectedButton.Background = Brushes.Yellow;
                        selectedColors[selectedColorPosition] = "Yellow";
                        break;
                    case "Groen":
                        selectedButton.Background = Brushes.Green;
                        selectedColors[selectedColorPosition] = "Green";
                        break;
                    case "Blauw":
                        selectedButton.Background = Brushes.Blue;
                        selectedColors[selectedColorPosition] = "Blue";
                        break;
                    case "Wit":
                        selectedButton.Background = Brushes.White;
                        selectedColors[selectedColorPosition] = "White";
                        break;
                    case "Oranje":
                        selectedButton.Background = Brushes.Orange;
                        selectedColors[selectedColorPosition] = "Orange";
                        break;
                    default:
                        break;
                }
            }

        }

        private void UncheckRadioButtons()
        {
            redRadioButton.IsChecked = false;
            greenRadioButton.IsChecked = false;
            blueRadioButton.IsChecked = false;
            whiteRadioButton.IsChecked = false;
            orangeRadioButton.IsChecked = false;
            yellowRadioButton.IsChecked = false;
        }

        private void validateColorCode_Click(object sender, RoutedEventArgs e)
        {
            // Define an array of buttons corresponding to each selected color
            Button[] buttons = { color1Button, color2Button, color3Button, color4Button };

            // Loop through each selected color and its corresponding button
            for (int i = 0; i < selectedColors.Length; i++)
            {
                // Check for an exact match
                if (selectedColors[i] == randomColorSelection[i])
                {
                    SetButtonStyle(buttons[i], new Thickness(2, 2, 2, 20), Colors.DarkRed);
                    StopCountdown();
                    StartCountdown();
                }
                // Check for a partial match
                else if (randomColorSelection.Contains(selectedColors[i]))
                {
                    SetButtonStyle(buttons[i], new Thickness(2, 2, 2, 20), Colors.Wheat);
                    StopCountdown();
                    StartCountdown();
                }

                if (selectedColors[0] == randomColorSelection[0] && selectedColors[1] == randomColorSelection[1] && selectedColors[2] == randomColorSelection[2] && selectedColors[3] == randomColorSelection[3])
                {
                    Close();
                    StopCountdown();
                }
            }
            attempts++;
            this.Title = $"Poging {attempts}";
            
        }

        private void SetButtonStyle(Button button, Thickness thickness, Color color)
        {
            button.BorderThickness = thickness;
            button.BorderBrush = new SolidColorBrush(color);
        }


        private void ToggleDebug()
        {
            randomGeneratedCodeTextBox.Text = $"{randomColorSelection[0]}, {randomColorSelection[1]}, {randomColorSelection[2]}, {randomColorSelection[3]}";
            randomGeneratedCodeTextBox.Visibility = Visibility.Visible;
        }

        DateTime startTime;
        TimeSpan elapsedTime;


        private void StartCountdown()
        {
            startTime = DateTime.Now;
            timer.Tick += Timer_Tick; //Event koppelen
            timer.Interval = new TimeSpan(0, 0, 1); //Elke seconde
            timer.Start(); //Timer starten
        }


        private void StopCountdown()
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = DateTime.Now - startTime;
            timerLabel.Content = elapsedTime.ToString("ss");

            if(elapsedTime.TotalSeconds > 10)
            {
                StopCountdown();
                attempts++;
                this.Title = $"Poging {attempts}";
                StartCountdown();
            }

        }



    }
}
using Integral.GUI.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Integral.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Brush DEFAULT_BACKGROUND_COLOR;
        private readonly Brush SELECTED_BACKGROUND_COLOR;
        private Button selectedButton;

        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
            DEFAULT_BACKGROUND_COLOR = rectangle.Background.Clone();
            SELECTED_BACKGROUND_COLOR = new SolidColorBrush(Color.FromRgb(0, 123, 255));

            selectedButton = rectangle;
            selectedButton.Background = SELECTED_BACKGROUND_COLOR;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+[.[0-9]+]?");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TabSelected(object sender, RoutedEventArgs e)
        {
            if (selectedButton != null)
            {
                selectedButton.Background = DEFAULT_BACKGROUND_COLOR;
            }

            var btn = (Button)sender;
            selectedButton = btn;
            btn.Background = SELECTED_BACKGROUND_COLOR;
        }
    }
}

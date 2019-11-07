using Integral.Calculator;
using Integral.GUI.Infrastructure;
using System;
using System.Windows;

namespace Integral.GUI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private IIntegral _calculator = new RectangleRuleIntegral();
        private readonly Compiler.Compiler _compiler = new Compiler.Compiler();

        private double from = -10;
        public double From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged(nameof(From));
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        private double to = 10;
        public double To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged(nameof(To));
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        private int n = 10;
        public int N
        {
            get { return n; }
            set
            {
                n = value;
                OnPropertyChanged(nameof(N));
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        private string formula = "pow(x, 2) + x + 1";

        public string Formula
        {
            get { return formula; }
            set
            {
                formula = value;
                OnPropertyChanged(nameof(Formula));
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        private double result;

        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public RelayCommand CalculateCommand { get; set; }
        public RelayCommand ChangeAlgorithmCommand { get; set; }

        public MainWindowViewModel()
        {
            CalculateCommand = new RelayCommand(ExecuteCalculateCommand, q => !string.IsNullOrWhiteSpace(Formula) && !_isCalculating && From <= To && n > 0 && n < 1000);
            ChangeAlgorithmCommand = new RelayCommand(ExecuteChangeAlgorithmCommand, q => true);
        }

        private bool _isCalculating;

        /// <summary>
        /// Calculates integral with given inputs.
        /// </summary>
        private async void ExecuteCalculateCommand(object parameter)
        {
            _isCalculating = true;
            CalculateCommand.RaiseCanExecuteChanged();

            // Compiles formula expression and gets function pointer on formula.
            var cr = await _compiler.Compile(Formula).ConfigureAwait(false);
            if (cr.Errors.Count == 0)
            {
                // f is function pointer.
                var f = _compiler.GetLambda(cr);
                Result = await _calculator.Integrate(f, From, To, N);
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                // Enables "Calculate" button.
                _isCalculating = false;
                CalculateCommand.RaiseCanExecuteChanged();
            });
        }

        /// <summary>
        /// Factory method on algorithm strategy.
        /// </summary>
        /// <param name="parameter">Clicked algorithm identifier.</param>
        private async void ExecuteChangeAlgorithmCommand(object parameter)
        {
            var param = Convert.ToInt16(parameter);

            if (param == 0x01)
            {
                _calculator = new RectangleRuleIntegral();
            }
            else if (param == 0x02)
            {
                _calculator = new TableRuleIntegral();
            }
            else
            {
                _calculator = new SimpsonsRuleIntegral();
            }
        }
    }
}

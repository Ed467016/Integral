using Integral.GUI.Infrastructure;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.GUI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly Calculator.Calculator _calculator = new Calculator.Calculator();
        private readonly Compiler.Compiler _compiler = new Compiler.Compiler();
        private const double _dx = 0.1;

        private double from;
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

        private double to;
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

        private string formula;

        public string Formula
        {
            get { return formula; }
            set
            {
                formula = value;
                OnPropertyChanged(nameof(Formula));
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


        private PlotModel mainPlot;

        public PlotModel MainPlot
        {
            get { return mainPlot; }
            set
            {
                mainPlot = value;
                OnPropertyChanged(nameof(MainPlot));
            }
        }

        public IPlotController Controller { get; set; } = new PlotController();

        public RelayCommand CalculateCommand { get; set; }

        public MainWindowViewModel()
        {
            CalculateCommand = new RelayCommand(ExecuteCalculateCommand, q => From <= To);
        }

        private void ExecuteCalculateCommand(object parameter)
        {
            var cr = _compiler.Compile(formula);
            if (cr.Errors.Count == 0)
            {
                var f = _compiler.GetLambda(cr);
                Result = _calculator.Integrate(f, From, To);

                var model = new PlotModel() { Title = "Equatation visualization." };
                try
                {
                    model.Series.Add(new FunctionSeries(f, From, To, _dx));
                }
                catch { }
                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MaximumPadding = 0.1,
                    MinimumPadding = 0.1
                });
                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MaximumPadding = 0.1,
                    MinimumPadding = 0.1
                });
                MainPlot = model;
            }
        }
    }
}

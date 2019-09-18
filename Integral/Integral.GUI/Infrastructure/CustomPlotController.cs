using OxyPlot;

namespace Integral.GUI.Infrastructure
{
    public class CustomPlotController : PlotController
    {
        public CustomPlotController()
        {
            this.UnbindAll();
            this.BindKeyDown(OxyKey.Left, PlotCommands.PanRight);
            this.BindKeyDown(OxyKey.Right, PlotCommands.PanLeft);
        }
    }
}

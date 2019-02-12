using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace MaxLifts.Controllers
{
    public class ProgressController : Controller
    {
        public DAL.MaxLiftsContext db = new DAL.MaxLiftsContext();
        // GET: Progress
        public ActionResult Chart()
        {
            return View();
        }

        public ActionResult ProgressChart()
        {

            var deads = db.Sets.Where(s => s.User == User.Identity.Name && s.Exercise == "Deadlift").OrderBy(s => s.SetDate);
            var bench = db.Sets.Where(s => s.User == User.Identity.Name && s.Exercise == "Bench Press").OrderBy(s => s.SetDate);
            var press = db.Sets.Where(s => s.User == User.Identity.Name && s.Exercise == "Press").OrderBy(s => s.SetDate);
            var squat = db.Sets.Where(s => s.User == User.Identity.Name && s.Exercise == "Squat").OrderBy(s => s.SetDate);
            var Max = db.Sets.Where(s => s.User == User.Identity.Name).Max(s => s.CalculatedMax);

            int[] deadsCalcMaxes = deads.Select( s => s.CalculatedMax).ToArray();
            DateTime[] deadsDates = deads.Select(s => s.SetDate).ToArray();

            int[] benchCalcMaxes = bench.Select(s => s.CalculatedMax).ToArray();
            DateTime[] benchDates = bench.Select(s => s.SetDate).ToArray();

            int[] squatCalcMaxes = squat.Select(s => s.CalculatedMax).ToArray();
            DateTime[] squatDates = squat.Select(s => s.SetDate).ToArray();

            int[] pressCalcMaxes = press.Select(s => s.CalculatedMax).ToArray();
            DateTime[] pressDates = press.Select(s => s.SetDate).ToArray();


            var chart = new Chart(width: 750, height: 400, theme:ChartTheme.Blue)
                .AddTitle("Progression Chart")
                .AddLegend("Lifts", "Lift")
                .SetYAxis("Weight(lbs)")
                
                
                .AddSeries(
                    
                    name: "Dead Lift",
                    legend: "Lift",
                    chartType: "Line",
                                     
                    
                    yValues: deadsCalcMaxes
                     
                )
                .AddSeries(
                        name: "Bench Press",
                        legend: "Lift",
                        chartType:"Line",
                        xValue: benchDates,
                        yValues: benchCalcMaxes                                     
                
                )
                .AddSeries(
                    name: "Squat",
                    legend: "Lift",
                    chartType: "Line",
                    xValue: squatDates,
                    yValues: squatCalcMaxes

                )
                .AddSeries(
                        name: "Press",
                        legend: "Lift",
                        chartType: "Line",
                        xValue: pressDates,
                        yValues: pressCalcMaxes

                )
                

            .GetBytes("png");

           
             
            return File(chart, "image/bytes");
        }
    }
}
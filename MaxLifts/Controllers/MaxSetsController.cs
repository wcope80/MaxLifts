using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaxLifts.Models;
using System.Data.Entity;
using MaxLifts.Helpers;
using PagedList;

namespace MaxLifts.Controllers
{
    public class MaxSetsController : Controller
    {
        public DAL.MaxLiftsContext db = new DAL.MaxLiftsContext();          
        [Authorize]
        public ActionResult MaxSets(string lift, int? page)
        {
            var sets = from s in db.Sets
                           select s;


            if (lift != "All" && lift != null )
            {
                //add code to filter based on lift
                //var sets = (from s in db.Sets
                //            where s.Exercise == lift && s.User == User.Identity.Name
                //            select s).ToList();
                ViewBag.Message = lift;
               sets = db.Sets.Where(s => s.Exercise == lift && s.User== User.Identity.Name).OrderBy(s => s.SetDate);
            }
            else
            {
                ViewBag.Message = "All";
               sets = db.Sets.Where(s => s.User == User.Identity.Name).OrderBy(s => s.SetDate);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(sets.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddSet(string lift)
        {
            if (lift == "All")
            {
                ViewBag.exerciseList = new SelectList(ExerciseList(), "Value", "Text");
            }
            else
            {
                ViewBag.exerciseList = new SelectList(ExerciseList(), "Value", "Text", lift);
            }
            Set set = new Set();
            
            return View(set);
        }

        [Authorize]
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Add")]
        public ActionResult AddSet(Set set)
        {
            
            if (ModelState.IsValid)
            {
                double dweight = System.Convert.ToDouble(set.Weight);
                double dreps = System.Convert.ToDouble(set.Reps);
                set.CalculatedMax = System.Convert.ToInt16(.0333 * dweight * dreps + dweight);
                set.User = User.Identity.Name;
                db.Sets.Add(set);
                db.SaveChanges();
                return RedirectToAction("MaxSets", "MaxSets", new { lift = set.Exercise });
            }
            ViewBag.exerciseList = new SelectList(ExerciseList(), "Value", "Text", set.Exercise);
            return View(set);
        }

        public ActionResult EditSet(int setId)
        {
            ViewBag.exerciseList = new SelectList(ExerciseList(), "Value", "Text");
            return View(db.Sets.Where(s=>s.SetId == setId).FirstOrDefault());
        } 


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Edit")]
        public ActionResult EditSet (Set set)
        {
            if (ModelState.IsValid)
            {
                double dweight = System.Convert.ToDouble(set.Weight);
                double dreps = System.Convert.ToDouble(set.Reps);
                set.CalculatedMax = System.Convert.ToInt16(.0333 * dweight * dreps + dweight);
                set.User = User.Identity.Name;
                db.Entry(set).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MaxSets", "MaxSets", new { lift = set.Exercise });
            }

            ViewBag.exerciseList = new SelectList(ExerciseList(), "Value", "Text", set.Exercise);
            return View(set);
        }



        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Delete")]
        public ActionResult DeleteSet(Set set)
        {
            //Set set = db.Sets.Where(s => s.SetId == setId).First();
            db.Entry(set).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("MaxSets");
        }





        //[HttpPost]
        //public ActionResult DeleteSet(int setId)
        //{
        //    Set set = db.Sets.Where(s => s.SetId == setId).First();
        //    db.Sets.Remove(set);
        //    db.SaveChanges();
        //    return RedirectToAction("MaxSets");
        //}
    

        public IEnumerable<SelectListItem> ExerciseList()
        {
            List<SelectListItem> exerciseList = new List<SelectListItem>()
                {
                new SelectListItem() { Text = "Deadlift", Value = "Deadlift" },
                new SelectListItem() { Text = "Squat", Value = "Squat" },
                new SelectListItem() { Text = "Bench Press", Value = "Bench Press" },
                new SelectListItem() { Text="Press", Value ="Press"}
                };
            return exerciseList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP01M06.Persistance;
using BO;
using System.Data.Entity;

namespace TP01M06.Controllers
{
    public class ArmeController : Controller
    {
        public class ArmesController : Controller
        {
            private Context db = new Context();

            public ActionResult Index()
            {
                return View(db.Armes.ToList());
            }

            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                Arme arme = db.Armes.Find(id);
                if (arme == null)
                {
                    return HttpNotFound();
                }
                return View(arme);
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,Nom,Degats")] Arme arme)
            {
                if (ModelState.IsValid)
                {
                    db.Armes.Add(arme);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(arme);
            }

             public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                Arme arme = db.Armes.Find(id);
                if (arme == null)
                {
                    return HttpNotFound();
                }
                return View(arme);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,Nom,Degats")] Arme arme)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(arme).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(arme);
            }

             public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                Arme arme = db.Armes.Find(id);
                if (arme == null)
                {
                    return HttpNotFound();
                }

                var samourais = db.Samourais.Where(s => s.Arme.Id == id).ToList();
                if (samourais.Any())
                {
                    ViewBag.Samourais = samourais.Select(s => s.Nom).ToList();
                }
                return View(arme);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Arme arme = db.Armes.Find(id);

                var samourais = db.Samourais.Where(s => s.Arme.Id == id).ToList();
                foreach (var samourai in samourais)
                {
                    samourai.Arme = null;
                }
                db.Armes.Remove(arme);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoSample.Models;

namespace ToDoSample.Controllers
{
    public class ToDoController : Controller
    {
        private ToDoContext db = new ToDoContext();

        // GET: ToDoModels
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int status=0)
        {
            //0=all,1=active,2=completed
            ViewBag.status = status;
            IEnumerable<ToDoModel> _result = db.ToDoModels;
            if (status==1)
            {
                _result= _result.Where(x => x.IsCompleted == false);
            }
            else  if (status==2)
            {
                _result = _result.Where(x => x.IsCompleted);
            }
            return View(_result.OrderBy(x=>x.IsCompleted).ThenBy(x=>x.CreateTime).ToList());
        }

        // GET: ToDoModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoModel toDoModel = db.ToDoModels.Find(id);
            if (toDoModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoModel);
        }

        // GET: ToDoModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToDo")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                toDoModel.Id = Guid.NewGuid();
                toDoModel.CreateTime = DateTime.Now;
                toDoModel.IsCompleted = false;
                db.ToDoModels.Add(toDoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoModel);
        }
        [HttpPost]
        public ActionResult CreateToDo([Bind(Include = "ToDo")] ToDoModel toDoModel)
        {
            try
            {
                toDoModel.Id = Guid.NewGuid();
                toDoModel.CreateTime = DateTime.Now;
                toDoModel.IsCompleted = false;
                db.ToDoModels.Add(toDoModel);
                db.SaveChanges();
                return Json(new AjaxResult() { status = "success", message = "" });
            }
            catch (Exception)
            {
                return Json(new AjaxResult() { status = "error", message = "" });
                
            }

        }


        // GET: ToDoModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoModel toDoModel = db.ToDoModels.Find(id);
            if (toDoModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoModel);
        }

        // POST: ToDoModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToDo")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoModel);
        }

        [HttpPost]
        public ActionResult IsCompleted([Bind(Include = "IsCompleted,Id")] ToDoModel toDoModel)
        {
            try
            {
                 ToDoModel _toDoModel = db.ToDoModels.Find(toDoModel.Id);
                _toDoModel.IsCompleted = toDoModel.IsCompleted;
                db.Entry(_toDoModel).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new AjaxResult() { status = "success", message = "" });
            }
            catch (Exception)
            {
                return Json(new AjaxResult() { status = "error", message = "" });

            }
        }

        // GET: ToDoModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoModel toDoModel = db.ToDoModels.Find(id);
            if (toDoModel == null)
            {
                return HttpNotFound();
            }
            return View(toDoModel);
        }

        // POST: ToDoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ToDoModel toDoModel = db.ToDoModels.Find(id);
            db.ToDoModels.Remove(toDoModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteToDo(Guid id)
        {
            
            try
            {
                ToDoModel toDoModel = db.ToDoModels.Find(id);
                db.ToDoModels.Remove(toDoModel);
                db.SaveChanges();
                return Json(new AjaxResult() { status = "success", message = "" });
            }
            catch (Exception)
            {
                return Json(new AjaxResult() { status = "error", message = "" });

            }
        }
        [HttpPost]
        public ActionResult DeleteAllCompleteToDo()
        {
            try
            {
                foreach (var item in db.ToDoModels.Where(x=>x.IsCompleted).ToList())
                {
                    db.ToDoModels.Remove(item);
                }
                db.SaveChanges();
                return Json(new AjaxResult() { status = "success", message = "" });
            }
            catch (Exception)
            {
                return Json(new AjaxResult() { status = "error", message = "" });

            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace TakYab.Areas.Ads.Controllers
{
    public class CarController : Controller
    {
        private TakYabEntities db = new TakYabEntities();

        //
        // GET: /Advertising/Car/

        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.AdType).Include(c => c.BuildYear).Include(c => c.PriceRange).Include(c => c.Priority).Include(c => c.Province).Include(c => c.SubModel);
            return View(cars.ToList());
        }


        public ActionResult Test()
        {
            return View();
        }

        //
        // GET: /Advertising/Car/Details/5

        public ActionResult Details(Guid id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // GET: /Advertising/Car/Create

        public ActionResult Create()
        {
            ViewBag.AdTypeId = new SelectList(db.AdTypes.OrderBy(m => m.SortOrder), "AdTypeId", "Name");
            ViewBag.BuildYearId = new SelectList(db.BuildYears.OrderBy(m => m.SortOrder), "BuildYearId", "Name");
            ViewBag.PriceRangeId = new SelectList(db.PriceRanges.OrderBy(m => m.SortOrder), "PriceRangeId", "Name");
            ViewBag.PriorityId = new SelectList(db.Priorities.OrderBy(m => m.SortOrder), "PriorityId", "Name");
            ViewBag.ProvinceId = new SelectList(db.Provinces.OrderBy(m => m.SortOrder), "ProvinceId", "Name");
            ViewBag.ModelId = new SelectList(db.Models.OrderBy(m => m.SortOrder), "ModelId", "Name");
            ViewBag.SubModelId = new SelectList(db.SubModels.OrderBy(m => m.SortOrder), "SubModelId", "Name");
            ViewBag.OutsideColourId = new SelectList(db.Colours.OrderBy(m => m.SortOrder), "ColourId", "Name");
            ViewBag.InsideColourId = new SelectList(db.Colours.OrderBy(m => m.SortOrder), "ColourId", "Name");
            ViewBag.FuelTypeId = new SelectList(db.FuelTypes.OrderBy(m => m.SortOrder), "FuelTypeId", "Name");
            ViewBag.InsuranceTypeId = new SelectList(db.InsuranceTypes.OrderBy(m => m.SortOrder), "InsuranceTypeId", "Name");
            ViewBag.CarStatusId = new SelectList(db.CarStatus.OrderBy(m => m.SortOrder), "CarStatusId", "Name");
            return View();
        }

        //
        // POST: /Advertising/Car/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car.CarId = Guid.NewGuid();

                var carSortNumber = db.Cars.Count() + 1;
                car.SortOrder = carSortNumber;
                car.AdCreatedDate = DateTime.Now;
                car.AdValidUntil = DateTime.Now.AddMonths(1);


                if (Request.Files != null)
                    for (var i = 0; i < Request.Files.Count && i < 5; i++)
                    {
                        if (Request.Files[i].ContentLength > 0)
                        {
                            var fileName = System.IO.Path.GetFileName(Request.Files[i].FileName);
                            var photoPath = car.CarId + "\\ImageURI" + (i + 1) + "\\";
                            var directory = System.Configuration.ConfigurationManager.AppSettings["ImagesFolderName"] + photoPath;
                            if (!System.IO.Directory.Exists(directory))
                                System.IO.Directory.CreateDirectory(directory);

                            var path = System.IO.Path.Combine(directory, "Original.jpg");
                            Request.Files[i].SaveAs(path);

                            var thumbnail = directory + "Thumbnail.jpg";
                            TakYab.Controllers.ImageManagerController.resizeImage(path, 200, 150, thumbnail);

                            var medium = directory + "Medium.jpg";
                            TakYab.Controllers.ImageManagerController.resizeImage(path, 640, 480, medium);

                            if (i == 0)
                                car.ImageURI1 = photoPath;

                            if (i == 1)
                                car.ImageURI2 = photoPath;

                            if (i == 2)
                                car.ImageURI3 = photoPath;

                            if (i == 3)
                                car.ImageURI4 = photoPath;

                            if (i == 4)
                                car.ImageURI5 = photoPath;


                        }
                    }


                db.Cars.Add(car);
                db.SaveChanges();

                return RedirectToAction("Index", "Car", new { @id = 2, are = "Ads" });
            }

            ViewBag.AdTypeId = new SelectList(db.AdTypes.OrderBy(m => m.SortOrder), "AdTypeId", "Name");
            ViewBag.BuildYearId = new SelectList(db.BuildYears.OrderBy(m => m.SortOrder), "BuildYearId", "Name");
            ViewBag.PriceRangeId = new SelectList(db.PriceRanges.OrderBy(m => m.SortOrder), "PriceRangeId", "Name");
            ViewBag.PriorityId = new SelectList(db.Priorities.OrderBy(m => m.SortOrder), "PriorityId", "Name");
            ViewBag.ProvinceId = new SelectList(db.Provinces.OrderBy(m => m.SortOrder), "ProvinceId", "Name");
            ViewBag.ModelId = new SelectList(db.Models.OrderBy(m => m.SortOrder), "ModelId", "Name");
            ViewBag.SubModelId = new SelectList(db.SubModels.OrderBy(m => m.SortOrder), "SubModelId", "Name");
            ViewBag.OutsideColourId = new SelectList(db.Colours.OrderBy(m => m.SortOrder), "ColourId", "Name");
            ViewBag.InsideColourId = new SelectList(db.Colours.OrderBy(m => m.SortOrder), "ColourId", "Name");
            ViewBag.FuelTypeId = new SelectList(db.FuelTypes.OrderBy(m => m.SortOrder), "FuelTypeId", "Name");
            ViewBag.InsuranceTypeId = new SelectList(db.InsuranceTypes.OrderBy(m => m.SortOrder), "InsuranceTypeId", "Name");
            ViewBag.CarStatusId = new SelectList(db.CarStatus.OrderBy(m => m.SortOrder), "CarStatusId", "Name");
            return View(car);
        }

        //
        // GET: /Advertising/Car/Edit/5

        public ActionResult Edit(Guid id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            ViewBag.AdTypeIdList = db.AdTypes.OrderBy(m => m.SortOrder);
            ViewBag.BuildYearIdList = db.BuildYears.OrderBy(m => m.SortOrder);
            ViewBag.PriceRangeIdList = db.PriceRanges.OrderBy(m => m.SortOrder);
            ViewBag.PriorityIdList = db.Priorities.OrderBy(m => m.SortOrder);
            ViewBag.ProvinceIdList = db.Provinces.OrderBy(m => m.SortOrder);
            ViewBag.ModelIdList = db.Models.OrderBy(m => m.SortOrder);
            ViewBag.SubModelIdList = db.SubModels.OrderBy(m => m.SortOrder);
            ViewBag.OutsideColourIdList = db.Colours.OrderBy(m => m.SortOrder);
            ViewBag.InsideColourIdList = db.Colours.OrderBy(m => m.SortOrder);
            ViewBag.FuelTypeIdList = db.FuelTypes.OrderBy(m => m.SortOrder);
            ViewBag.InsuranceTypeIdList = db.InsuranceTypes.OrderBy(m => m.SortOrder);
            ViewBag.CarStatusIdList = db.CarStatus.OrderBy(m => m.SortOrder);


            return View(car);
        }

        //
        // POST: /Advertising/Car/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;

                if (Request.Files != null)
                    for (var i = 0; i < Request.Files.Count && i < 5; i++)
                    {
                        if (Request.Files[i].ContentLength > 0)
                        {
                            var fileName = System.IO.Path.GetFileName(Request.Files[i].FileName);
                            var photoPath = car.CarId + "\\ImageURI" + (i + 1) + "\\";
                            var directory = System.Configuration.ConfigurationManager.AppSettings["ImagesFolderName"] + photoPath;
                            if (!System.IO.Directory.Exists(directory))
                                System.IO.Directory.CreateDirectory(directory);

                            var path = System.IO.Path.Combine(directory, "Original.jpg");
                            Request.Files[i].SaveAs(path);

                            var thumbnail = directory + "Thumbnail.jpg";
                            TakYab.Controllers.ImageManagerController.resizeImage(path, 200, 150, thumbnail);

                            var medium = directory + "Medium.jpg";
                            TakYab.Controllers.ImageManagerController.resizeImage(path, 640, 480, medium);

                            if (i == 0)
                                car.ImageURI1 = photoPath;

                            if (i == 1)
                                car.ImageURI2 = photoPath;

                            if (i == 2)
                                car.ImageURI3 = photoPath;

                            if (i == 3)
                                car.ImageURI4 = photoPath;

                            if (i == 4)
                                car.ImageURI5 = photoPath;


                        }
                    }



                db.SaveChanges();

                return RedirectToAction("Index", "Car", new { @id = 2, are = "Ads" });
            }
            ViewBag.AdTypeIdList = db.AdTypes.OrderBy(m => m.SortOrder);
            ViewBag.BuildYearIdList = db.BuildYears.OrderBy(m => m.SortOrder);
            ViewBag.PriceRangeIdList = db.PriceRanges.OrderBy(m => m.SortOrder);
            ViewBag.PriorityIdList = db.Priorities.OrderBy(m => m.SortOrder);
            ViewBag.ProvinceIdList = db.Provinces.OrderBy(m => m.SortOrder);
            ViewBag.ModelIdList = db.Models.OrderBy(m => m.SortOrder);
            ViewBag.SubModelIdList = db.SubModels.OrderBy(m => m.SortOrder);
            ViewBag.OutsideColourIdList = db.Colours.OrderBy(m => m.SortOrder);
            ViewBag.InsideColourIdList = db.Colours.OrderBy(m => m.SortOrder);
            ViewBag.FuelTypeIdList = db.FuelTypes.OrderBy(m => m.SortOrder);
            ViewBag.InsuranceTypeIdList = db.InsuranceTypes.OrderBy(m => m.SortOrder);
            ViewBag.CarStatusIdList = db.CarStatus.OrderBy(m => m.SortOrder);

            return View(car);
        }

        //
        // GET: /Advertising/Car/Delete/5

        public ActionResult Delete(Guid id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        //
        // POST: /Advertising/Car/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index", "Car", new { @id = 2, are = "Ads" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        public ActionResult CarPanel()
        {
            var cars = db.Cars.Include(c => c.AdType).Include(c => c.BuildYear).Include(c => c.PriceRange).Include(c => c.Priority).Include(c => c.Province).Include(c => c.SubModel).Where(x => x.AdStatus.Code == "Approved");
            return View(cars.ToList());
        }

        public ActionResult ListCars()
        {
            var cars = db.Cars.Include(c => c.AdType).Include(c => c.BuildYear).Include(c => c.PriceRange).Include(c => c.Priority).Include(c => c.Province).Include(c => c.SubModel)
                .OrderBy(m => m.Priority.SortOrder).OrderBy(m => m.SortOrder).Take(9);
            return View(cars.ToList());
        }

    }
}
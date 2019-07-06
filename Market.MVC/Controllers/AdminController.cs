using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Market.Abstract.Service;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Market.Models.DTO;
using Market.MVC.Models;
using System.Drawing;
using System.IO;
using Market.MVC.Mapper;

namespace Market.MVC.Controllers
{
    /// <summary>
    /// For admin's tasks. Manage of products
    /// </summary>
    [Authorize]
    public class AdminController : BaseController
    {
        public AdminController(IMarketService ctx) : base(ctx) { }

        // GET: Admin
        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            var productsDTO = service.ProductManager.GetPaged(page, pageSize);
            var config = new MapperConfiguration(cfg =>cfg.AddProfile<TableProductProfile>());
            var mapper = config.CreateMapper();
            var model = mapper.Map<TableProductViewModel>(productsDTO);
            model.Page = page;
            return View(model);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var product = new ProductViewModel();
            var productDTO = service.ProductManager.Get(id);

            var config = new MapperConfiguration(cfg =>cfg.AddProfile<TableProductProfile>());
            var mapper = config.CreateMapper();

            product = mapper.Map<ProductViewModel>(productDTO);
            return View(product);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            var product = new ProductFormViewModel();
            product.Errors = new List<string>();

            var units = service.UnitManager.GetAll();

            product.UnitList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(units, "Id", "Title");

            return View(product);
        }



        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormViewModel model)
        {
            model.Errors = new List<string>();
            var units = service.UnitManager.GetAll();
            model.UnitList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(units, "Id", "Title");
            model.UnitTitle = units.First(x => x.Id == model.UnitId).Title;
            try
            {
                if (ModelState.IsValid)
                {
                    //Vidate model
                    model.Errors = CheckProductModel(model);
                    if (model.Errors.Any())
                        return View(model);

                    using (var binaryReader = new BinaryReader(model.ImageForm.OpenReadStream()))
                    {
                        model.Image = binaryReader.ReadBytes((int)model.ImageForm.Length);
                    }

                    //Use profile of automapper to fill DTO model
                    var config = new MapperConfiguration(cfg =>cfg.AddProfile<ProductFormProfile>());
                    var mapper = config.CreateMapper();

                    var productDTO = mapper.Map<ProductDTO>(model);
                    productDTO.Unit = mapper.Map<UnitDTO>(model);

                    service.ProductManager.Create(productDTO);
                }
                else
                {
                    foreach (var err in ModelState.Values.Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
                    {
                        model.Errors.Add(err.Errors.ToString());
                    }
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                model.Errors.Add(ex.Message);
                return View(model);
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var product = new ProductFormViewModel();
            var productDTO = service.ProductManager.Get(id);

            var config = new MapperConfiguration(cfg =>cfg.AddProfile<TableProductProfile>());
            var mapper = config.CreateMapper();

            product = mapper.Map<ProductFormViewModel>(productDTO);

            product.Errors = new List<string>();

            var units = service.UnitManager.GetAll();

            product.UnitList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(units, "Id", "Title");

            return View(product);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductFormViewModel model)
        {
            model.Errors = new List<string>();
            var units = service.UnitManager.GetAll();
            model.UnitList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(units, "Id", "Title");
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] imageData = null;
                    if (model.ImageForm != null)
                    {
                        model.Errors = CheckProductModel(model);
                        if (model.Errors.Any())
                            return View(model);

                        using (var binaryReader = new BinaryReader(model.ImageForm.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)model.ImageForm.Length);
                        }
                    }

                    if (imageData != null)
                        model.Image = imageData;

                    var config = new MapperConfiguration(cfg =>cfg.AddProfile<ProductFormProfile>());
                    var mapper = config.CreateMapper();

                    var productDTO = mapper.Map<ProductDTO>(model);
                    productDTO.Unit = mapper.Map<UnitDTO>(model);

                    service.ProductManager.Update(productDTO);
                }
                else
                {
                    foreach (var err in ModelState.Values.Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
                    {
                        model.Errors.Add(err.Errors.ToString());
                    }
                    return View(model);
                }

                //If erros is presence than return model with errors
                if (model.Errors.Any())
                {
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                model.Errors.Add(ex.Message);
                return View(model);
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var product = new ProductViewModel();
            var productDTO = service.ProductManager.Get(id);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<TableProductProfile>());
            var mapper = config.CreateMapper();

            product = mapper.Map<ProductViewModel>(productDTO);
            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                service.ProductManager.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Validate model. Check file for valid image
        /// Picture size must be lower than 500 Kb, width and height are equals 200px.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<string> CheckProductModel(ProductFormViewModel model)
        {
            List<string> errors = new List<string>();
            //Vlidate file of format
            if (model.ImageForm.ContentType.ToLower() != "image/jpeg" &&
                   model.ImageForm.ContentType.ToLower() != "image/jpg" &&
                   model.ImageForm.ContentType.ToLower() != "image/png")
            {
                errors.Add("Upload jpg or png picture");
                return errors;
            }
            int width = 0, height = 0;
            using (var image = Image.FromStream(model.ImageForm.OpenReadStream()))
            {
                width = image.Width;
                height = image.Height;
            }
            if (width != 200 & height != 200)
                errors.Add("Picture width and hegth must are equals 200px");
            if (model.ImageForm.Length > 500 * 1024)
                errors.Add("Picture size greate than 500кб");

            return errors;
        }
    }
}
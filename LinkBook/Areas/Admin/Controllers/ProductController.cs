using LinkBook.DataAccess;
using LinkBook.DataAccess.Repository.IRepository;
using LinkBook.Models;
using LinkBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkBook.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IunitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(IunitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };
        
        // Product product = new();
        // IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
        //     u => new SelectListItem
        //     {
        //         Text = u.Name,
        //         Value = u.Id.ToString()
        //     }
        // );
        //
        // IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
        //     u => new SelectListItem
        //     {
        //         Text = u.Name,
        //         Value = u.Id.ToString()
        //     }
        // );

        if (id is null or 0)
        {
            // ViewBag.CategoryList = CategoryList;
            // ViewData["CoverTypeList"] = CoverTypeList;
            return View(productVM);
        }

        else
        {
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            // var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            // // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            // if (coverTypeFromDb == null)
            // {
            //     return NotFound();
            // }
            
            return View(productVM);
        }
    }

    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                if (obj.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                obj.Product.ImageUrl = @"\images\products" + fileName + extension;
            }

            if (obj.Product.Id == 0)
            {
                _unitOfWork.Product.Add(obj.Product);
            }
            else
            {
                _unitOfWork.Product.Update(obj.Product);
            }
            
            //_unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    
        return View(obj);
    }

    //  // delete get
    //  public IActionResult Delete(int? id)
    //  {
    //      if (id is null or 0)
    //      {
    //          return NotFound();
    //      }
    //      
    //      var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
    //      // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
    //      // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
    //      if (coverTypeFromDb == null)
    //      {
    //          return NotFound();
    //      }
    //      
    //      _unitOfWork.CoverType.Remove(coverTypeFromDb);
    //      _unitOfWork.Save();
    //      return RedirectToAction("Index");
    //  }
    //
    // post
    //  [HttpPost]
    //  [ValidateAntiForgeryToken]
    //  public IActionResult Delete(int? id)
    //  {
    //      var obj = _db.Categories.Find(id);
    //      if (obj == null)
    //      {
    //          return NotFound();
    //      }
    //      _db.Categories.Remove(obj);
    //      _db.SaveChanges();
    //      return RedirectToAction("Index");
    //  }

    #region API Calls

    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
        return Json(new { data = productList });
    }
    

    #endregion
}
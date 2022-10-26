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

    public ProductController(IunitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET
    public IActionResult Index()
    {
        IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
        return View(objProductList);
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
            var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
        }

        return View(productVM);
    }

    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile file)
    {
        if (ModelState.IsValid)
        {
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
}
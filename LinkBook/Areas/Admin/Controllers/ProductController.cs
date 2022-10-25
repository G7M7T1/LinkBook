using LinkBook.DataAccess;
using LinkBook.DataAccess.Repository.IRepository;
using LinkBook.Models;
using Microsoft.AspNetCore.Mvc;

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
        Product product = new();
         
         if (id is null or 0)
         {
             return View(product);
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
         return View(product);
     }
    
     //post
     [HttpPost]
     [ValidateAntiForgeryToken]
     public IActionResult Upsert(Product obj)
     {
         if (ModelState.IsValid)
         {
             _unitOfWork.Product.Update(obj);
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
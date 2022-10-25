using LinkBook.DataAccess;
using LinkBook.DataAccess.Repository.IRepository;
using LinkBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkBook.Controllers;

public class CategoryController : Controller
{
    private readonly IunitOfWork _unitOfWork;

    public CategoryController(IunitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
    }

    //get
    public IActionResult Create()
    {
        return View();
    }
    
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "Name Cannot Match The Display Order");
        }
        
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
        // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }
    
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "Name Cannot Match The Display Order");
        }
        
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    
    // delete get
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
        // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        
        _unitOfWork.Category.Remove(categoryFromDb);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    
    //post
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Delete(int? id)
    // {
    //     var obj = _db.Categories.Find(id);
    //     if (obj == null)
    //     {
    //         return NotFound();
    //     }
    //     _db.Categories.Remove(obj);
    //     _db.SaveChanges();
    //     return RedirectToAction("Index");
    // }
}
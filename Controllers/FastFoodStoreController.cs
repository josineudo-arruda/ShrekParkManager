using Microsoft.AspNetCore.Mvc;
using AdministracaoShrekPark.Models;

namespace AdministracaoShrekPark.Controllers;

public class FastFoodStore : Controller
{
    private readonly ShrekParkManagerContext _context;

    public FastFoodStore (ShrekParkManagerContext context)
    {
        _context = context;
    }

    public IActionResult List()
    {
        return View(_context.FastFoodStores);
    }

    public IActionResult Show(int id)
    {
        FastFoodStore fastFoodStore = _context.FastFoodStores.Find(id);

        if(fastFoodStore == null)
        {
            return NotFound();
        }
        else
        {
            return View(fastFoodStore);
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] FastFoodStore fastFoodStoreViewModel)
    {
        if(!ModelState.IsValid){
            return View("Create");
        }

        if(_context.FastFoodStores.Find(fastFoodStoreViewModel.Id) == null)
        {
            FastFoodStores fastFoodStore = new FastFoodStore(fastFoodStoreViewModel.Id,fastFoodStoreViewModel.Localization,fastFoodStoreViewModel.NumberOfBooths,fastFoodStoreViewModel.Mirror,fastFoodStoreViewModel.ToiletPaper);
            _context.FastFoodStores.Add(fastFoodStore);
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
        else
        {
            return Content("Lavatório já criado no banco de dados do parque, tente outro id");
        }
    }

    public IActionResult Update(int id)
    {
        FastFoodStore fastFoodStore = _context.FastFoodStores.Find(id);

        if(fastFoodStore == null)
        {
            return Content("Lavatório não existente, crie com este id ou tente outro");
        }
        else
        {
            return View(fastFoodStore);
        }

    }

    [HttpPost]
    public IActionResult Update([FromForm] FastFoodStore fastFoodStoreViewModel)
    {
        if(!ModelState.IsValid){
            return View();
        }

        FastFoodStore fastFoodStore = _context.FastFoodStores.Find(fastFoodStoreViewModel.Id);
        
        fastFoodStore.Localization = fastFoodStoreViewModel.Localization;
        fastFoodStore.NumberOfBooths = fastFoodStoreViewModel.NumberOfBooths;
        fastFoodStore.Mirror = fastFoodStoreViewModel.Mirror;
        fastFoodStore.ToiletPaper = fastFoodStoreViewModel.ToiletPaper;
        _context.SaveChanges();
        return RedirectToAction("List");
    }

    public IActionResult Delete(int id)
    {
        _context.FastFoodStores.Remove(_context.FastFoodStores.Find(id));
        _context.SaveChanges();
        return View();
    }
}
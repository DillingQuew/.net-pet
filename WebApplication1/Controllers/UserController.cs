using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService usersService) =>
        _userService = usersService;
    
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetListAsync();
        return View(users);
    }
    
    [HttpPost]
    public async Task<IActionResult> Save(User user)
    {
        
        if (!ModelState.IsValid)
        {
            ViewBag.SelectedUser = user;
            return View("Edit");
        }
        
        if (user.Id == null)
            _userService.CreateAsync(user);
        else
            _userService.UpdateAsync(user.Id, user);
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userService.GetAsync(id);
        ViewBag.SelectedUser = user;
        return View("Edit");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await _userService.RemoveAsync(id);
        return RedirectToAction("Index");
    }

    public IActionResult AddPage()
    {
        return View("Edit");
    }

    public IActionResult Cancel()
    {
        return RedirectToAction("Index");
    }
}
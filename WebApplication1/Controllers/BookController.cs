using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService bookService) =>
        _bookService = bookService;

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAsyncList();
        return View(books);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var book = await _bookService.GetAsync(id);
        ViewBag.EditedBook = book;
        return View("Edit");
    }

    public async Task<IActionResult> Save(Book book)
    {
        if (book.Id != null)
        {
            await _bookService.UpdateAsync(book.Id, book);
        }
        else
        {
            await _bookService.CreateAsync(book);
        }
        
        return RedirectToAction("Index");
    }

    public IActionResult AddPage()
    {
        return View("Edit");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _bookService.RemoveAsync(id);
        return RedirectToAction("Index");
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookWorld.data;
using MyBookWorld.Models;

namespace MyBookWorld.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDBcontext _db;

    public HomeController(ApplicationDBcontext db)
    {
        _db = db;
    }

    // picks one randome Qoute from database
    [HttpGet]
    public IActionResult Index()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 101);
        try
        {
            var theQuote = _db.Quotes.Find(randomNumber);
            return View(theQuote);
        }
        catch
        {
            ViewData["Welcome"] = "Welcome";
            return View();
        }
    }

    // loads your books
    [HttpGet]
    public IActionResult Library()
    {
        try
        {
            List<Book> books = _db.Books.Include(b => b.Author).Include(b => b.Category).ToList<Book>();
            return View(books);
        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }

    }

    // search for all books in library
    [HttpPost]
    public IActionResult Library(string searchString)
    {
        if (searchString != null)
        {
            List<Book> matchedBooks = _db.Books.Include(b => b.Author).Include(b => b.Category).Where(b => b.Title.Contains(searchString)).ToList<Book>();
            if (!(matchedBooks.Count > 0))
            {
                return View();
            }
            else
            {
                return View(matchedBooks);
            }
        }
        return View("Library");
    }

    // loads details of chosen book
    [HttpGet]
    public IActionResult Details(int id)
    {   
        Book? book = _db.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
            return NotFound();

        var relatedBooks = _db.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Where(b => b.CategoryId == book.CategoryId && b.Id != book.Id)
            .ToList();

        ViewData["relatedBooks"] = relatedBooks;
        return View(book);
    }

    // loads book content
    [HttpGet]
    public IActionResult Read(int id)
    {
        var book = _db.Books.Include(b => b.Author).Include(b => b.Category).FirstOrDefault(b => b.Id == id);

        return View(book);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookWorld.data;
using MyBookWorld.Models;

namespace MyBookWorld.Controllers;
public class ManageController : Controller
{
    private readonly ApplicationDBcontext _db;
    public ManageController(ApplicationDBcontext db)
    {
        _db = db;
    }

    // loads books
    [HttpGet]
    public IActionResult Index()
    {
        List<Book> books = _db.Books.Include(b => b.Author).ToList<Book>();
        return View(books);
    }

    // search for books to manage
    [HttpPost]
    public IActionResult Search(string searchString)
    {
        if (searchString != null)
        {
            try
            {
                List<Book> matchedBooks = _db.Books.Include(b => b.Author).Include(b => b.Category).Where(b => b.Title.Contains(searchString)).ToList<Book>();
                if (!(matchedBooks.Count > 0) || matchedBooks == null)
                {
                    throw new Exception("Not Found.");
                }
                else
                {
                    return View("Index", matchedBooks);
                }

            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("Index");
            }
        }
        return View("Index");
    }

    // loads add book form
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // adds book to database
    [HttpPost]
    public IActionResult Add(string Title, string Description, string Summery, string CoverImageUrl, DateTime PublishedDate, IFormFile file)
    {
        if (string.IsNullOrEmpty(Title))
        {
            ModelState.AddModelError("Title", "Title is required.");
        }
        else if (Title.Length > 200)
        {
            ModelState.AddModelError("Title", "Title should not exceed 200 characters.");
        }

        if (string.IsNullOrEmpty(Description))
        {
            ModelState.AddModelError("Description", "Description is required.");
        }

        if (string.IsNullOrEmpty(Summery))
        {
            ModelState.AddModelError("Summery", "Summery is required.");
        }

        if (string.IsNullOrEmpty(CoverImageUrl) || !Uri.IsWellFormedUriString(CoverImageUrl, UriKind.Absolute))
        {
            ModelState.AddModelError("CoverImageUrl", "Valid Cover URL is required.");
        }

        if (PublishedDate == default(DateTime))
        {
            ModelState.AddModelError("PublishedDate", "Published Date is required.");
        }
        else if (PublishedDate > DateTime.Now)
        {
            ModelState.AddModelError("PublishedDate", "Published Date cannot be in the future.");
        }

        if (file == null)
        {
            ModelState.AddModelError("File", "File is required.");
        }
        else if (file.Length == 0)
        {
            ModelState.AddModelError("File", "File cannot be empty.");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        if (file != null && file.Length > 0)
        {

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            byte[] fileContent = memoryStream.ToArray();

            Book newBook = new()
            {
                Title = Title,
                Description = Description,
                Summary = Summery,
                CoverImageUrl = CoverImageUrl,
                PublishedDate = PublishedDate,
                Content = fileContent,
                AuthorId = 1,
                CategoryId = 1,
            };

            try
            {
                _db.Books.Add(newBook);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving data: " + ex.Message);
                return View();
            }

        }

        return RedirectToAction("Index");
    }

    // delete book from database
    [HttpPost]
    public IActionResult Delete(int? id)
    {
        var book = _db.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }
        try
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error Deleting Book: " + ex.Message);
            return View("Index");
        }
        return RedirectToAction("Index");
    }

    // loads edit form 
    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error Finding Book: " + ex.Message);
            return View("Index");
        }
    }

    // edits book
    [HttpPost]
    public IActionResult Edit(int id, string Title, string Description, string Summary, string CoverImageUrl, DateTime PublishedDate, IFormFile file)
    {

        Book currentbook = _db.Books.Find(id);
        byte[]? fileContent = [];

        if (string.IsNullOrEmpty(Title))
        {
            ModelState.AddModelError("Title", "Title is required.");
        }
        else if (Title.Length > 200)
        {
            ModelState.AddModelError("Title", "Title should not exceed 200 characters.");
        }

        if (string.IsNullOrEmpty(Description))
        {
            ModelState.AddModelError("Description", "Description is required.");
        }
        if (string.IsNullOrEmpty(Summary))
        {
            ModelState.AddModelError("Summary", "Description is required.");
        }

        if (string.IsNullOrEmpty(CoverImageUrl) || !Uri.IsWellFormedUriString(CoverImageUrl, UriKind.Absolute))
        {
            ModelState.AddModelError("CoverImageUrl", "Valid Cover URL is required.");
        }

        if (PublishedDate == default(DateTime))
        {
            ModelState.AddModelError("PublishedDate", "Published Date is required.");
        }
        else if (PublishedDate > DateTime.Now)
        {
            ModelState.AddModelError("PublishedDate", "Published Date cannot be in the future.");
        }
        // Handle File Upload (if file is provided)
        if (file != null && file.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            file?.CopyTo(memoryStream);
            fileContent = memoryStream.ToArray();
        }
        else
        {
            // Assign new file content
            fileContent = currentbook!.Content;
        }
        currentbook!.Title = Title;
        currentbook.Description = Description;
        currentbook.PublishedDate = PublishedDate;
        currentbook.CoverImageUrl = CoverImageUrl;
        currentbook.Content = fileContent;
        currentbook.Summary = Summary;
        currentbook.AuthorId = 1;
        try
        {
            _db.Books.Update(currentbook);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error saving data: " + ex.Message);
            return View();
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
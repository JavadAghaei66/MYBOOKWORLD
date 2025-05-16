using Microsoft.EntityFrameworkCore;
using MyBookWorld.data;
using MyBookWorld.Models;

namespace Online_Library.utils;

public class LibraryService
{
    
    static public string filePath = Path.Combine(Environment.CurrentDirectory, "contentFile.txt");
    static public Book CreateBook(string Title, string Description, string CoverImageUrl, byte[]? ContentFile, int AuthorId = 1)
    {
        Book book = new()
        {
            Title = Title,
            Description = Description,
            PublishedDate = DateTime.Now,
            CoverImageUrl = CoverImageUrl,
            AuthorId = AuthorId,
            Content = ContentFile,
        };
        return book;
    }
    // Makes it cound primary key from zero
    static public void IdFromZero(ApplicationDBcontext db)
    {
        db.Database.ExecuteSqlRaw("ALTER TABLE Books AUTO_INCREMENT = 1");
        db.SaveChanges();
    }
    // Remove all records of table
    static public void RemoveRecords(ApplicationDBcontext db)
    {
        List<Book> books = db.Books.ToList();
        db.RemoveRange(books);
        db.SaveChanges();
    }
    // fill content 
    static public void FillContent(ApplicationDBcontext db)
    {
        string textContent;
        using (var reader = new StreamReader(filePath))
        {
            textContent = reader.ReadToEnd().Trim();
        }
        Console.WriteLine(textContent);

        byte[] contentFile = System.Text.Encoding.UTF8.GetBytes(textContent.Trim());
        // Console.WriteLine(cameFromByte);

        // var currentFileContent = db.Books.Find(1);
        // string cameFromDatabase = System.Text.Encoding.UTF8.GetString(currentFileContent!.Content!);
        // Console.WriteLine(cameFromDatabase);
        
        for (int i = 1; i <= 12; i++)
        {
            Console.WriteLine($"round {i} ...");
            var currentBook = db.Books.Find(i);
            currentBook!.Content = contentFile;
            db.Books.Update(currentBook);
            db.SaveChanges();
        }
    }
}
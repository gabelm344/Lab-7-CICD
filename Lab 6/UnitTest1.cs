using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_5;
using Lab_5.Models;
[TestClass]
public class LibraryServiceTests
{
    private LibraryService _libraryService;

    [TestInitialize]
    public void Setup()
    {
        _libraryService = new LibraryService();
    }

    [TestMethod]
    public void TestAddBook()
    {

        var book = new Book { Title = "Test Book", Author = "Author", ISBN = "12345" };

        _libraryService.AddBook(book);

        var books = _libraryService.GetBooks();
        Assert.AreEqual(1, books.Count, "Book count should be 1.");
        Assert.AreEqual("Test Book", books[0].Title, "Book title should match.");
    }

    [TestMethod]
    public void TestDeleteBook()
    {
        var book = new Book { Title = "Test Book", Author = "Author", ISBN = "12345" };
        _libraryService.AddBook(book);

        _libraryService.DeleteBook(1);

        var books = _libraryService.GetBooks();
        Assert.AreEqual(0, books.Count, "Book count should be 0 after deletion.");
    }

    [TestMethod]
    public void TestEditBook()
    {
        var book = new Book { Title = "Test Book", Author = "Author", ISBN = "12345" };
        _libraryService.AddBook(book);
        var updatedBook = new Book { Id = 1, Title = "Updated Title", Author = "New Author", ISBN = "67890" };

        _libraryService.EditBook(updatedBook);

        var books = _libraryService.GetBooks();
        Assert.AreEqual(1, books.Count, "There should still be 1 book.");
        Assert.AreEqual("Updated Title", books[0].Title, "Book title should be updated.");
        Assert.AreEqual("New Author", books[0].Author, "Book author should be updated.");
    }
    [TestMethod]
    public void TestReadUsers()
    {
        var users = new List<User>
    {
        new User { Id = 1, Name = "User 1", Email = "user1@example.com" },
        new User { Id = 2, Name = "User 2", Email = "user2@example.com" }
    };

        _libraryService.GetUsers().AddRange(users);

        var result = _libraryService.GetUsers();

        Assert.AreEqual(2, result.Count, "User list should contain 2 users.");
        Assert.AreEqual("User 1", result[0].Name, "The first user name should match.");
    }
    [TestMethod]
    public void TestAddUser()
    {

        var user = new User { Name = "New User", Email = "newuser@example.com" };

        _libraryService.AddUser(user);

        var result = _libraryService.GetUsers();
        Assert.AreEqual(1, result.Count, "User list should contain 1 user.");
        Assert.AreEqual("New User", result[0].Name, "The user name should match.");
        Assert.AreEqual(1, result[0].Id, "The user ID should be auto-assigned as 1.");
    }
    [TestMethod]
    public void EditUser_ShouldUpdateUserDetails()
    {

        var user = new User { Id = 1, Name = "Original User", Email = "original@example.com" };
        _libraryService.AddUser(user);
        var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };

        _libraryService.EditUser(updatedUser);

        var result = _libraryService.GetUsers();
        Assert.AreEqual("Updated User", result[0].Name, "The user name should be updated.");
        Assert.AreEqual("updated@example.com", result[0].Email, "The user email should be updated.");
    }
    [TestMethod]
    public void TestDeleteUser()
    {
        var user1 = new User { Id = 1, Name = "User 1", Email = "user1@example.com" };
        var user2 = new User { Id = 2, Name = "User 2", Email = "user2@example.com" };
        _libraryService.AddUser(user1);
        _libraryService.AddUser(user2);

        _libraryService.DeleteUser(1); // Delete the first user

        var result = _libraryService.GetUsers();
        Assert.AreEqual(1, result.Count, "User list should contain 1 user after deletion.");
        Assert.AreEqual("User 2", result[0].Name, "The remaining user should be User 2.");
    }

}

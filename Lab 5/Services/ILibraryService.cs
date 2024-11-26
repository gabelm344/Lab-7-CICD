using System.Collections.Generic;
using Lab_5.Models;
public interface ILibraryService
{
    // Book Management
    List<Book> GetBooks();
    void AddBook(Book book);
    void DeleteBook(int bookId);
    void EditBook(Book updatedBook);

    // User Management
    List<User> GetUsers();
    void AddUser(User user);
    void DeleteUser(int userId);
    void EditUser(User updatedUser);

    // Borrowing and Returning Books
    void BorrowBook(User user, Book book);
    void ReturnBook(User user, Book book);

    // Retrieve Borrowed Books by User
    Dictionary<User, List<Book>> GetBorrowedBooks();
}


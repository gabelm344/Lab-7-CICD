using System.Collections.Generic;
using System.Linq;
using Lab_5.Models;

public class LibraryService : ILibraryService
{
        private List<Book> Books { get; set; } = new List<Book>();
        private List<User> Users { get; set; } = new List<User>();
        private Dictionary<User, List<Book>> BorrowedBooks { get; set; } = new Dictionary<User, List<Book>>();

    public LibraryService()
    {
        ReadBooks();
        ReadUsers();
    }

    // Load books from Books.csv
    private void ReadBooks()
    {
        try
        {
            var lines = File.ReadLines("./Data/Books.csv");
            foreach (var line in lines)
            {
                var fields = line.Split(',');
                if (fields.Length >= 4)
                {
                    var book = new Book
                    {
                        Id = int.Parse(fields[0].Trim()),
                        Title = fields[1].Trim(),
                        Author = fields[2].Trim(),
                        ISBN = fields[3].Trim()
                    };
                    Books.Add(book);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading Books.csv: {ex.Message}");
        }
    }

    // Load users from Users.csv
    private void ReadUsers()
    {
        try
        {
            var lines = File.ReadLines("./Data/Users.csv");
            foreach (var line in lines)
            {
                var fields = line.Split(',');
                if (fields.Length >= 3)
                {
                    var user = new User
                    {
                        Id = int.Parse(fields[0].Trim()),
                        Name = fields[1].Trim(),
                        Email = fields[2].Trim()
                    };
                    Users.Add(user);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading Users.csv: {ex.Message}");
        }
    }

// Method to get all books
        public List<Book> GetBooks()
        {
            return Books.ToList();
        }

        public void AddBook(Book book)
        {
            book.Id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            var bookToDelete = Books.FirstOrDefault(b => b.Id == bookId);
            if (bookToDelete != null)
            {
                Books.Remove(bookToDelete);
            }
        }

        public void EditBook(Book updatedBook)
        {
            var existingBook = Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (existingBook != null)
            {
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
                existingBook.ISBN = updatedBook.ISBN;
            }
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public void AddUser(User user)
        {
            user.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
            Users.Add(user);
        }

        // Method to delete a user by Id and remove their borrowed books if they have any
        public void DeleteUser(int userId)
        {
            var userToDelete = Users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                Users.Remove(userToDelete);
                if (BorrowedBooks.ContainsKey(userToDelete))
                {
                    BorrowedBooks.Remove(userToDelete);
                }
            }
        }
    public void EditUser(User updatedUser)
    {
        var existingUser = Users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (existingUser != null)
        {
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
        }
    }

    // Method to borrow a book by associating it with a user and removing it from available books
    public void BorrowBook(User user, Book book)
        {
            if (!BorrowedBooks.ContainsKey(user))
            {
                BorrowedBooks[user] = new List<Book>();
            }
            BorrowedBooks[user].Add(book);
            Books.Remove(book);
        }

        // Method to return a borrowed book, making it available again
        public void ReturnBook(User user, Book book)
        {
            if (BorrowedBooks.ContainsKey(user) && BorrowedBooks[user].Contains(book))
            {
                BorrowedBooks[user].Remove(book);
                Books.Add(book);
            }
        }

        // Method to get the dictionary of borrowed books by user
        public Dictionary<User, List<Book>> GetBorrowedBooks()
        {
            return BorrowedBooks;
        }
    
}

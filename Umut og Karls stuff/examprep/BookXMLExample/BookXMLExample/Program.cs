using System.Text;
using System.Xml;

namespace BookXMLExample {

  public class Program {

    public static void Main() {
      List<Book> books = ReadBooks("data/books.xml");

      foreach (Book book in books)
        Console.WriteLine(book);

      WriteBooks("../../../data/books2.xml", books);
    }

    private static List<Book> ReadBooks(string path) {
      List<Book> books = new();

      XmlReaderSettings settings = new() {
        IgnoreWhitespace = true,
        IgnoreComments = true
      };

      using XmlReader reader = XmlReader.Create(path, settings);

      reader.MoveToContent();

      reader.ReadStartElement("books");

      while (reader.IsStartElement("book")) {
        Book book = new();

        book.Language = reader.GetAttribute("language");
        book.Pages = Int32.Parse(reader.GetAttribute("pages"));

        reader.ReadStartElement();

        while (reader.IsStartElement())
          switch (reader.Name) {
            case "title":
              book.Title = reader.ReadElementContentAsString();
              break;
            case "subtitle":
              book.Subtitle = reader.ReadElementContentAsString();
              break;
            case "author":
              book.Authors.Add(reader.ReadElementContentAsString());
              break;
            case "publisher":
              book.Publisher = reader.ReadElementContentAsString();
              break;
            case "year":
              book.Year = reader.ReadElementContentAsInt();
              break;
          }

        reader.ReadEndElement(); // </book>

        books.Add(book);
      }

      reader.ReadEndElement(); // </books>

      return books;
    }

    private static void WriteBooks(string path, List<Book> books) {
      XmlWriterSettings settings = new() {
        Indent = true,
        IndentChars = "  ",
        Encoding = Encoding.UTF8
      };

      using XmlWriter writer = XmlWriter.Create(path, settings);

      writer.WriteStartDocument();
      {
        writer.WriteStartElement("books");
        {
          writer.WriteComment("Følgende er tre bøger fra reolen");

          foreach (Book book in books)
            WriteBook(writer, book);
        }
        writer.WriteEndElement(); // </books>
      }
      writer.WriteEndDocument();
    }

    private static void WriteBook(XmlWriter writer, Book book) {
      writer.WriteStartElement("book");
      {
        WriteAttributeString(writer, "language", book.Language);
        WriteAttributeString(writer, "pages", book.Pages.ToString()); //*

        WriteElementString(writer, "title", book.Title);

        WriteElementString(writer, "subtitle", book.Subtitle);

        foreach (string author in book.Authors)
          WriteElementString(writer, "author", author); //*

        WriteElementString(writer, "publisher", book.Publisher);
        WriteElementString(writer, "year", book.Year.ToString()); //*
      }
      writer.WriteEndElement(); // </book>
    }

    //* overflødigt at kalde service-metoden (gøres dog pga. ensartethed).

    private static void WriteAttributeString(XmlWriter writer, string name, string value) {
      if (value != null)
        writer.WriteAttributeString(name, value);
    }

    private static void WriteElementString(XmlWriter writer, string name, string value) {
      if (value != null)
        writer.WriteElementString(name, value);
    }
  }
}

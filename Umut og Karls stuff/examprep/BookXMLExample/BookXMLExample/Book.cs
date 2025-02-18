
namespace BookXMLExample {

  public class Book {
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Publisher { get; set; }
    public string Language { get; set; }
    public int Pages { get; set; }
    public int Year { get; set; }
    public List<string> Authors { get; set; }

    public Book() {
      Authors = new();
    }

    public Book(string title, string subtitle, string publisher, string language, int pages, int year, List<string> authors) {
      Title = title;
      Subtitle = subtitle;
      Publisher = publisher;
      Language = language;
      Pages = pages;
      Year = year;
      Authors = authors;
    }

    public override string ToString() {
      return "[Book: title=" + Title +
        ", subtitle=" + Subtitle +
        ", publisher=" + Publisher +
        ", language=" + Language +
        ", pages=" + Pages +
        ", year=" + Year +
        ", authors=" + string.Join(", ", Authors) + "]";
    }
  }
}

namespace DatingApp.API.Helpers
{
  public class PaginationHeader
  {

    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
      this.TotalPages = totalPages;
      this.TotalItems = totalItems;
      this.ItemsPerPage = itemsPerPage;
      this.CurrentPage = currentPage;
    }

    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
  }
}

namespace TaskHelperWebApp.ViewModels;

public class CreateBoardsViewModel
{
    public Guid ID { get; set; }
    public Guid ProjectID { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Color { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}

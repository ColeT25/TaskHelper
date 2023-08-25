namespace TaskHelperWebApp.ViewModels;

public class TasksViewModel
{
    public Guid ID { get; set; }
    public Guid BoardID { get; set; }
    public Guid UserID { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid? IsSubtaskOf { get; set; } 
    public bool IsComplete { get; set; } = false;
    public DateTime CreatedDate { get; set; }
}

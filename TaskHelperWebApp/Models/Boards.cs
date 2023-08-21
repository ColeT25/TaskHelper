namespace TaskHelperWebApp;

public class Boards
{
    public Guid ID { get; set; }
    public Guid projectID { get; set; }
    public string name { get; set; } = default!;
    public string description { get; set; } = default!;
    public string color { get; set; } = default!;
}

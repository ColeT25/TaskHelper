using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TaskHelperWebApp.ViewModels;
public class BoardsViewModel
{
    public Guid? ID { get; set; } = null;
    public Guid? ProjectID { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? Color { get; set; } = null;

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? CreatedDate { get; set; } = null;

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? ClosedDate { get; set; } = null;
    public string? ProjectName { get; set; } = String.Empty;
    public List<SelectListItem>? PotentialProjects { get; set; } = null;

    public bool ModelIsComplete
    {
        get
        {
            foreach (PropertyInfo boardProp in this.GetType().GetProperties())
            {
                if (boardProp.GetValue(this) == null)
                    return false;
            }
            return true;
        }
    }
}

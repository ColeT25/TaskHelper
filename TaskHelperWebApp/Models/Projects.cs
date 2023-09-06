using System;
using System.ComponentModel.DataAnnotations;

namespace TaskHelperWebApp.Models;

public class Projects
{
    public Guid ID { get; set; } 
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    [Display(Name = "Created Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Closed Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? ClosedDate { get; set; }= null;

    #region Overrides
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is Projects)
        {
            Projects? that = obj as Projects;
            if (that is null)
                return false;

            bool areEqual = this.Name == that.Name && this.ID == that.ID
                && this.Description == that.Description && this.CreatedDate == that.CreatedDate
                && this.ClosedDate == that.ClosedDate;
            return areEqual;
        }

        return false;
    }
    #endregion
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MenuItem <P,C>
{
    public int MenuItemId { get; set; }
    [StringLength(50)]
    public string MenuText { get; set; }
    [StringLength(255)]
    public string LinkUrl { get; set; }
    public int? MenuOrder { get; set; }
    public int? ParentMenuItemId { get; set; }
    public virtual P Parent { get; set; }
    public virtual ICollection<C> Children { get; set; }
    public int MenuId { get; set; }
    public virtual Menu Menu { get; set; }


}
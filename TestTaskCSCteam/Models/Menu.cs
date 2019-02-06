using System.Collections.Generic;

public class Menu
{
    public int MenuId { get; set; }
    public string MenuName { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; }
}
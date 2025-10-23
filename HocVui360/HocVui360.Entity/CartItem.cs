namespace HocVui360.Entity;

public class CartItem : Entity<string>
{
    public string CartId { get; set; }
    public Cart Cart { get; set; }
}

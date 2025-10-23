namespace HocVui360.Entity;

public class Cart : Entity<string>
{
    public ICollection<CartItem> CartItems { get; set; }
}

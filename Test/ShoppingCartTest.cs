using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ShoppingCartTests
{
    [TestInitialize]
    [TestMethod]
    public void AddToShoppingCart_ShouldIncreaseItemCount()
    {
        // Arrange
        ShoppingCart cart = new ShoppingCart();
        Snack snack = new Snack("Test Snack", 2.99); // Assuming you have a Snack class

        // Act
        cart.AddtoShoppingCart(snack);

        // Assert
        Assert.AreEqual(1, cart.shoppingcart.Count);
        Assert.AreEqual(snack, cart.shoppingcart[0]);
    }

    [TestMethod]
    public void ShoppingCartCosts_ShouldCalculateTotalCost()
    {
        // Arrange
        ShoppingCart cart = new ShoppingCart();
        Snack snack1 = new Snack("Snack 1", 2.99);
        Snack snack2 = new Snack("Snack 2", 1.99);

        // Act
        cart.AddtoShoppingCart(snack1);
        cart.AddtoShoppingCart(snack2);

        // Assert
        Assert.AreEqual(4.98, cart.ShoppingCartCosts());
    }

    // [TestMethod]
    // public void ModifyShoppingCart_ShouldRemoveSelectedItems()
    // {
    //     // Arrange
    //     ShoppingCart cart = new ShoppingCart();
    //     Snack snack1 = new Snack("Snack 1", 2.99);
    //     Snack snack2 = new Snack("Snack 2", 1.99);

    //     cart.AddtoShoppingCart(snack1);
    //     cart.AddtoShoppingCart(snack2);

    //     // Act
    //     cart.ModifyShoppingCart(); // Assuming the method modifies the cart by removing items

    //     // Assert
    //     Assert.AreEqual(0, cart.shoppingcart.Count); // Assuming all items were removed
    // }
}

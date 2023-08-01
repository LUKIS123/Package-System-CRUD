using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Utility
{
    public class UserPageUtils
    {
        public static View RenderOrderCellViews(Order order)
        {
            var view = new VerticalStackLayout
            {
                new Label { Text = $"OrderId={order.Id} , Status={order.Status}" },
                new Button()
                {
                    Text = "Test Btn"
                }
            };

            return view;
        }
    }
}
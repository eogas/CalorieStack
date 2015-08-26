namespace CalorieStack.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public string Name { get; set; }
        public int Calories { get; set; }
    }
}
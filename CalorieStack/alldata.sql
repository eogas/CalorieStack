
select *
  from Stacks
  join Days
    on days.StackId = stacks.Id
  join Meals
    on meals.Day_Id = Days.id
  join FoodItems
    on FoodItems.Meal_Id = meals.Id

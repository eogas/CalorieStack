use CalorieStack;

select *
  from Stacks
  join Days
    on days.StackId = stacks.Id
  join Meals
    on meals.DayId = Days.id
  join FoodItems
    on FoodItems.MealId = meals.Id


/* drop everything */
/* DANGEROUS! Don't uncommment unless you want to delete everything! */
--delete from FoodItems
--delete from Meals
--delete from Days
--delete from Stacks

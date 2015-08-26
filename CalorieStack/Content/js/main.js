
ngApp.controller('FoodCtrl', function ($scope, $resource) {

    var Stack = $resource('./api/stacks/:id', { id: '@id' }, {
        update: { method: 'PUT' }
    });

    var StackDay = $resource('./api/days/:stackId/:year/:month/:day', {
        stackId: '@stackId',
        year: '@year',
        month: '@month',
        day: '@day'
    });

    var Meal = $resource('./api/meals/:id', { id: '@id' });

    var FoodItem = $resource('./api/fooditems/:id', { id: '@id' }, {
        update: { method: 'PUT' }
    });

    $scope.MealStateEnum = {
        View: 0, Add: 1
    };

    $scope.ItemStateEnum = {
        View: 0, Edit: 1
    };

    // Fetches current StackDay
    var dataFetch = function () {
        StackDay.get({
            stackId: $scope.stackId,
            year: $scope.currentDate.year(),
            month: $scope.currentDate.month() + 1,
            day: $scope.currentDate.date()
        }, function (data) {
            dataMap(data);
        }, function () {
            // Specified StackDay doesn't exist yet, let's create it
            var newDay = new StackDay({
                stackId: $scope.stackId,
                date: $scope.currentDate.startOf('day')
            });

            // Save new StackDay and map data
            newDay.$save(function (data) {
                dataMap(data);
            });
        });
    };

    // Maps retrieved data to $scope
    var dataMap = function (data) {
        var meals = data.meals.map(function (meal) {
            meal.items = meal.items.map(function (item) {
                return new FoodItem($.extend(item, {
                    state: $scope.ItemStateEnum.View
                }));
            });

            return $.extend(meal, {
                state: $scope.MealStateEnum.View
            });
        });

        $scope.meals = meals;
    };

    $scope.init = function(stackId) {
        $scope.stackId = stackId;
        $scope.currentDate = moment();

        $scope.stack = Stack.get({
            id: stackId
        });

        // initial fetch of stackDay data
        dataFetch();

        $('#datepicker').datetimepicker({
            pickTime: false,
            maxDate: moment(),
            defaultDate: $scope.currentDate
        }).on('dp.change', function (e) {
            // Change current date and refetch all data
            $scope.currentDate = e.date;
            dataFetch();
        });
    };

    $scope.original = {};

    $scope.dismissReminder = function () {
        $scope.stack.isReminderDismissed = true;
        $scope.stack.$update();
    };

    $scope.itemSelect = function(item) {
        // cancel edit of other items
        $scope.meals.forEach(function(meal) {
            meal.items.forEach(function(foodItem) {
                if (foodItem !== item && foodItem.state == $scope.ItemStateEnum.Edit) {
                    $scope.cancelEdit(foodItem);
                }
            });
        });

        angular.copy(item, $scope.original);
        item.state = $scope.ItemStateEnum.Edit;
    };

    $scope.cancelEdit = function(item, event) {
        angular.copy($scope.original, item);
        item.state = $scope.ItemStateEnum.View;

        // This stops the click from passing through to the list item and
        // immediately triggering another edit
        if (event) {
            event.stopPropagation();
            event.preventDefault();
        }
    };

    $scope.saveEdit = function(item, event) {
        item.$update(function() {
            item.state = $scope.ItemStateEnum.View;
        });

        // This stops the click from passing through to the list item and
        // immediately triggering another edit
        if (event) {
            event.stopPropagation();
            event.preventDefault();
        }
    };

    $scope.deleteItem = function(item, meal, index) {
        item.$remove(function() {
            meal.items.splice(index, 1);
        });
    };

    $scope.addItem = function(meal) {
        meal.state = $scope.MealStateEnum.Add;
    };

    $scope.saveAdd = function(meal, item) {
        item.state = $scope.ItemStateEnum.View;
        meal.state = $scope.MealStateEnum.View;

        var newItem = new FoodItem(item);
        newItem.mealId = meal.id;
        newItem.$save(function(cbItem) {
            $.extend(cbItem, {
                state: $scope.ItemStateEnum.View
            });
            meal.items.push(cbItem);
        });
    };

    $scope.cancelAdd = function(meal, item) {
        item = {};
        meal.state = $scope.MealStateEnum.View;
    };

    $scope.mealCals = function(meal) {
        var cals = 0;

        meal.items.forEach(function(item) {
            cals += item.calories;
        });

        if ($.isNumeric(cals)) {
            meal.totalCals = cals;
        }

        return meal.totalCals;
    };

    $scope.dayCals = function() {
        var cals = 0;

        if (!$scope.meals) {
            return 0;
        }

        $scope.meals.forEach(function(meal) {
            cals += meal.totalCals;
        });

        return cals;
    };

    // watch for edit form creation so we can set focus
    $scope.$watch(function() {
        return $('[name="editForm"]')[0];
    }, function(newVal, oldVal) {
        newVal && $(newVal).find('input')[0].focus();
    });

    // watch for add form creation so we can set focus
    // TODO: may need to limit to one add form at a time
    $scope.$watch(function() {
        return $('[name="addForm"]')[0];
    }, function(newVal, oldVal) {
        newVal && $(newVal).find('input')[0].focus();
    });
});

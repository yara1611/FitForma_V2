using System;
using Google.OrTools.LinearSolver;

public class MealGeneratorService
{

    //Integrate the solver -> generate plan
    //load the dataset of foods and their nutritional info (calories, protein, fats, carbs)
    // get nutritopn target from user (calories, protein, fats, carbs)

    public void Generate()
    {
        //Declare the solver
        Solver solver = Solver.CreateSolver("SCIP"); //why SCIP? bec. it supporys integer variables which we need for 0/1 selection of foods.
        //why not GoLP? because it only supports continuous variables
        if (solver is null)
        {
            return;
        }
        //simple trial
        // Sample foods (name, calories. protein, fat, carbs)
        var foods = new List<(string Name, double Calories, double Protein,double Fat, double Carbs)>
        {
            ("Chicken Breast", 250,30,5,0),
            ("Rice", 200,4,1,45),
            ("Broccoli", 50,3,0,10),
            ("Eggs", 150,12,10,1),
            ("Oats", 300,10,5,55),
        };

        double targetCalories = 500;
        double targetProtein = 40;
        double targetCarbs = 50;
        double targetFats = 10;

        //create the variables (0/1 for each food)
        var vars = foods.Select(f => solver.MakeIntVar(0, 1, f.Name)).ToList();
        Console.WriteLine("Number of variables = " + solver.NumVariables());

        //define expressions
        LinearExpr totalCalories = new LinearExpr();
        LinearExpr totalProtiens = new LinearExpr();
        LinearExpr totalFats = new LinearExpr();
        LinearExpr totalCarbs = new LinearExpr();
        for(int i =0;i<foods.Count;i++){
            totalCalories += vars[i] * foods[i].Calories; //sum of (0/1 * calories)
            totalProtiens += vars[i] * foods[i].Protein; //sum of (0/1 * calories)
            totalFats += vars[i] * foods[i].Fat; //sum of (0/1 * calories)
            totalCarbs += vars[i] * foods[i].Carbs; //sum of (0/1 * calories)
        }

        //Define the constraints (cals -/+ 10%)
        // Constraint 1: total calories must be >= 90% of target
        solver.Add(totalCalories>=0.9*targetCalories);
        // Constraint 2: total calories must be <= 110% of target
        solver.Add(totalCalories<=1.1*targetCalories);
        // Constraint 1: total calories must be >= 90% of target
        solver.Add(totalProtiens>=0.9*targetProtein);
        // Constraint 2: total calories must be <= 110% of target
        solver.Add(totalProtiens<=1.1*targetProtein);
        // Constraint 1: total calories must be >= 90% of target
        solver.Add(totalCarbs>=0.9*targetCarbs);
        // Constraint 2: total calories must be <= 110% of target
        solver.Add(totalCarbs<=1.1*targetCarbs);
        // Constraint 1: total calories must be >= 90% of target
        solver.Add(totalFats>=0.9*targetFats);
        // Constraint 2: total calories must be <= 110% of target
        solver.Add(totalFats<=1.1*targetFats);

        // Define the objective function: // Maximize number of selected foods
        LinearExpr totalSelected = new LinearExpr();
        for (int i = 0; i < vars.Count; i++)
            totalSelected += vars[i]; //totalSelected = x[0] + x[1] + x[2] + x[3] + x[4]
        
        solver.Maximize(totalSelected); //picks the mx totalSelected number

        // Solve the system. (invoke the solver)
        Solver.ResultStatus resultStatus = solver.Solve();
        // Check that the problem has an optimal solution.
        if (resultStatus != Solver.ResultStatus.OPTIMAL)
        {
            Console.WriteLine("The problem does not have an optimal solution!");
            return;
        }
        Console.WriteLine("Solution:");
        Console.WriteLine("Objective value = " + solver.Objective().Value()); //what is the objective value? it is the totalSelected value which is the number of foods selected. we want to maximize it while meeting the constraints.
        Console.WriteLine("Selected foods:");
        for (int i = 0; i < foods.Count; i++)
        {
            if (vars[i].SolutionValue() == 1)
                Console.WriteLine($"- {foods[i].Name} ({foods[i].Calories} cal) - Protein: {foods[i].Protein}g - Fats: {foods[i].Fat}g - Carbs: {foods[i].Carbs}g");
        }
        Console.WriteLine("Total calories = " +
            foods.Where((f, i) => vars[i].SolutionValue() == 1).Sum(f => f.Calories));
        Console.WriteLine("Total Protein = " +
            foods.Where((f, i) => vars[i].SolutionValue() == 1).Sum(f => f.Protein));
        Console.WriteLine("Total Fats = " +
            foods.Where((f, i) => vars[i].SolutionValue() == 1).Sum(f => f.Fat));
        Console.WriteLine("Total Carbs = " +
            foods.Where((f, i) => vars[i].SolutionValue() == 1).Sum(f => f.Carbs));
    }

}
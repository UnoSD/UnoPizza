module UnoCash.Fulma.Pages

open UnoCash.Fulma.Models
open UnoCash.Fulma.ExpenseForm
open UnoCash.Fulma.About
open UnoCash.Fulma.Leavening

let page model =
    model |>
    match model.CurrentTab with
    | Recipe    -> recipeCard
    | Leavening -> leaveningTimesCard
    | About     -> aboutCard
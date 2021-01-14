module UnoCash.Fulma.Models

open System

type Tab =
    | Recipe
    | Leavening
    | About

type Model =
    {
        CurrentTab : Tab
        StartTime : DateTime
        EndTime : DateTime
        InitialRT : decimal
        CT : decimal
        FinalRT : decimal
        ReviewHoverValue : int
        ReviewValue : int option
    }
    
let emptyModel = 
    {
        CurrentTab = Leavening
        StartTime = DateTime.Now
        EndTime = DateTime.Now.AddHours(24.)
        InitialRT = 0m
        CT = 0m
        FinalRT = 0m
        ReviewHoverValue = 0
        ReviewValue = None
    }
module UnoCash.Fulma.Updates

open System
open Elmish
open UnoCash.Fulma.Models
open UnoCash.Fulma.Messages

let init _ =
    emptyModel, Cmd.none

let update message model =
    match message with
    
    | ChangeToTab tab    -> { model with CurrentTab = tab }, Cmd.none
    
    | StartTimeChanged t -> { model with StartTime = DateTime(model.StartTime.Year,
                                                              model.StartTime.Month,
                                                              model.StartTime.Day,
                                                              Int32.Parse(t.Substring(0, 2)),
                                                              Int32.Parse(t.Substring(3, 2)),
                                                              0) }, Cmd.none
    
    | EndTimeChanged t   -> { model with EndTime = DateTime(model.EndTime.Year,
                                                            model.EndTime.Month,
                                                            model.EndTime.Day,
                                                            Int32.Parse(t.Substring(0, 2)),
                                                            Int32.Parse(t.Substring(3, 2)),
                                                            0) }, Cmd.none
        
    | StartDateChanged t -> { model with StartTime = DateTime.Parse(t)
                                                             .AddHours(float model.StartTime.Hour)
                                                             .AddMinutes(float model.StartTime.Minute) }, Cmd.none
     
    | EndDateChanged   t -> { model with EndTime = DateTime.Parse(t)
                                                           .AddHours(float model.EndTime.Hour)
                                                           .AddMinutes(float model.EndTime.Minute) }, Cmd.none
    
    | InitialRTChanged t -> { model with InitialRT = Decimal.Parse(t) }, Cmd.none
    | CTChanged t        -> { model with CT = Decimal.Parse(t) }, Cmd.none
    | FinalRTChanged t   -> { model with FinalRT = Decimal.Parse(t) }, Cmd.none
    
    | ReviewIconHover n  -> { model with ReviewHoverValue = n }, Cmd.none
    | ReviewIconLeave _  -> { model with ReviewHoverValue = 0 }, Cmd.none
    | ReviewChanged n    -> { model with ReviewValue = Some n }, Cmd.none
module UnoCash.Fulma.Leavening

open System
open Fable.React
open Fable.React.Props
open Feliz
open Fulma
open Fulma.Extensions.Wikiki
open UnoCash.Fulma.Helpers
open UnoCash.Fulma.Models
open UnoCash.Fulma.Messages

let private leaveningForm model dispatch =
    let columns left right =
        Columns.columns [ Columns.Props [ Style [ AlignContent AlignContentOptions.Center ] ] ]
                         [ Column.column [ Column.Width (Screen.All, Column.Is6) ]
                                         left
                           Column.column []
                                         right ]
                         
    let date message (modelDateTime : DateTime) =
        Input.date [ Input.Props [ Style [ Width "40%"; TextAlign TextAlignOptions.Center ] ]
                     Input.Value (modelDateTime.ToString("yyyy-MM-dd"))
                     Input.OnChange (fun ev -> ev.Value |> message |> dispatch) ]
        
    let time message (modelDateTime : DateTime) =
        Input.time [ Input.Value (modelDateTime.ToString("HH:mm"))
                     Input.Props [ Style [ Width "40%"; TextAlign TextAlignOptions.Center ] ]
                     Input.OnChange (fun ev -> ev.Value |> message |> dispatch) ]
    
    let title text =
        h1 [ Style [ TextAlign TextAlignOptions.Center ] ] [ str text ]
    
    let dateTimeBox =
        div [ Style [ TextAlign TextAlignOptions.Center; Padding "10px" ] ]
    
    let threeColumns left center right =
       Columns.columns [ Columns.IsGap (Screen.All, Columns.Is2)
                         Columns.Props [ Style [ TextAlign TextAlignOptions.Center ] ] ]
                       [ Column.column []
                                       left
                         Column.column []
                                       center
                         Column.column []
                                       right ]
    
    let column content =
       Columns.columns [ Columns.IsGap (Screen.All, Columns.Is2)
                         Columns.Props [ Style [ TextAlign TextAlignOptions.Center ] ] ]
                       [ Column.column [] content ]
    
    let card =
        Card.card []
    
    let slider changedMessage value max =
        let noHoursLeft =
            match max with
            | 0m -> true
            | _ -> false
        
        Slider.slider [ Slider.Color IsWarning
                        Slider.Props [ Style [ Width "100%" ] ]
                        Slider.OnChange (fun ev -> ev.Value |> changedMessage |> dispatch)
                        Slider.Value (float value)
                        Slider.Max (float max)
                        Slider.Step 0.5
                        Slider.Disabled noHoursLeft ]
    
    let sliderValue value =
        let isPlural = function | 1m -> "" | _ -> "s"
        div [ Style [ TextAlign TextAlignOptions.Center ] ] [ (value, isPlural value) ||> sprintf "%.2f hour%s" |> str ]
    
    let divText text =
        div [] [ str text ]
    
    let getHoursLeft model =
        let total = model.EndTime - model.StartTime
        (decimal total.TotalHours) - model.CT - model.InitialRT - model.FinalRT
    
    let dateTimes =
        {|
            FinishedInitialRT = model.StartTime.AddHours(float model.InitialRT).ToString("dd/MM HH:mm") |> sprintf "End: %s"
            FinishedCT = model.StartTime.AddHours(float (model.InitialRT + model.CT)).ToString("dd/MM HH:mm") |> sprintf "End: %s"
            FinishedFinalRT = model.StartTime.AddHours(float (model.InitialRT + model.CT + model.FinalRT)).ToString("dd/MM HH:mm") |> sprintf "End: %s"
        |}        
    
    card [
           columns [
                        title "Start"
                        dateTimeBox [
                            date StartDateChanged model.StartTime
                            time StartTimeChanged model.StartTime
                        ]
                   ]
                   [
                        title "End"
                        dateTimeBox [
                            date EndDateChanged model.EndTime
                            time EndTimeChanged model.EndTime
                        ]
                   ]
            
           threeColumns [
                h1 [] [ str "RT" ]
                slider InitialRTChanged model.InitialRT (getHoursLeft model + model.InitialRT)
                sliderValue model.InitialRT
                divText dateTimes.FinishedInitialRT
           ] [
                h1 [] [ str "CT" ]
                slider CTChanged model.CT (getHoursLeft model + model.CT)
                sliderValue model.CT
                divText dateTimes.FinishedCT
           ] [
                h1 [] [ str "RT" ]
                slider FinalRTChanged model.FinalRT (getHoursLeft model + model.FinalRT)
                sliderValue model.FinalRT
                divText dateTimes.FinishedFinalRT
           ] 
    
           column [ (model.EndTime - model.StartTime).TotalHours |>
                    sprintf "Total hours: %f" |>
                    str |>
                    List.singleton |>
                    h1 [] ] ]
                
let leaveningTimesCard model dispatch =
    card [ leaveningForm model dispatch ] Html.none

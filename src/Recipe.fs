module UnoCash.Fulma.ExpenseForm

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open Feliz
open Fulma
open UnoCash.Fulma.Helpers
open UnoCash.Fulma.Messages
open UnoCash.Fulma.Models

let private expenseForm model dispatch =
    let inlineElements elements =
        div [ Style [ Display DisplayOptions.Flex
                      FlexFlow "row wrap" ] ]
            elements
    
    let descriptionField =
        Field.div [ ]
                  [ Label.label [ ] [ str "Description" ]
                    Control.div []
                                [ Textarea.textarea [] [] ] ]
    
    let simpleField labelText icon input =
        Field.div []
                  [ Label.label [] [ str labelText ]
                    Control.div [ Control.HasIconLeft ]
                                [ input
                                  Icon.icon [ Icon.Size IsSmall; Icon.IsLeft ]
                                            [ Fa.i [ icon ] [ ] ] ] ]
    
    let dateField =
        Input.date [] |>
        simpleField "Date" Fa.Solid.CalendarDay
    
    let upload =
        Field.div []
                  [ File.file [ File.HasName ]
                      [ File.label [ ]
                          [ File.input [ ]
                            File.cta [ ]
                              [ File.icon [ ]
                                  [ Icon.icon [ ]
                                      [ Fa.i [ Fa.Solid.Upload ]
                                          [ ] ] ]
                                File.label [ ]
                                  [ str "Choose a file..." ] ]
                            File.name [ ]
                              [ str "Pre-leavening.jpg" ] ] ] ]
    
    let review =
       let icon n =
           let color =
               match (model.ReviewValue, model.ReviewHoverValue) with
               | (Some x, 0) ->
                              match n with
                              | n when n > x   -> "darkgrey"
                              | _ when x <= 1  -> "darkred"   
                              | _ when x <= 2  -> "orangered"   
                              | _ when x <= 3  -> "tomato"   
                              | _ when x <= 4  -> "darkorange"   
                              | _ when x <= 5  -> "orange"   
                              | _ when x <= 6  -> "gold"   
                              | _ when x <= 7  -> "yellowgreen"   
                              | _ when x <= 8  -> "mediumseagreen"   
                              | _ when x <= 9  -> "seagreen"   
                              | _ when x <= 10 -> "green"
                              | _              -> "darkgrey"
                              
                              // Each of a different color
//                              match n with
//                              | n when n > x -> "darkgrey"
//                              | 1  -> "darkred"   
//                              | 2  -> "orangered"   
//                              | 3  -> "tomato"   
//                              | 4  -> "darkorange"   
//                              | 5  -> "orange"   
//                              | 6  -> "gold"   
//                              | 7  -> "yellowgreen"   
//                              | 8  -> "mediumseagreen"   
//                              | 9  -> "seagreen"   
//                              | 10 -> "green"
//                              | _  -> "darkgrey"
               | (_, _) -> 
                              match model.ReviewHoverValue with
                              | x when n > x  -> "darkgrey"
                              | 1  -> "darkred"   
                              | 2  -> "orangered"   
                              | 3  -> "tomato"   
                              | 4  -> "darkorange"   
                              | 5  -> "orange"   
                              | 6  -> "gold"   
                              | 7  -> "yellowgreen"   
                              | 8  -> "mediumseagreen"   
                              | 9  -> "seagreen"   
                              | 10 -> "green"
                              | _  -> "darkgrey"

                          
           Fa.i [ Fa.Size Fa.Fa2x
                  Fa.Solid.UtensilSpoon
                  Fa.IconOption.Props [ Style [ Color color ]
                                        OnMouseOver (fun _ -> ReviewIconHover n |> dispatch)
                                        OnMouseLeave (fun _ -> ReviewIconLeave n |> dispatch)
                                        OnClick (fun _ -> ReviewChanged n |> dispatch) ] ]
                []
                
       let icons =
           [1..10] |>
           List.map icon
       
       Field.div []
                 [ Label.label [] [ str "Result" ]
                   Control.div []
                               icons ]
    
    form [ dateField
           
           inlineElements [
               simpleField "Flour type" Fa.Solid.PizzaSlice (Input.text [])
                    
               simpleField "Flour (g)" Fa.Solid.Leaf (Input.text [])
           ]
           
           inlineElements [
               simpleField "Water (ml)" Fa.Solid.Tint (Input.text [])
               
               simpleField "Hydration" Fa.Solid.Water (Input.text [])
           ]
           
           inlineElements [
               simpleField "Yeast type" Fa.Solid.Cubes (Input.text [])
               
               simpleField "Yeast (g)" Fa.Solid.Cube (Input.text [])
           ]

           inlineElements [
               simpleField "Salt (g)" Fa.Solid.Mountain (Input.text [])
               
               simpleField "Oil (g)" Fa.Solid.WineBottle (Input.text [])
           ]
           
           descriptionField
                      
           upload
           
           review ]
                
let recipeCard model dispatch =
    card [ expenseForm model dispatch ]
         Html.none
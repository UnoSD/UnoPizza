module UnoCash.Fulma.About

open Fable.React.Props
open Fulma
open UnoCash.Fulma.Helpers
open Fable.React

let private buttons =
    Card.footer []
                [ Card.Footer.a []
                                [ str "Contact" ]
                  Card.Footer.a [ Props [ Href "https://github.com/UnoSD/UnoPizza"
                                          Target "_blank" ] ]
                                [ str "GitHub" ]
                  Card.Footer.a []
                                [ str "Blog" ] ]

let aboutCard _ _ =
    let modifiers =
        Container.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ]
        
    let subText =
        "Pizza makers' companion app"
        
    card [ Hero.body []
                     [ Container.container [ Container.IsFluid
                                             modifiers ]
                     [ Heading.h1 [] [ str "UnoPizza" ]
                       Heading.h4 [ Heading.IsSubtitle ] [ str subText ] ] ] ]
         buttons
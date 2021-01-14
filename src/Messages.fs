module UnoCash.Fulma.Messages

open UnoCash.Fulma.Models

type Message =
    | ChangeToTab of Tab
    | StartTimeChanged of string
    | StartDateChanged of string
    | EndDateChanged of string
    | EndTimeChanged of string
    | InitialRTChanged of string
    | CTChanged of string
    | FinalRTChanged of string
    | ReviewIconHover of int
    | ReviewIconLeave of int
    | ReviewChanged of int
## Weston Twiner 12/17/2025
## This project is a Console application built targeting .NET 8 
## It was built as part of the interview process for Stuller Inc.



## Usage

The project has reached a V1.0.0 release and meets the criteria to run. 
It can be used to find a Pokemon's type and strengths and weaknesses by providing the
Pokemon's name.

# To run:
You may either download the zipped release and run the .exe file or 
pull the code, build the solution and run the project manually. 

# How to use:
The Console application will open and greet you, it will ask for your name. 
You may choose to provide it or not. 

Next it will ask for the name of a pokemon. If you do not provide one, it will ask again.
If you do enter something it will attempt to run the API call and retrieve the Pokemon's type information. 

If it is able to get the type information it will then use that to make a second API call to request the 
strengths and weaknesses associated with that type. 

It does support Pokemon with more than one type. 


## Improvements to be made:

1) Review Dependency Injection and Unit Tests setup
2) Support more ways of inputing pokemon and types


## Stretch Goal

I'd like to create some sort of front end to use for this other than just the Console. 
Perhaps a Blazor webpage for the experience. 

Also, adding in some form of Cache functionality to improve performance and reliability would be a good exercise.

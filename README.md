# UWP Demo App

## Intro

This is an application I had to develop from scratch, fully in solo, for the course INF3995 (final project of third year of engineering).  

It is a MVVM client app made for UWP (now deprecated), but uses similar systems to an ASP.NET Core app.

As part of a bigger project, its purpose was to monitor a custom blockchain and its miners through a log viewer and a blockchain visualizer, aswell as providing user management. As a client app, data is retrieved by querying a REST API - to fetch the blocks, the logs and the users who can login and use the app. The app can only be used past a login page, which uses a simple OAuth system (login token).  

## Technicals
Although the app is a bit old today, it used a couple of cool features at the time, including, but not limited to:
* databinding (with viewmodels)
* relay commands
* dependency injection
* a resource loader
* json schemas
* a config file manager
* a few unit tests with mocked data
* a splash screen

## Flow

On launch, the app requires the user to login. Doing so retrieves a login token, which is used for every subsequent request.  
(Seen below: Splash screen on the left, Login page on the right)

![Alt text](demo-sample1.png?raw=true "Splash")

Past the login screen, the app offers four main views : a blockchain viewer, a logs viewer, a user manager, a settings page. When switching to one view, the previous one remains alive.  
(Seen below: Logs viewer on top left, User manager on top right, Blockchain viewer on bottom left, Settings page on bottom right)

![Alt text](demo-sample2.png?raw=true "Main view")

(Not illustrated, but some care was given to UI aspects. For instance, the blockchain viewer illustrates the state of the block - red = 1 confirmation, yellow = 2, green = 3)
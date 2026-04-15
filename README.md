# Web Browser Application

## Overview 📚
This Web Browser application is a simple implementation of a windows
forms application, built using C#. It follows the 
Model-View-Presenter (MVP) design pattern to separate concerns,
improve maintainability, and testability. The application includes features 
such as bookmarking, history management, HTTP requests handling, 
and session management.

## Why 🤔
Developed as a learning project to understand the
MVP design pattern and to gain experience in building a desktop
application in C#. The application serves as a basic web browser, 
allowing users to navigate the web, manage bookmarks and history, 
and handle HTTP requests.

## Features 🚀
- **Bookmarking**: Users can bookmark their favourite websites for easy access.
- **History Management**: The application keeps track of the user's browsing history, allowing them to revisit previously visited sites.
- **HTTP Requests Handling**: The application can send HTTP requests to fetch web pages and display the HTML to the user.
- **Session Management**: The application can manage user sessions, allowing users to save their browsing state and restore it later.
- **MVP Design Pattern**: The application is structured using the MVP design pattern, which promotes separation of concerns and improves maintainability.

### Directory Structure 🏗️

The structure of the Web Browser application adheres to the 
Model-View-Controller (MVP) design pattern. 

```bash
.
├── Model/
│   ├── BookmarkHandler.cs
│   ├── HistoryHandler.cs
│   ├── HTTPClient.cs
│   └── StateHandler.cs
├── Presenter/
│   ├── SubPresenter/
│   │   ├── BookmarkingPresenter.cs
│   │   ├── NavigationPresenter.cs
│   │   ├── SessionPresenter.cs
│   │   └── ShortcutPresenter.cs
│   ├── AppBootstrapper.cs
│   ├── BrowserFacade.cs
│   └── BrowserPresenter.cs
├── Resources/
│   └── ...
├── Views/
│   ├── IViews/
│   │   ├── IApplicationStateView.cs
│   │   ├── IBookmarkView.cs
│   │   ├── IHistoryView.cs
│   │   ├── INavigationView.cs
│   │   └── IPageView.cs
│   ├── WebBrowser.cs
│   └── WebEventArgs.cs
└── Program.cs
```

🎮 Presenter: Contains the presenter layer that manage the flow of the 
application and handle user interactions. Ie., interacts with 
business logic handlers and coordinates the UI updates based 
on user actions.

💼 Model: Contains the data handling and business logic of the 
application. This includes handlers for bookmarks, history, 
HTTP requests, and application state management.

🔭 View: Contains the user interface components of the application. 
This includes interfaces for different views and their 
implementation, as well as custom event arguments for handling 
UI events.

## Compatibility 🖥️
The Web Browser application is designed to run on Windows operating
systems and as such is not compatible with other operating systems 
such as macOS or Linux.

- **.NET**: Developed on .NET 8
- **Windows 7 or later**: The application is compatible with Windows 7 and later versions.

## Future Enhancements 🌟
- **Tab Management**: Implementing support for multiple tabs to allow users to browse multiple websites simultaneously.
- **Rendering Engine**: Integrating a rendering engine to display web pages in a more user-friendly manner, rather than just showing the HTML source.
- **Security Features**: Adding features such as blocking pop-ups, managing cookies, and providing warnings for potentially harmful websites.
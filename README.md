External Plugins: <br> &nbsp;&nbsp;&nbsp;&nbsp; <a href="https://assetstore.unity.com/packages/tools/utilities/hot-reload-edit-code-without-compiling-254358">Hot Reload | Edit Code Without Compiling</a>

# Unity Programmer Assignment

# 1\. Introduction

Assume there is a backend server with the data and functionality required to communicate with the Unity application.

The backend endpoints are available at the following URLs:

1. Register
    1. URL: ...
    2. parameters:
        1. string username
        2. string password
    3. returns json with:
        1. bool isSuccessful
        2. string message
2. Login
    1. URL: ...
    2. parameters:
        1. string username
        2. string password
    3. returns json with:
        1. bool isSuccessful
        2. string message
3. GetItems
    1. URL: ...
    2. parameters:
        1. int pageNumber (starting from 1)
    3. returns json with:
        1. string message
        2. array of items containing the following:
            1. int id
            2. string name
            3. string description
            4. string image_url
            5. decimal price
            6. int retailer_id
            7. int item_category_id
4. GetRetailers
    1. URL: ...
    2. parameters: none
    3. returns json with:
        1. array of retailers class containing the following:
            1. int id
            2. string name
            3. string image_url
5. GetItemCategories
    1. URL: ...
    2. parameters: none
    3. returns json with:
        1. array of item categories containing the following:
            1. int id
            2. string name

# 2\. Unity application

Create a Unity project which consists of the following functionalities:

1. Defined screen flow
2. Preview backend item data
3. Register an account
4. Login with an existing account
5. Preview the user’s data
6. Update the user’s data

You can use any platform for Unity project development - Windows, Android, or other.

Develop the Unity application on the 9:16 portrait aspect ratio.

**Use Unity Engine version 2022.3.17f1** ([Unity download archive link](https://unity.com/releases/editor/archive))

Creating a build (.exe, .apk, or other) is **NOT necessary!**

Application layout design

Design the application screens and elements to match the design diagram on this ...

In all the locations where multiple elements are listed in a vertical layout group, only one element is shown in the diagram but you will have multiple in your functional application.

## 1\. Application Startup

1. On application startup
    1. fetch the following from the backend
        1. retailer data
        2. item category data
        3. items
    2. and while fetching (waiting for the response) display an overlay screen with a “Fetching data…” text

## 2\. Item, Account, Filtering details

### 2.1. Item fetch and display

1. Display items received from the backend in a vertical list (using a Vertical layout group).
2. Assemble the vertical list in a way that 5 items are displayed at the same time
3. Initially fetch 8 items from the backend, and display them all in the vertical list (only the first 5 being visible at once at the top of the list)
4. When scroll view is scrolled all the way down - fetch the next 8 items from the backend - repeat this functionality until there are no more new items to display
5. All fetched items remain displayed in the “growing” vertical list

### 2.2. Item screen

1. Title “Items”
2. Vertical list of items inside a Scroll Rect, where each item display consists of:
    1. Name
    2. Price
    3. Image - load from URL asynchronously
    4. Category name
    5. Retailer name
3. Scroll bar always visible on the right

### 2.3. Account screen

1. At the top of the Item display screen, display the “Account” button - clicking shows the Account screen
2. The Account screen consists of
    1. Title “Account”
    2. Input field “Email”
    3. Input field “Password”
    4. Button “Register”
    5. Button “Login”
3. On Register click - data is sent to the backend for an attempt to register
    1. if that Email is taken, the message “Email taken.” is displayed on a new overlay panel in the Unity application, with a button “Retry” below which hides the panel, showing the Account screen
    2. otherwise
        1. A user account is created on the backend with a User role
        2. the message “Account created.” is displayed on a new overlay panel in the Unity application with the a button “Continue” below which on click hides the panel, showing the Account screen
        3. user may now attempt to login with registered credentials
4. On Login click
    1. if credentials are invalid, the message “Invalid email or password.” is displayed on a new overlay panel in the Unity application with a button “Retry” below which on click hides the panel, showing the Account screen - this includes separate cases of account not existing or password mismatch
    2. otherwise
        1. the message “Welcome” is displayed in the Unity application
5. When the user is logged in
    1. hide UI elements from 2
    2. show button “Logout”
    3. below “Logout” display the logged in user’s username

### 2.4. Filtering screens

1. At the top of the Item display screen, next to the “Account” button, show buttons “Category”, and “Retailers”
2. All information for all available category data and retailer data should be fetched from the backend on application startup
3. Each of the buttons opens a new panel that contains
    1. Title “Category” or “Retailer
    2. a vertical list of Category/Retailer information with a scroll bar on the right:
        1. Category name/Retailer name
        2. Checkbox - if a checkbox is on - items from that Category or Retailer are shown in the main item display - note that multiple checkboxes can be toggled on at the same time
        3. In case of retailers screen - Retailer Image loaded from image_url in retailer data
4. When a user is successfully logged in, load the user’s saved selected Categories and Retailers from PlayerPrefs and apply them immediately.
5. On any of the Filtering screens () closing - save the changes to PlayerPrefs and apply them immediately.

Note: all listed requirements are valid for both “Category” and “Retailer” screens - they still are two separate screens.

## 3\. UI Transitions

1. Create smooth UI transitions for each overlay screen/panel
    1. timed show - transparency fade in from 0% to 100% over 0.25 seconds
    2. timed hide - transparency fade out from 100% to 0% over 0.25 seconds.

## 4\. Settings

Add a Settings screen, with the following content:

1. Title text “Settings”
2. Create a button with the text “Settings” at the top right corner of the Items screen, next to the Account, Category, Retailer buttons - on clicking the “Settings” button, open the Settings screen.
3. In the Settings screen, add the content described in sections 4.1 and 4.2.

### 4.1. Localization

1. For each text you use in the application, implement localization to Croatian and English.
2. In the Settings screen, add a button “Language” with the localized text of the currently selected language, the button press opens a new Language overlay panel with:
    1. Title text “Language” (also a localized word)
    2. Vertical list of language entries, with scroll bar on the right, each with:
        1. Country flag
        2. Language name text in a toggle button (with current language visibly selected), localized to the name of the current language
    3. Button “Confirm” (also a localized word) which changes the selected language and exits the overlay panel
    4. Button “Cancel” (also a localized word) which reverts the selected language to the one before opening the panel and closes the panel
3. Save the language on each change using Unity’s PlayerPrefs or a similar principle to store the data on the user’s device.

### 4.2. Themes

Add a Themes screen, with the following content:

1. Title text “Themes”
2. Vertical list of buttons with theme name, with the current theme visibly selected
    1. Dark Theme
    2. Light Theme
3. Add the button “Confirm” to apply the new theme and return to the settings screen.
4. Add the button “Cancel” to revert the theme to the one before opening the screen and closes the panel

Apply the Theme change immediately on the toggle state change.

Each theme changes the color of all the panel background, button, toggle and text colors, and text element colors in the application. Use the following colors per theme:

Dark theme:

1. panel background - #000000
2. text color / toggle fill image color - #FFFFFF
3. button and toggle background image color - #3da7c4

Light theme:

1. panel background - #FFFFFF
2. text color / toggle fill image color - #000000
3. button and toggle background image color - #ffe8a3

All other colors you use on the application components are not required to change with theme change.

## 5\. Favorites

Add the option to mark each item as a favorite.

Add a Favorites screen, accessible from the button “Favorites” on the main screen.

On the Favorites screen, the following content:

1. Vertical list of Favorited items with scroll bar on the right, each item listed with all the information for that item as on the Item screen.

Additionally, add a Favorite icon on each Item display on the Item screen and on this screen.

On the Item screen, the item can be added and removed to Favorites by pressing the Favorite toggle.

On the Favorites screen, the item can only be removed from the Favorites by pressing the Favorite button.

Save the items set as Favorite by their item ID using Unity’s PlayerPrefs or a similar principle to store the data on the user’s device.

# Syntax instructions

Use Microsoft’s .NET [Common C# code conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

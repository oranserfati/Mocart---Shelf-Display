# Mocart Shelf Display
 Home assignment for mocart Unity developer application

## How to start the project
Open a terminal or command prompt.
Navigate to the Builds folder of this repository:

```bash
cd path/to/Builds
```

Run the local server:

```bash
python -m http.server 8000
```

Open your browser and navigate to:

```bash
http://localhost:8000
```

The Unity WebGL build should now load in the browser.



# Project Overview
This project is a modular, event-driven Unity application designed to handle product fetching, UI interactions, and camera controls. Each component is responsible for a specific function, following object-oriented principles to keep the code modular, reusable, and easy to maintain.

## Code Structure
### 1. Events and Decoupling
The code uses C# events to allow components to communicate without direct dependencies. Key events include:
- `onClickRoll`: Triggered when the "Roll Products" button is clicked.
- `onFetchProducts`: Invoked after products are fetched from an external API.
This approach makes the system flexible and scalable by decoupling components.

### 2. Product Handling
Product fetching and display are managed with asynchronous operations and prefab instantiation:
- `ProductFetcher`: Retrieves product data asynchronously from an API and emits an event upon success.
- `ProductsController`: Dynamically populates slots with product prefabs based on fetched data, filling extra slots with dummy products if necessary.

### 3. UI Components
User interaction and feedback are handled by various UI components:
- `TextInput`: Manages text fields for product names and prices.
- `UIPopup`: Provides animated feedback using LeanTween to inform users of changes.
- `RollProductsButton` & `ResetViewButton`: Control button visibility based on user actions, enhancing the interactive experience.

## Object-Oriented Principles
Each component adheres to the Single Responsibility Principle, focusing on a specific functionality:
- **Modularity**: Components are designed independently, making the system easy to maintain and extend.
- **Reusability**: Each class can be reused or extended without impacting other parts of the system.



## External Libraries
LeanTwean - For ui animation
Package link: https://assetstore.unity.com/packages/tools/animation/leantween-3595

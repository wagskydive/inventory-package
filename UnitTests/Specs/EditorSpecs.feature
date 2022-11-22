Feature: Editor Buttons
    In order to have functional editor buttons
    As a programmer you need to be able to simlutate the button inputs 
    and verify the that they functionality works properly.

@Main-Editor-Window
Scenario: Menu Button Opens Editor Window
    Given I have unity installed and opened.
    When I click on the menu button of the Inventory Editor.
    Then The Inventory Editor window will be opened.



@Main-Editor-Window
Scenario: Inventory Loading looks for icons in folder
    Given I have a folder with an item library file
    And I have matching icons in that folder with the correct specifactions
    When I click on the load button in the Inventory File Select Window.
    Then The Inventory Editor window should show the icons.



@Recipes-Editor-Window
Scenario: Add Ingredient Button Opens Add Ingredient Window
    Given I have the recipe window open
    And I have a recipe loaded
    When I click on the add Ingredients button
    Then the Add Ingredient Window Opens.





@Add-Ingredients-Window
Scenario: Add Ingredient Button Opens Add Ingredient Window
    

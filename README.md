# AutomationTestsSample

# Task:
Create automation tests for the Alphabet application (Read the specification below)

Artifacts you should provide us with:
1. Atomation tests list
2. List of issues found during tests run

# Alphabet application specification.

We would like to ask you to create SPA (Single Page Application) using ExtJS framework
version 4.2.x.

Page layout contains 2 parts: left and right. By default the left part should take 35% of horizontal
viewport size. Page layout should be adaptive and grow/shrink when a viewport size changes.
The left part should contain a tree and the right should contain a grid (Ext.grid.Panel).

The tree should contain a root node named "English alphabet" and 10 children each named after the first 10 English alphabet letters (A, B, C, D, E, F, G, H, I, J) sorted ascending.
The grid should contain the rest of the letters (16 rows). Each row should have exactly one
english letter in an "English letter" column starting from letter "K" and to the end of the alphabet
sorted descending.

Make the tree and the grid support drag'n'drop of letters from the grid to the tree and back from
the tree to the grid. 

Application must provide an ability to add/delete missing letters into the tree/grid.

The grid would have two columns: 
1. Checkbox selection column, with multiselect allowed
2. "English letter" column, presenting an English alphabet letter.
The grid footer should contain a toolbar with two buttons: “Add” and “Delete”. 
“Add” button click should open a dialog window proposing to select/enter a letter to be added.
The dialog window should contain validation. The letter would be inserted into the grid preserving grid sort order. “Add” button should be disabled if another letter addition is not possible. “Delete” button deletes selected letters from the grid.
The tree should have context menu with two items: “Add”, “Delete”, doing the same actions as the grid corresponding buttons.

![alt tag](http://image.prntscr.com/image/f52f6b0ee9714a66bcddd235952a50b1.png)


# Results: Automated test list:
* TestSuit 1
   * Test case 1.1
   * Test case 1.2
* TestSuit 2
   * Test case 2.1
   * Test case 2.2


# Results: Bugs found
* Bug 1
* Bug 2

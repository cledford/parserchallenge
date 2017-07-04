# Generic Company Parser Code Challenge
Code Challenge Using Json.NET

## Problem 1

Develop an interactive command line application using the provided solution file NET Candidate Problem 1.zip. You must read in data from both of the given JSON files (located in the Data folder). With the following stipulations -

 

The application must contain the following commands at minimum. Note, we have provided the command line parser and included the exit command. We have stubs for most of the other required commands:

 

- exit

    Exits the application

- help

    Displays all available commands

- list

    Displays all of the data contained in the selected file

- searchby [field] [searchString]

    Searches the selected data on the given <field> and displays all rows matching any part of the <searchString> in that field. Passing no parameters to this command displays all available fields of the selected file.

- select [filename]

    Selects the data (source/file) you are viewing. Passing no parameters to this command shows the available files to view.

   
   ## Notes

1. Think of the files as data from a database table.

2. You must use JSON.NET (provided in .sln) to read in each JSON file.

3. No additional libraries should be used except what is provided in the solution.

4. Feel free to add to or alter the code as you deem necessary with any additional functionality, parameters, or commands

 
## Evaluation Criteria

 1. Your program must run and at minimum use the commands above

 2. We are not concerned about pretty printing the outputs (opt to spend less time on formatting and more on functionality)

 3. Expect to be questioned on your design choices
# Products

#Question:
Table A is in Sterling, Table B is the conversion to Euros. Write a test to check the correctness of Table B in relation to Table A. 

#Test Strategy : Applied TestPyramid approach where functionality is covered through unit tests and E2E tests


Steps to Follow:

>Pre-requsite: Install visual studio ,configure git and navigate to path, to clone the project

>git clone https://github.com/jagadeesh666/Products.git

>open the products solution in visual studio

>build the solution

>open test explorer and click on RunAll and verify that the both unit tests and specflow tests are executed successfully

Solution has 3 projects 
1) Products.Repository has the code for creating TableA in Sterling, Table B is in the conversion to Euros
2) Products.Tests has unit tests has a scenario which covers all postive and negative tests using SHOULD
3) Product.E2ETests has E2E tests using specflow BDD
#This project is to test as below 

#steps to execute scenarios
Clone the Product Solution

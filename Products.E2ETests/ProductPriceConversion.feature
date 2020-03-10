Feature: ProductPriceConversion
	In order to ensure product costs in different currencies
	As I fetch the rate for sterling to euro  
	I want to verify product costs are in sync with sterling to euro conversion

@Product @userstory001
Scenario Outline: Verify Currency Conversion from Sterling to Euros
	Given I have the product price in Sterling for each variety
	And I have the product price in Euros for each variety
	When I covert the product price from Sterling to Euros as per <rate> for each variety
	Then the currency converstion From Sterling to Euros should match for product and variety
Examples:
	| Scenario           | rate |
	| positive test case | 1.5  |




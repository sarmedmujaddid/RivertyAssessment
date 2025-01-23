Feature: Currency Conversion API Testing
  As a QA engineer
  I want to test the currency conversion REST API
  So that I can ensure its functionality and reliability

A short summary of the feature

@Positive @smoke
  Scenario: Verify successful response for default base currency as EUR
    Given I send a GET request to "GETLatest"
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the base parameter currency should be "EUR"

@Positive @Regression   
Scenario: Verify successful response conversion rates for USD
    Given I send a GET request to "GETLatest" 
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the response should contain rates for "USD" 

@Positive @Regression
Scenario: Verify successful response conversion rates for GBP
    Given I send a GET request to "GETLatest"
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the response should contain rates for "GBP" 

@Negative @Smoke
Scenario: Verify response for invalid base currency
    Given I send a GET request to "GETLatestWrongBaseCurrency"
    Then the response status code should be 200
    And the payload error code should be 201
    And the payload response should contain success as "false"
    And the payload response error type should contain "invalid_base_currency"

@Negative @Regression
Scenario: Verify response for invalid Access key
    Given I send a GET request to "GETLatestInvalidAccessKey"
    Then the response status code should be 200
    And the payload error code should be 101
    And the payload response should contain success as "false"
    And the payload response error type should contain "invalid_access_key"
    And the payload response error info should contain "You have not supplied a valid API Access Key."

@Negative @Regression   
Scenario: Verify response for missing Access key
    Given I send a GET request to "GETLatestMissingAccessKey"
    Then the response status code should be 200
    And the payload error code should be 101
    And the payload response should contain success as "false"
    And the payload response error type should contain "missing_access_key"
    And the payload response error info should contain "You have not supplied an API Access Key. [Required format: access_key=YOUR_ACCESS_KEY]"


    #Scenarios that are not automated..
@Positive 
Scenario: Validate that the default base currency (EUR) does not appear in comparison rates.
    Given I send a GET request to "GETLatest"
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the rates object should not contain the currency "EUR"

@Positive 
Scenario: Verify timestamp in the response.
    Given I send a GET request to "GETLatest"
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the response timestamp should not differ from the current server time by more than 60 seconds

@Negative
Scenario: Verify response for invalid symbols parameter
    Given I send a GET request to "GETLatestInvalidSymbols"
    Then the response status code should be 200
    And the payload error code should be 202
    And the payload response should contain success as "false"
    And the payload response error type should contain "invalid_currency_codes"
    And the payload response error info should contain "You have provided one or more invalid Currency Codes."
    
@Negative
Scenario: Verify behavior when base value is null
    Given I send a GET request to "GETLatestWithNullBase"
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the base parameter currency should be "EUR"


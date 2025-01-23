# Currency Conversion API Testing Framework
  
This project is a Behavior-Driven Development (BDD) testing framework for automating REST API testing. It is implemented in C # and utilizes SpecFlow for BDD-style Gherkin syntax and Playwright for API interaction. The framework enables robust testing of currency conversion APIs, with features for verifying status codes, response payloads, and error handling.

## Features
- #### Dynamic Request Handling: Supports various HTTP methods (GET, POST, PUT, DELETE, PATCH) with dynamic payloads and headers.

- #### JSON-Based Configuration: Reads API descriptions and payload details from JSON files for reusable and modular testing.

- #### BDD-Style Scenarios: Uses SpecFlow's Gherkin syntax for human-readable test cases, improving collaboration between QA and business teams.

   - Response Verification:
   - Status code validation.
  
- #### Verifies specific response fields like success, base currency, rates, and error details.

- #### Playwright Integration: Leverages Playwright for making API calls with features like timeout and header injection.

- #### Error Handling and Logging: Captures errors during request and response processing with detailed logging.

## Technologies Used
- #### SpecFlow: For BDD-style testing and step definitions.

- #### Playwright: For API request handling.

- #### NUnit: For assertions and test validations.

- #### Newtonsoft.Json: For JSON parsing.

- #### C#: A programming language for implementation.

# Installation Instructions

Clone the Repository:
```
git clone <https://github.com/sarmedmujaddid/RivertyAssessment.git>

cd <repository-folder>
```

# Install Dependencies:

Open the solution (.sln) file in Visual Studio.
Restore NuGet packages:

```
dotnet restore
```

Setup Configuration:

Place the API description under the /APIDescription.
Ensure proper JSON structure for the files. For example:
```Json
{
  "GETLatest": {
    "method": "GET",
    "targetURL": "https://api.example.com/",
    "endpoint": "latest",
    "queryparams": "?base=EUR",
    "headers": "{ }",
    "payload": "None"
  }
}

```
### Disclaimer: Make sure you have your API Key, if not then do not worry, Get a free one from https://fixer.io/documentation. 

![apikey](https://github.com/user-attachments/assets/aeb3ae54-ebab-40bc-baa6-0bfeaa188141)

### You need to replace your API Key in APIDescription.json. (Fixer API Key has limited usage in the Free plan) 

![accesskey](https://github.com/user-attachments/assets/9c1257e7-3ed0-460a-b382-1415431ae2e1)


How to Run Tests
Run via Visual Studio:
Open the solution in Visual Studio.
Use the Test Explorer to execute tests.

Run via Command Line:

```
dotnet test
```

Generate Reports (Optional):

Integrate with reporting tools like Allure or ExtentReports for detailed execution logs.

# Method Descriptions

## 1. CallServiceRequestAndFetchResponse

- Purpose: Sends API requests based on details from APIDescription.json.
- Inputs: serviceName (e.g., GETLatest).
- Output: API response is stored for further validation.

## 2. VerifyStatusCodeFromResponse
- Purpose: Validates that the API response status code matches the expected value.
- Inputs: statusCode (e.g., 200).
  
## 3. VerifyResponse
- Purpose: Confirms specific content (like success flags, currency rates, and error codes) exists in the response payload.
- Inputs: eResponse (expected response content).

## 4. HitServiceRequestAndReturnResponse
- Purpose: Handles API calls dynamically, supporting various HTTP methods and headers.

# Example Test Scenarios
### Feature: Currency Conversion API Testing
```Gherkins

@Positive
  Scenario: Verify successful response for default base currency as EUR
    Given I send a GET request to "GETLatest"
    Then the response status code should be 200
    And the payload response should contain success as "true"
    And the base parameter currency should be "EUR"

@Negative
Scenario: Verify response for invalid base currency
    Given I send a GET request to "GETLatestWrongBaseCurrency"
    Then the response status code should be 200
    And the payload error code should be 201
    And the payload response should contain success as "false"
    And the payload response error type should contain "invalid_base_currency"

```

# Project Structure

```

/RivertyAssessment
│
├── /Support
│   ├── RestAPIHelpers.cs       # Contains methods for API requests and validations
│
├── /StepDefinitions
│   ├── CurrencyConversionAPITestingStepDefinitions.cs # Step definitions for BDD scenarios
│
├── /Features
│   ├── CurrencyConversion.feature # Gherkin-based feature file for test scenarios
│
├── /APIDescription
│   ├── APIDescription.json     # JSON file containing API details
│
└── README.md                   # Documentation


```

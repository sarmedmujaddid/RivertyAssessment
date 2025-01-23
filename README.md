# Currency Conversion API Testing Framework
  
This project is a Behavior-Driven Development (BDD) testing framework for automating REST API testing. It utilizes SpecFlow for BDD-style Gherkin syntax, Playwright for API interaction, and is implemented in C#. The framework enables robust testing of currency conversion APIs with features for verifying status codes, response payloads, and error handling.

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

- #### C#: Programming language for implementation.

# Installation Instructions

Clone the Repository:
```
git clone <https://github.com/sarmedmujaddid/RivertyAssessment.git>

cd <repository-folder>
```

# Install Dependencies:

Open the solution (.sln) file in Visual Studio.
Restore NuGet packages:

dotnet restore
Setup Configuration:

Place the API description and payload files under the /APIDescription and /Payloads directories, respectively.
Ensure proper JSON structure for the files. For example:
```
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
- Purpose: Confirms specific content (like success flags, currency rates, error codes) exists in the response payload.
- Inputs: eResponse (expected response content).

## 4. HitServiceRequestAndReturnResponse
- Purpose: Handles API calls dynamically, supporting various HTTP methods and headers.

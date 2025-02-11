# Currency Conversion API Testing Framework

This repository contains a BDD testing framework for automating REST API testing, specifically for currency conversion APIs. It is implemented in C# using SpecFlow and Playwright for robust API testing.

## Features
- **Dynamic Request Handling:** Supports various HTTP methods (GET, POST, PUT, DELETE, PATCH) with dynamic payloads and headers.

- **JSON-Based Configuration:** Reads API descriptions and payload details from JSON files for reusable and modular testing.

- **BDD-Style Scenarios:** Uses SpecFlow's Gherkin syntax for human-readable test cases, improving collaboration between QA and business teams.
- **Response Verification:**
   - Status code validation.
   - Verifies specific response fields like success, base currency, rates, and error details.

- **Playwright Integration:** Leverages Playwright for making API calls with features like timeout and header injection.

- **Error Handling and Logging:** Captures errors during request and response processing with detailed logging.

## Technologies Used
- **SpecFlow:** For BDD-style testing and step definitions.

- **Playwright:** For API request handling.

- **NUnit:** For assertions and test validations.

- **Newtonsoft.Json:** For JSON parsing.

- **C#:** A programming language for implementation.

# Installation Instructions

Follow these steps to set up and run the RivertyAssessment project on your local machine:

#### Prerequisites
- Install .NET SDK ([Download Here](https://dotnet.microsoft.com/download))

 - Download and install the latest version of the .NET SDK from the official Microsoft .NET website.
 - Ensure the installation includes the .NET CLI for building and running the project.

- Install an IDE ([Download Here](https://visualstudio.microsoft.com/))

 - Use an IDE compatible with .NET development:
 - Visual Studio (recommended, with .NET development workload installed).

- Install Git ([Download Here](https://git-scm.com/))

  - Download and install Git from git-scm.com if you don’t already have it installed.
  - Steps to Install and Run

### Prerequisites

- **Clone the GitHub repository:** Start by cloning the GitHub repository to your local machine using the command:

```bash
git clone https://github.com/sarmedmujaddid/RivertyAssessment.git

cd RivertyAssessment

```

- Open the Solution in Your IDE

  - Open the file RivertyAssessment.sln in your IDE (e.g., Visual Studio).

# Install Dependencies

  - Use the IDE’s integrated tools to restore NuGet packages automatically.
  - Alternatively, restore packages via the command line:

```bash
dotnet restore
```

- Build the Project

  - Build the solution to ensure all dependencies are resolved:
```bash
dotnet build
```

Configuration:

Place your API key in APIDescription.json under the /APIDescription folder.
Example APIDescription.json structure:
```json
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

### Disclaimer
- The Fixer API key provided in this framework has limited usage on the free plan. Ensure you obtain your API key for extended usage. Visit [Fixer Documentation](https://fixer.io/documentation) for more details.
- 
![apikey](https://github.com/user-attachments/assets/aeb3ae54-ebab-40bc-baa6-0bfeaa188141)

Replace your API Key in APIDescription.json.

![accesskey](https://github.com/user-attachments/assets/9c1257e7-3ed0-460a-b382-1415431ae2e1)

- Run the Project

Use the **Test Explorer** to execute tests.

**Run via Command Line:**

```bash
dotnet test
```

Generate Reports (Optional):

Integrate with reporting tools like Allure or ExtentReports for detailed execution logs.

# Method Descriptions

## Request and Response Handling

### 1. CallServiceRequestAndFetchResponse

- Purpose: This method is used to extract all values of a service from ServiceDescription.json file, and pass those parameters to Helper_Services_Rest > CallRequest method
  which will actually execute the API request and return the responseSends API requests based on details from APIDescription.json.
  - Inputs: serviceName (e.g., GETLatest).
  - Output: API response is stored for further validation.
 
### 2. HitServiceRequestAndReturnResponse

- Purpose: This method allows the user to handle API calls dynamically and call various HTTP methods (Get/Post/Put/Post/Patch/Delete request). 
  - :param method: Type of request.Get or Post etc..                
  - :param url: Request URL
  - :param headers: Headers to call a request (dictionary)
  - :param payload: payload object
  - :param authToken: bearer token 
  - :param timeout: (optional) How long to wait for the server to send response
  - :return: response - response object generated on calling a request

  
## Verification Methods

### 3. VerifyStatusCodeFromResponse

- Purpose: This method verifies if the provided API response matches the expected status code.
  - :param response: response of type IRestResponse
  - :param statusCode: statusCode of type int
  - :return: boolean
  - Examples: VerifyOkResponse(response, 200)

### 4. VerifyResponse

- Purpose: Confirms specific content (like success flags, currency rates, and error codes) exists in the response payload.
  - Inputs: eResponse (expected response content).


## JSON Data Extraction

### 5. GetValueFromJsonKeyPath

- Retrieves a value from a JSON file based on the provided key path (e.g., root/level1/level2/key). Returns the value as a dictionary.

### 6. GetServiceDescriptionFromJsonFile

- Fetches the complete service description for a specific API from a JSON file, including method, URL, headers, query parameters, and payload.


## Directory and Path Utilities


### 7. FetchServicesDescriptionDirPath()

- Returns the path to the APIDescription directory where service descriptions are stored.

### 8. FetchServicePayloadPath()

- Returns the path to the Payloads directory where JSON payload files are stored.


# Example Test Scenarios
### Feature: Currency Conversion API Testing

```gherkin
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

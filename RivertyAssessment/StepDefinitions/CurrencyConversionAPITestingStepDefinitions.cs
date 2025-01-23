using RivertyAssessment.Support;
using System;
using TechTalk.SpecFlow;

namespace RivertyAssessment.StepDefinitions
{
    [Binding]
    public class CurrencyConversionAPITestingStepDefinitions
    {
        private readonly RestAPIHelpers _restAPIHelpers;

        public CurrencyConversionAPITestingStepDefinitions(RestAPIHelpers restAPIHelpers)
        {
            _restAPIHelpers = restAPIHelpers;
        }

        [Given(@"I send a GET request to ""([^""]*)""")]
        public async Task GivenISendAGETRequestTo(string getLatest)
        {
            await _restAPIHelpers.CallServiceRequestAndFetchResponse(getLatest);
        }

        [Then(@"the response status code should be (.*)")]
        public async Task ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            await _restAPIHelpers.VerifyStatusCodeFromResponse(statusCode);
        }

        [Then(@"the payload response should contain success as ""([^""]*)""")]
        public async Task ThenThePayloadResponseShouldContainSuccessAs(string flag)
        {
            await _restAPIHelpers.VerifyResponse(flag);

        }

        [Then(@"the base parameter currency should be ""([^""]*)""")]
        public async Task ThenTheBaseParameterCurrencyShouldBe(string baseCurrency)
        {
            await _restAPIHelpers.VerifyResponse(baseCurrency);
        }

        [Then(@"the response should contain rates for ""([^""]*)""")]
        public async Task ThenTheResponseShouldContainRatesFor(string currency)
        {
            await _restAPIHelpers.VerifyResponse(currency);
        }

        [Then(@"the payload error code should be (.*)")]
        public async Task ThenThePayloadErrorCodeShouldBe(string errorCode)
        {
            await _restAPIHelpers.VerifyResponse(errorCode);
        }

        [Then(@"the payload response error type should contain ""([^""]*)""")]
        public async Task ThenThePayloadResponseErrorTypeShouldContain(string errorType)
        {
            await _restAPIHelpers.VerifyResponse(errorType);
        }

        [Then(@"the payload response error info should contain ""([^""]*)""")]
        public async Task ThenThePayloadResponseErrorInfoShouldContain(string errorInfo)
        {
            await _restAPIHelpers.VerifyResponse(errorInfo);
        }
    }
}

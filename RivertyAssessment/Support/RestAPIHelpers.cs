using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RivertyAssessment.Support
{
    public class RestAPIHelpers
    {
        #region class global variables

        private string projectPath;
        private IAPIRequestContext? request = null;
        private IAPIRequestContext authRequest;
        public IAPIResponse response;
        private string? authToken = null;

        #endregion class global variables
        public RestAPIHelpers()
        {
            // This will get the current PROJECT directory upto \bin\Debug 
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            //the project directory is the grand-father of the current directory
            projectPath = currentDirectory.Parent.Parent.Parent.FullName;

        }

        #region hit request and fetch response
        public async Task CallServiceRequestAndFetchResponse(string serviceName)
        {
            // This method is used to extract all values of a service from ServiceDescription.json file, and pass those parameters to Helper_Services_Rest > CallRequest method
            // which will actually execute the API request and return the response

            try
            {
                Dictionary<String, String> dictServiceDesc = GetServiceDescriptionFromJsonFile("APIDescription.json", serviceName);
                string method = dictServiceDesc["method"];
                string url = dictServiceDesc["targetURL"] + dictServiceDesc["endpoint"] + dictServiceDesc["queryparams"];
                string headers = dictServiceDesc["headers"];
                string payload = dictServiceDesc["payload"];

                response = await HitServiceRequestAndReturnResponse(method, url, headers, payload);
                Assert.That(response != null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in CreateServiceRequest method -->" + e.Message);
                throw;
            }
        }
        #endregion hit request and fetch response


        #region get service and payload details
        public Dictionary<String, String> GetValueFromJsonKeyPath(String jsonFilePath, String keyPath)
        {
            /*
             * Description:
                |   This method fetches value of a key from a json file
            
                :return: List

                Examples:
                    |  An example of keyPath is root/level1/level2/key
             */

            try
            {
                // read file from Path and parse it into JSON object
                JObject jsonSDFileData = JObject.Parse(File.ReadAllText(jsonFilePath));
                // get JSON keyPath objects and convert into a dictionary
                Dictionary<String, String> serviceParams = jsonSDFileData[keyPath].ToObject<Dictionary<string, string>>();

                return serviceParams;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in GetValueFromJsonKeyPath method -->" + e.Message);
                throw;
            }
        }

        public String FetchServicesDescriptionDirPath()
        {
            /*
             * Description:
                |   This method fetches path of the {project}/Services/ServiceDescription directory
             */

            try
            {
                String serviceDescPath = Path.Combine(projectPath, "", "APIDescription");
                return serviceDescPath;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in FetchServiceDescriptionPath method-->" + e.Message);
                throw;
            }
        }

        public String FetchServicePayloadPath()
        {
            /*
             * Description:
                |   This method fetches path of the {project}/Services/Payloads directory
             */

            try
            {
                String servicePayloadsPath = Path.Combine(projectPath, "", "Payloads");
                return servicePayloadsPath;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in FetchServicePayloadPath method-->" + e.Message);
                throw;
            }
        }

        public Dictionary<String, String> GetServiceDescriptionFromJsonFile(string serviceDescRelFilePath, string keyPath)
        {
            /*
             * Description:
                |  This method fetches entire service description params of a particular endpoint from the service description json file.
                |  The service description json file contains below keys
                |  method:
                |  targetURL:
                |  endpoint:
                |  queryparams:
                |  headers:
                |  authType:
                |  username:
                |  password:
                |  payload:                

                :param serviceDescRelFilePath: Relative path of the service description file      
                :param keyPath:

                :return: Dictionary
             */

            try
            {
                Dictionary<String, String> dictServiceDesc = new Dictionary<String, String>();
                String serviceDescPath = Path.Combine(FetchServicesDescriptionDirPath(), serviceDescRelFilePath);
                Dictionary<String, String> dictServiceDescription = GetValueFromJsonKeyPath(serviceDescPath, keyPath);

                dictServiceDesc["method"] = dictServiceDescription["method"];
                dictServiceDesc["targetURL"] = dictServiceDescription["targetURL"];
                dictServiceDesc["endpoint"] = dictServiceDescription["endpoint"];

                if (dictServiceDescription["queryparams"] == "None")
                    dictServiceDesc["queryparams"] = "";
                else
                    dictServiceDesc["queryparams"] = dictServiceDescription["queryparams"];

                if (dictServiceDescription["headers"] == "None")
                    dictServiceDesc["headers"] = "{ }";
                else
                    dictServiceDesc["headers"] = dictServiceDescription["headers"];

                if (dictServiceDescription["payload"] == "None")
                    dictServiceDesc["payload"] = "";
                else
                {
                    String payloadPath = Path.Combine(FetchServicePayloadPath(), dictServiceDescription["payload"]);
                    String jsonPayload = File.ReadAllText(payloadPath);
                    dictServiceDesc["payload"] = jsonPayload;
                }

                return dictServiceDesc;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in GetServiceDescriptionFromJsonFile method -->" + e.Message);
                throw;
            }
        }
        #endregion get service and payload details


        #region prepare and hit request
        public async Task<IAPIResponse> HitServiceRequestAndReturnResponse(String method, String url, String headers, Object payload, int timeout = 60000)
        {
            /*
             * Description:
                |   This method allows user to call a Get/Post/Put/Post/Patch/Delete request

                :param method: Type of request.Get or Post etc..                
                :param url: Request URL
                :param headers: Headers to call a request (dictionary)
                :param payload: payload object
                :param authToken: bearer token 
                :param timeout: (optional) How long to wait for the server to send response
                
                :return: response - response object generated on calling a request

                .. note::
                    |  method (String) :
                    |  Accepts: Get, Post, Put, Patch or Delete
                    |
                    |  authType(String) :
                    |  Accepts: basic, ntlm, digest, proxy                                                           
             */

            try
            {
                var requestHeaders = new Dictionary<string, string>();

                // add header parameters, if any
                if (!String.IsNullOrEmpty(headers))
                {
                    Dictionary<String, String> dictionary = JObject.Parse(headers).ToObject<Dictionary<string, string>>();
                    foreach (var param in dictionary)
                        requestHeaders.Add(param.Key, param.Value);
                }

                // add token parameter, if any
                if (!String.IsNullOrEmpty(authToken))
                {
                    requestHeaders.Add("Authorization", "Bearer " + authToken);
                }

                var playwright = await Playwright.CreateAsync();
                request = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
                {
                    ExtraHTTPHeaders = requestHeaders,
                    Timeout = timeout,
                    IgnoreHTTPSErrors = true,
                });

                if (method.ToUpper() == "GET")
                {
                    response = await request.GetAsync(url);

                }

                else if (method.ToUpper() == "POST")
                {
                    response = await request.PostAsync(url, new APIRequestContextOptions()
                    {
                        DataString = (string)payload
                    });
                }

                else if (method.ToUpper() == "PUT")
                {
                    if (requestHeaders["Content-Type"] == "multipart/form-data")
                    {
                        //Passing filepath
                        var filePath = projectPath + "\\TestData\\TestFile.txt";

                        //Read file content in bytes
                        var fileContent = await File.ReadAllBytesAsync(filePath);
                        var multiFormData = request.CreateFormData();

                        //Set multiform data
                        multiFormData.Set("RecordFile", new FilePayload()
                        {
                            Name = "TestFile.txt",
                            MimeType = "text/plain",
                            Buffer = fileContent
                        });

                        response = await request.PutAsync(url, new APIRequestContextOptions()
                        {
                            Multipart = multiFormData
                        });
                    }
                    else
                    {
                        response = await request.PutAsync(url, new APIRequestContextOptions()
                        {
                            DataObject = payload
                        });
                    }
                }

                else if (method.ToUpper() == "PATCH")
                {
                    response = await request.PatchAsync(url, new APIRequestContextOptions()
                    {
                        DataObject = payload
                    });
                }

                else if (method.ToUpper() == "DELETE")
                {
                    response = await request.DeleteAsync(url);
                }

                else
                    Console.WriteLine("Method can be any one among Get/Post/Put/Patch/Delete but the value is " + method);

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in CallRequest method -->" + e.Message);
                throw;
            }
        }
        #endregion prepare and hit request


        #region status code verification        
        public async Task VerifyStatusCodeFromResponse(int statusCode)
        {
            /*
             * Description:
            	|  This method is used to validate if the expected status code is same as returned by the provided API response.
  
                :param response: response of type IRestResponse
                :param statusCode: statusCode of type int
  
                :return: boolean
                Examples:
                |  VerifyOkResponse(response, 200)
                                                          
             */

            try
            {
                Assert.That(statusCode == response.Status, $"Expected Status Code: {statusCode}\n Actual Status Code: {response.Status}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in VerifyStatusCodeFromResponse method -->" + e.Message);
                throw;
            }

        }
        #endregion status code verification

        #region response content verification
        public async Task VerifyResponse(string eResponse)
        {
            try
            {
                var jsonResponse = await response.JsonAsync();
                string responseContent = jsonResponse?.ToString();
                Assert.That(responseContent, Does.Contain(eResponse).IgnoreCase);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in VerifyResponseContent method -->" + e.Message);
                throw;
            }
        }
        #endregion response content verification
    }
}

using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RestSharpTestCase
{
    [TestClass]
    public class Employee
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string SALARY { get; set; }
    }
    [TestClass]
    public class RESTSharp
    {
        RestClient client;

        [TestMethod]
        public void OnCallingGetMethod_CompareCount_ShouldReturnEmployeeList()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee", Method.Get);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(22, list.Count);
            foreach (Employee value in list)
            {
                Console.WriteLine("Id:-" + value.ID + " " + "NAME:-" + value.NAME + " " + "SALARY:-" + value.SALARY);
            }
        }
        [TestMethod]
        public void OnPostingEmployeeData_AddtoJsonServer_ReturnRecentlyAddedData()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee", Method.Post);
            var body = new Employee { NAME = "JAGADEESH", SALARY = "60000" };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee exp = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("JAGADEESH", exp.NAME);
            Assert.AreEqual("60000", exp.SALARY);
            Console.WriteLine(response.Content);
        }
        [TestMethod]
        public void OnPostingMultipleEmployees_AddToJsonServer_ReturnListOfAddedData()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            List<Employee> list = new List<Employee>();
            list.Add(new Employee { NAME = "AMMA", SALARY = "25000" });
            list.Add(new Employee { NAME = "NANNA", SALARY = "60000" });
            list.Add(new Employee { NAME = "CHITTI", SALARY = "40000" });
            list.ForEach(body =>
            {
                RestRequest request = new RestRequest("/Employee", Method.Post);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                //Act
                RestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                Employee exp = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual(body.NAME, exp.NAME);
                Assert.AreEqual(body.SALARY, exp.SALARY);
                Console.WriteLine(response.Content);
            });
        }
        [TestMethod]
        public void OnUpdatingEmployeeData_ShouldUpdateValueInJsonServer()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee/4", Method.Put);
            List<Employee> list = new List<Employee>();
            Employee body = new Employee { NAME = "lakshmi", SALARY = "70000" };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee exp = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("lakshmi", exp.NAME);
            Assert.AreEqual("70000", exp.SALARY);
            Console.WriteLine(response.Content);
        }
        [TestMethod]
        public void OnDeletingEmployeeData_ShouldDeleteDataInJsonServer()
        {
            client = new RestClient("http://localhost:4000");
            //Arrange
            RestRequest request = new RestRequest("/Employee/15", Method.Delete);
            //Act
            RestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(response.Content);
        }

    }
    
}
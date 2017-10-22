# Nao Robot using Microsoft Azure

Nao (pronounced as now) is an autonomous, programmable humanoid robot by SoftBank Group.

More info here: [**Wikipedia on Nao**](https://en.wikipedia.org/wiki/Nao_(robot))

This project is a joint-collaboration with customers to explore the potential use case and how to implement artificial intelligence within Nao.

Here, we will explore the potential of Nao as digital concierge. Breakdown of content as below:

1. Use Case Description
1. Technical Overview of Nao
1. Solution Overview
1. Next Step
1. Reference

## 1. Use Case Description
Nao looks like a human, or like a little boy. First impression, Nao can be next generation digital concierge, who is able to answer typical concierge questions such as direction, general enquiries etc. Since we can program the motor, it adds more interaction during response. For example, when users are asking for direction, Nao can point to a specific direction on top of the response, i.e. when Nao reply "It's on your left", it will point to left as well.

This use case is straight forward, and a lot of companies are exploring this. One example is collaboration between IBM and Hilton, where IBM leverages Watson to provide AI capabilities to Nao. More info here: [**NAO, IBM create new Hilton concierge**](https://developer.softbankrobotics.com/us-en/showcase/nao-ibm-create-new-hilton-concierge) This project shows the capabilities of Nao with AI, but the main focus here is how users can simplify the process, yet able to leverage on some AI capabilities. IBM Watson may require subject expertise to train it, and cost of deploying IBM is non-neglectable. 

The focus of this project is exploring a cost-effective solution to achieve digital concierge.

## 2. Technical Overview of Nao
Nao is running a custom distribution of Linux called **Gentoo**. To develop solutions on Nao, the SDK used is **Choregraphe**. This tool is simple and intuitive, as it is drag-and-drop GUI. Developers just need to connect the modules to create flow. Of course, the build-in tools may not be sufficient, and fortunately, developers have the option to create custom script on **Python**. In short, Nao is a full-fledge system which can run multiple processes. However, the hardware may not be optimized to run complex processes such as artificial intelligence, machine learning and deep learning.

Apart from processing unit, Nao comes with many sensors that can be configured, for instance LED of his eye, motors around the body, mic, or even camera. With right configuration, all these sensors can provide a human-like experience solution on Nao.

Here's the technical overview of Nao: [**Nao Technical Overview**](http://doc.aldebaran.com/2-1/family/robots/index_robots.html#all-robots)

Before we move on, a few things to take note. The main language used here is Python, and we need several packages to be installed. You can't install the packages from IDE. Since I'm using windows 10, I used [**Bash**](https://blogs.technet.microsoft.com/canitpro/2016/06/07/step-by-step-enabling-bash-on-windows-10/) to SSH into Nao and install [_pip_](https://pypi.python.org/pypi/pip/) to make life easy. Nao, out of the box, do not have pip......

```bash
ssh nao@<ip address>
```

PS: _I forgot how i install PIP without pip install!_ With pip installed, you can easily install other python packages from the terminal.

```bash
pip install SpeechRecognition
```

Another tool that I used to transfer/retrieve files is [Filezilla](https://filezilla-project.org/). This is handy when you want to transfer whl file into Nao!

## 3. Solution Overview
Given the limitation of hardware, one suggestion is offload processing task to cloud, and bring back to Nao for response. 

Consider the following scenario: users are asking Nao questions via speech, Nao need to _convert speech to text for processing_. Once converted, it needs to _analyze the sentiment_ of users to decide whether a human intervention is required. If sentiment is low, it will trigger a specific task to notify users/admin. Else, it will _search the answer and respond_ to users. The knowledge base of Nao must be expandable, and _it needs to learn_ more and more over time, thus a database is required to _archive conversations_, as well as record the unanswered questions, so that admin can re-train knowledge base using the unanswered questions. 

Looking at this scenario, we decided to leverage on the following technology/tools for 2 simple reasons, (1) **ease of usage**, (2) **cost effective**. We will walk thru the solution in detailed.

#### (A) Microsoft Cognitive Services
1. [**Bing Speech API**](https://azure.microsoft.com/en-us/services/cognitive-services/speech/)
1. [**Text Analytics API**](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/)
1. [**QnA Maker API**](https://azure.microsoft.com/en-us/services/cognitive-services/qna-maker/)

#### (B) Microsoft Azure
1. [**Azure Function**](https://azure.microsoft.com/en-us/services/functions/)
1. [**Azure Table Storage**](https://azure.microsoft.com/en-us/services/storage/tables/)

One of the advantages that we observed is agile. The solution is modularized, and we can always replace one another. To developers, they can train and configure the knowledge over cloud without running the process within Nao. All services above can be invoked using REST API. 

This is just one aspect of the whole solution. From manageability perspective, we wanted to monitor the status of Nao, because Nao will be deployed in several locations, and we need to monitor/control it remotely. One of the concern raised is overheating. Throughout the development, we noticed that using ethernet instead of WiFi will generate more heat for CPU, when CPU is overheated, the motors may not move accordingly, Nao will down and causes damage. 

Since there are so many sensors built-in, we decided to stream relevant sensors value back to command center, by creating a bi-directional gateway for cloud-device communication. Of course, when data is streamed back, real-time analytics can be applied to pre-amp admin if there's any potential issue. On top of that, we built chatbot for admin to send command over to Nao. This will provide flexibility and agility for admin to monitor the status. Services that we used are:

#### (A) Microsoft Azure IoT Suite
1. [**Azure IoT Hub**](https://azure.microsoft.com/en-us/services/iot-hub/)
1. [**Azure Stream Analytics**](https://azure.microsoft.com/en-us/services/stream-analytics/)
1. [**Azure Event Hub**](https://azure.microsoft.com/en-us/services/event-hubs/)
1. [**Azure Machine Learning**](https://azure.microsoft.com/en-us/services/machine-learning-studio/)

#### (B) Real-time Data Visualization + Dashboard
1. [**Power BI**](https://powerbi.microsoft.com/en-us/)

#### (C) Bots
1. [**Azure Bot Services**](https://azure.microsoft.com/en-us/services/bot-service/)
1. [**Language Understanding Intelligent Service**](https://www.luis.ai/home)

With all these services, here's the overview architecture. First, let’s look at IDE, and here’s the processes that we configured in Nao.

![Nao IDE]( https://eijqqg-dm2306.files.1drv.com/y4m6OkmWCMH5OalzPUgwy1-ssW3OdyQuTonwlqzSqnAntoiDGtYDm1n6QfxNpDmZBUae4_kMW1BshrNyQ6Fh63uDOa929zF32y-DzHjUj8ARZTv3rvhKWGBQvRwjcKbVftoF5GpAHklJB-cLoLiIkb-ILcFeaoduf-Zu_Mi0IqOyBz7BSoFJ8_5Lrt3S46cv30Wyg-mj7DfaXU9y6k6Afac-g?width=1200&height=765&cropmode=none)

##### (A) Flow for Conversation
1. Check Internet Connection.
1. Notify users if Nao is in online mode.
1. Start speech recognition, by default, Nao will record a sound file when speech is detected.
1. Then the file path of recorded sound clip will be sent to Bing Speech API to extract text.
1. Text will then be sent to Azure Function to process response.
1. In Azure Function, first we analyze sentiment of input, and it will trigger a negative response (Something like “Please don’t scold me”) when it detects users are upset.
1. If not, it will call QnA Maker API to get response.
1. If answer is found, then Azure Function will output the response. Else, it will be recorded in Table Storage for admin to update.
1. Both input and output are archived in table storage for validation.
1. Key word in responses from Azure Function will trigger specific movement.
1. Repeat step 3.
1. At any point in time, if head is tapped, it will stop the conversation.

##### (B) Flow for Sensors Monitoring
1. Check for internet connection.
1. Set standing posture as initialization.
1. Read sensors value, here in our case, temperature of CPU, and Hands.
1. Create a JSON message to record readings.
1. Send over to IoT Hub via HTTP.
1. Check if there’s any cloud-to-device messages from IoT Hub.
1. If yes, trigger specific actions. For simplicity, we trigger stand up and sit down.
1. Delay for 0.5 seconds. Repeat step 3.

Based on the flow above, we are trying to off load the processing task in Azure while Nao is more on output and actions based on responses. Nao, let’s look at the processes from Azure perspective. Here’s the architecture diagram to connect all Azure services.

![Nao Azure]( https://rdkwzq-dm2306.files.1drv.com/y4mVOXv5_kyrwUICcT7hronQpoLcUCSZ7ClLoIAjNiVPViEJ3b_7NN8UZagsE67dW0bAPfBX_sV6zvtl3-9I8SfKid_fdWnwe7XgbEJuZMBM5v3a8KXVuF3wSkQYz59m5G0Em_qLt7e4yPAJqJ1fGxS_1z3JQy3Ab88snJBh4fDBit43LIX5GBr6wTr0H8_2gwWgcmVXvkhYP6hMLmJ8Ly6KQ?width=1024&height=822&cropmode=none)

There are 3 sections in diagram above, let’s look at every individual section below.
##### (1) Speech to Text
There’s a built-in speech to text engine within Nao, but the challenge here is the accuracy, especially we may have different accent. Thus, we leverage Bing Speech API to retrieve text. Of course, there are many other Speech to Text API such as Google, or other python library, and these services provide similar performance. For ease of management, we use Bing Speech API here. Note that this increases the latency compared to using the build-in engine, as it will send the audio file over to cloud for processing. 
```python
Import speech_recognition as sr

def onInput_wavPath(self, p):
        self.executing = True
        WAV_FILE = p #get wav file path
        key = '<API Key>'

        r = sr.Recognizer()
        with sr.WavFile(WAV_FILE) as source:
            audio = r.record(source) # read the entire WAV
	response = r.recognize_bing(audio,key) # here we get the text from user input
```


Once text is extracted, it then passes to next module to process the input and generate output.

##### (2) Conversation
As described above, string extracted from previous module will then be sent to Azure Functions as input. Here, we use HTTP to trigger Azure Function, and inputs form the body of this HTTP POST. 
Here’s the snippet of calling Azure Function.

```python
        try:
            response = r.recognize_bing(audio,key)
            if self.executing:
                url = "https://naorobot.azurewebsites.net/api/NaoResponse?code=<API Key>"
                payload = '{\"query\":\"' + str(response) + '\"}'
                headers = {
                    'content-type': "application/json"
                        }
                response2 = requests.request("POST", url, data=payload, headers=headers)
                replyAns = str(response2.text)
                self.outputString(replyAns)
        except sr.UnknownValueError:
            self.outputError("Can you repeat that? I couldn't understand you.")
        except sr.RequestError as e:
            self.outputError("I can't reach internet! The internet must be broken!")
        except sr.URLError as e:
            self.outputError("Something went wrong. Idk what it is but though.")
        except socket.timeout:
            self.outputError("One of my sockets timed out!")
            print "Timed out!"
        pass
```

From here, let’s look at Azure function code (written in C#) to see how the message is processed.

Start from extracting message from HTTP POST. Here, we set query to be the body text.

```csharp

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // parse query parameter
    string query = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "query", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
query = query ?? data?.query;
}
```

Then, we analyze the sentiment. We can just invoke Text Analytics API via HTTP, so it’s straight forward. The JSON object of response is defined as below:
```csharp
public class Document
{
    public double score { get; set; }
    public string id { get; set; }
}

public class sentimentObj
{
    public IList<Document> documents { get; set; }
    public IList<object> errors { get; set; }
}
```

To invoke the services, this can be done easily by using HTTP Client of C#. Here’s the code to achieve what we need:

```csharp
public class SentimentAnalysis
{
    private const string baseURL = "https://westus.api.cognitive.microsoft.com/";
    private const string AccountKey = "<Text Analytics API Key>";

    public static async Task<double> MakeRequests(string input)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            byte[] byteData = Encoding.UTF8.GetBytes("{\"documents\":[" +
            "{\"id\":\"1\",\"text\":\"" + input + "\"},]}");
            var uri = "text/analytics/v2.0/sentiment";
            var response = await CallEndpoint(client, uri, byteData);
            return response.documents[0].score;
        }
    }

    public static async Task<sentimentObj> CallEndpoint(HttpClient client, string uri, byte[] byteData)
    {
        using (var content = new ByteArrayContent(byteData))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            sentimentObj sentimentJSON = JsonConvert.DeserializeObject<sentimentObj>(result);
            return sentimentJSON;
        }
    }
}
```
Once we processed the sentiment, we then decide on whether to proceed QnA Maker API or reply to users directly. The code to invoke QnA Maker API is identical to calling Text Analytics API, we simply just perform HTTP POST, you can find the code in \Conversation\NaoResponse folder.

The final things that we do here is record the conversation in Table Storage. Again, we use HTTP POST to invoke another Azure Function to store the conversation in Table format. Here’s the sample of performing HTTP POST.

```csharp
public class UpdateTable
{
    private const string converURL = "<Table Storage URL>";
    private const string converKey = "<Table Storage Key>";

    public static async Task<string> converTable(string query, string answer, double sentiment)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(converURL + converKey);
            byte[] byteData = Encoding.UTF8.GetBytes("{\"query\": \"" + query + "\",\n    \"answer\": \"" + answer + "\",\n  \"sentiment\": " + Math.Round(sentiment,3).ToString() + "}");
            var itemContent = new ByteArrayContent(byteData);
            itemContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(converURL + converKey, itemContent);
            return "ok";
        }
    }  
}
```
To developer, they just need to focus on building the knowledge base from cloud, Nao will always get the latest knowledge, without re-deploying the solution into Nao. This can effectively simplify knowledge base management and training process. Recall that we record unanswered questions in Table storage, so admin can just update the knowledge base based on what is recorded in this separate table. To retrain the knowledge base, it’s as simple as input the questions and answer, no coding is required. For more info, refer to [**QnA Maker Portal**](https://qnamaker.ai/).

This conclude conversation section, and let’s move on to monitoring.

##### (3) Monitoring
As mentioned above, Nao has numerous sensors, and depends on need, developer can choose the relevant values and stream over to IoT. In our case, we want to monitor CPU temperature, hands temperature, as well as battery status.
To do that, first we need to tell Nao that we want to access the value stored in its memory. This can be done with this line of code. 

```python
def onLoad(self):
    self.memory = ALProxy("ALMemory")
    pass
```
To retrieve sensors value, you need to define path, the rest of the info can be found here: [Sensors list]( http://doc.aldebaran.com/2-1/family/nao_dcm/actuator_sensor_names.html?highlight=sensors)

We retrieve the value, and convert to string after we round it.

```python
sTemplate = "Device/SubDeviceList/%s/Temperature/Sensor/Value"

batterySensors = sTemplate % "Battery"
headSensors = sTemplate % "Head"
lHandSensors = sTemplate % "LHand"
rHandSensors = sTemplate % "RHand"

batteryValue = str(round(self.memory.getData(batterySensors),2))
headValue = str(round(self.memory.getData(headSensors),2))
lHandValue = str(round(self.memory.getData(lHandSensors),2))
rHandValue = str(round(self.memory.getData(rHandSensors),2))
```

Then we construct the message to be sent to IoT Hub.

```python
jsonTemplate = '{\"battery\":%s, \"head\":%s,\"lHand\":%s,\"rHand\":%s}'

jsonMessage = jsonTemplate % (batteryValue, headValue, lHandValue, rHandValue)
```

Now, take a closer look at sending the message to IoT Hub. Azure IoT Hub works with Python, the information can be found here: [**Azure IoT Hub with Python**](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-python-getstarted) It requires to install python packages in Nao. Due to limited internal storage of Nao (we have installed many other packages, that’s why! Duh…), we decided to use REST API instead.  This can be done easily via Python:

```python
try:
# first we perform HTTP Post to send D2C message to IoT Hub
sas = "<IoT Hub Connection String>"
 url = "https://NaoRobotIoT.azure-devices.net/devices/nao_python_1/messages/events?api-version=2016-02-03"
payload = jsonMessage
headers = {
    'Authorization': sas,
    }
response = requests.request("POST", url, data=payload, headers=headers)
``` 
Sample data sent to cloud:

```t
10/8/2017 5:21:47 PM> Device: [nao_python_1], Data:[{"battery":"2.9", "head":"58.0","lHand":"38.0","rHand":"38.0"}]
10/8/2017 5:21:53 PM> Device: [nao_python_1], Data:[{"battery":"2.9", "head":"60.0","lHand":"38.0","rHand":"38.0"}]
```

Once the message reached IoT Hub, it will go thru Stream Analytics, Event Hub, Power BI, Azure Machine Learning etc. I have a repository to document this, you can refer them here: [**Azur IoT Suite Tutorial on GitHub**](https://github.com/guangying94/AzureIoT)

Another aspect of IoT Hub is receiving messages from cloud to device. This can be done via HTTP GET easily, again from Python.

```python
# here we perform HTTP Get to receive C2D message from IoT Hub
url2 = "https://naorobotiot.azure-devices.net/devices/nao_python_1/messages/devicebound"
querystring = {"api-version":"2016-02-03"}
headers2 = {
    'authorization': sas,
}
response2 = requests.request("GET", url2, headers=headers2, params=querystring)
```

Once the message is received, we then trigger predefined actions such as stand up and sit down from Nao IDE.

##### (4) Remote Control via Chatbots
The final approach is leveraging on chatbot to send messages to IoT Hub. I have written a few Azure Bot Services tutorial and published them in GitHub, you can find them here, [**Azure Bot Services Tutorial on GitHub**](https://github.com/guangying94/Azure-Bot-Services-Sample). Slides format can be found in [**Speaker Deck**](https://speakerdeck.com/guangying94/azure-bot-services-guide).

One thing that I want to highlight here is how we send D2C message into IoT Hub. You will need to install “Microsoft.Azure.Devices” package from NuGet. Then, you can send message to IoT Hub using the following code:

```csharp
private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
{
    var activity = await result as Activity;

    ServiceClient serviceClient;
    string connectionString = "HostName=NaoRobotIoT.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=<API Key>";
    serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

    var commandMessage = new Message(Encoding.ASCII.GetBytes(activity.Text));
    await serviceClient.SendAsync("nao_python_1", commandMessage);

    string sayText = "";
    if(activity.Text.Contains("stand"))
    {
        sayText = "Nao is standing right now";
    }
    {
        sayText = "I have asked Nao to sit.";
    }

    await context.SayAsync($"Message: {activity.Text} is sent.", sayText);

    context.Wait(MessageReceivedAsync);
}
```

For demo purpose, we only use 2 simple commands, of course, you can leverage on [LUIS](https://luis.ai) to improve user experience. Again, refer to the Azure Bot Services repository on creating a bot.

So, this conclude the technical overview of Azure services used.

## 4. Next Step

This is just first step, and we barely scratched the surface of Nao. For instance, areas that we can explore includes navigation, or even computer vision, since Nao is equipped with camera. 

With Face recognition, we can also explore to customize conversation based on users. With object detection and sonar sensors around Nao, perhaps we can program Nao such that it can recognize certain object and pick them up. 

Depends on the knowledge base, another route that we can explore is using Nao as office concierge to allows employee to book rooms, as well as welcoming guest in concierge, and even guide them to the meeting room.

There’s unlimited possibilities waiting for us to explore!

## 5. Reference
This solution draws a lot inspiration from [**Zachery Thomas**]( https://www.youtube.com/watch?v=1-DWayGQoCI), really appreciate for sharing the source code online! 😊 

Of course, another reference is drawn from [**Avatarion**](https://microsoft.github.io/techcasestudies/iot/2017/04/04/avatarion.html). They build one of the most amazing solution for kids using Nao and Microsoft Azure.

With that, we are proud to present you the masterpiece of Nao V1 here! 

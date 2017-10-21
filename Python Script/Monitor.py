import requests

class MyClass(GeneratedClass):
    def __init__(self):
        GeneratedClass.__init__(self, False)

    def onLoad(self):
        self.memory = ALProxy("ALMemory")
        pass

    def onUnload(self):
        pass

    def onInput_onStart(self):
        # Currently only monitor temperature
        sTemplate = "Device/SubDeviceList/%s/Temperature/Sensor/Value"
        jsonTemplate = '{\"battery\":%s, \"head\":%s,\"lHand\":%s,\"rHand\":%s}'

        batterySensors = sTemplate % "Battery"
        headSensors = sTemplate % "Head"
        lHandSensors = sTemplate % "LHand"
        rHandSensors = sTemplate % "RHand"

        batteryValue = str(round(self.memory.getData(batterySensors),2))
        headValue = str(round(self.memory.getData(headSensors),2))
        lHandValue = str(round(self.memory.getData(lHandSensors),2))
        rHandValue = str(round(self.memory.getData(rHandSensors),2))

        jsonMessage = jsonTemplate % (batteryValue, headValue, lHandValue, rHandValue)

        try:
            # first we perform HTTP Post to send D2C message to IoT Hub
            sas = "<IoT Hub Connection String>"
            url = "https://NaoRobotIoT.azure-devices.net/devices/nao_python_1/messages/events?api-version=2016-02-03"
            payload = jsonMessage
            headers = {
                'Authorization': sas,
                }
            response = requests.request("POST", url, data=payload, headers=headers)

            # here we perform HTTP Get to receive C2D message from IoT Hub
            url2 = "https://naorobotiot.azure-devices.net/devices/nao_python_1/messages/devicebound"
            querystring = {"api-version":"2016-02-03"}
            headers2 = {
                'authorization': sas,
                }
            response2 = requests.request("GET", url2, headers=headers2, params=querystring)
            sendMessage = str(response2.text)
            self.message(sendMessage)
        except:
           self.logger.error("Error")
        pass
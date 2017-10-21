import speech_recognition as sr
import socket
import requests
import json


class MyClass(GeneratedClass):


    def __init__(self):
        GeneratedClass.__init__(self)
        self.executing = True

    def onLoad(self):
        pass

    def onUnload(self):
        self.executing = False
        self.outputError("Sorry, I spaced out. Can you say that again?")
        pass

    def onInput_wavPath(self, p):
        self.executing = True
        WAV_FILE = p #get wav file path
        key = '<Speech to text API Key>'

        r = sr.Recognizer()
        with sr.WavFile(WAV_FILE) as source:
            audio = r.record(source) # read the entire WAV

        try:
            print "---------------- Detecting -------------------"
            response = r.recognize_bing(audio,key)
            if self.executing:
                url = "<Azure Function URL>"
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
            self.outputError("Something went wrong.")
        except socket.timeout:
            self.outputError("One of my sockets timed out!")
            print "Timed out!"
        pass

    def onInput_stop(self):
        self.onUnload()
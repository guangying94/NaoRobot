class MyClass(GeneratedClass):
    def __init__(self):
        GeneratedClass.__init__(self)

    def onLoad(self):
        pass

    def onUnload(self):
        pass

    def onInput_onStart(self,p):
        action = str(p)
        if 'sit' in action:
            self.output_sit()
        elif 'stand' in action:
            self.output_stand()
        else:
            self.default()
        pass

    def onInput_onStop(self):
        self.onUnload()
        self.onStopped()
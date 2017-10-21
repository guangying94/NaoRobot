class MyClass(GeneratedClass):
    def __init__(self):
        GeneratedClass.__init__(self)

    def onLoad(self):
        pass

    def onUnload(self):
        pass

    def onInput_onStart(self,p):
        action = str(p)
        if 'left' in action:
            self.output_left()
        elif 'right' in action:
            self.output_right()
        elif 'up' in action:
            self.output_up()
        elif 'front' in action:
            self.output_front()
        elif 'sorry' in action:
            self.output_sorry()
        elif 'hurt' in action:
            self.output_cry()
        else:
            self.default()
        pass

    def onInput_onStop(self):
        self.onUnload()
        self.onStopped()